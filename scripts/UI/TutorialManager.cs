using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class TutorialManager : Node
{
	private record TutorialStep(string Message, string ButtonText, Func<bool>? AutoAdvance = null, bool UnpauseDuringStep = false);
	private record DeferredTutorialStep(string Message, string ButtonText, Func<bool> Trigger);

	private GameSession _session = null!;
	private TutorialDialog _dialog = null!;
	private Action<bool>? _setPaused;
	private List<TutorialStep> _steps = null!;
	private int _currentStep = -1;
	private bool _waitingForAutoAdvance;
	private int _initialGold;
	private int _startingCreatureCount;
	private bool _mainTutorialComplete;
	private List<DeferredTutorialStep> _deferredSteps = new();

	public void Initialize(GameSession session, TutorialDialog dialog, Action<bool> setPaused)
	{
		_session = session;
		_dialog = dialog;
		_setPaused = setPaused;

		_initialGold = session.Players.Count > 0 ? session.Players[0].Dungeon.Gold.Current : 0;
		_startingCreatureCount = session.Players.Count > 0 ? session.Players[0].Dungeon.OwnedCreatureIds.Count : 0;

		_steps = new List<TutorialStep>
		{
			new(
				"[b]Welcome, Keeper![/b]\n\n" +
				"You are an evil overlord building an underground dungeon. " +
				"The [color=#ff4444]red square[/color] at the center is your [b]Dungeon Heart[/b] -- protect it at all costs!",
                "Next"
			),
			new(
				"[b]Camera Controls[/b]\n\n" +
				"Use [b]WASD[/b] or arrow keys to move the camera.\n" +
				"Use the [b]scroll wheel[/b] to zoom in and out.\n" +
				"Try it now!",
                "Next"
			),
			new(
				"[b]Your Imps[/b]\n\n" +
				"Those [color=#ff9933]orange creatures[/color] are your [b]Imps[/b]. " +
				"They are your workers -- they dig tunnels, mine gold, and claim territory for you automatically.",
                "Next"
			),
			new(
				"[b]Digging[/b]\n\n" +
				"[b]Left-click[/b] on the brown [color=#8B4513]earth tiles[/color] to mark them for digging. " +
				"Your imps will walk over and dig them out.\n\n" +
				"[color=#ffff66]Try it now! Click on an earth tile nearby.[/color]",
				"Waiting...",
				AutoAdvance: () => HasAnyMarkedTile(),
				UnpauseDuringStep: true
			),
			new(
				"[b]Good job![/b]\n\n" +
				"Your imps are on their way to dig! Watch them work.\n" +
				"[color=#FFD700]Gold tiles[/color] yield gold when mined.\n\n" +
				"[color=#ffff66]Wait for the digging to complete...[/color]",
				"Waiting...",
				AutoAdvance: () => HasAnyDugTile(),
				UnpauseDuringStep: true
			),
			new(
				"[b]Gold![/b]\n\n" +
				"Check your [color=#FFD700]gold counter[/color] in the top-left corner. " +
				"Gold is used to build rooms and pay your creatures.\n\n" +
				"[b]Right-click[/b] on a marked tile to unmark it.",
                "Next"
			),
			new(
				"[b]One more thing...[/b]\n\n" +
				"Press [b]P[/b] to pause the game at any time.\n\n" +
				"Now let's build some rooms!",
                "Next"
			),
			new(
				"[b]Building Rooms[/b]\n\n" +
				"Look at the [b]room panel[/b] on the right side of the screen. " +
				"Click a room type, then click on [color=#cccccc]claimed path tiles[/color] to place it.\n\n" +
				"Build all four rooms:\n" +
				"- [b]Lair[/b] — creatures sleep here\n" +
				"- [b]Hatchery[/b] — feeds your creatures\n" +
				"- [b]Treasury[/b] — stores your gold\n" +
				"- [b]Portal[/b] — attracts creatures to your dungeon\n\n" +
				"[color=#ffff66]Build all four rooms to continue.[/color]",
				"Waiting...",
				AutoAdvance: () => HasAllRequiredRooms(),
				UnpauseDuringStep: true
			),
			new(
				"[b]Creatures![/b]\n\n" +
				"Now that you have a [b]Portal[/b], [b]Lair[/b], and [b]Hatchery[/b], " +
				"creatures will start arriving through the portal every 30 seconds.\n\n" +
				"[color=#ffff66]Wait for your first creature to arrive...[/color]",
				"Waiting...",
				AutoAdvance: () => HasAttractedCreature(),
				UnpauseDuringStep: true
			),
			new(
				"[b]Your Goal[/b]\n\n" +
				"Attract [b]4 creatures[/b] (8 total including your imps) to claim this realm.\n\n" +
				"But beware — [color=#4488ff]heroes[/color] will invade your dungeon! " +
				"Your creatures will fight them when they get close. " +
				"Keep building and defend your [color=#ff4444]Dungeon Heart[/color]!\n\n" +
				"Good luck, Keeper!",
                "Start playing!"
			),
		};

		_deferredSteps = new List<DeferredTutorialStep>
		{
			new(
				"[b]Heroes Approaching![/b]\n\n" +
				"A band of [color=#4488ff]heroes[/color] has entered your dungeon! " +
				"They will march toward your [b]Dungeon Heart[/b] and try to destroy it.\n\n" +
				"Your creatures will fight them automatically when they get close. " +
				"Make sure you have enough creatures to defend!",
				"Defend!",
				Trigger: () => IsHeroWaveImminent()
			),
		};

		_dialog.Dismissed += OnDialogDismissed;
		AdvanceStep();
	}

	public override void _Process(double delta)
	{
		if (_mainTutorialComplete)
		{
			if (_deferredSteps.Count > 0)
			{
				var deferred = _deferredSteps[0];
				if (deferred.Trigger())
				{
					_deferredSteps.RemoveAt(0);
					_dialog.ShowMessage(deferred.Message, deferred.ButtonText);
					_setPaused?.Invoke(true);
				}
			}
			return;
		}

		if (_currentStep < 0 || _currentStep >= _steps.Count) return;
		if (!_waitingForAutoAdvance) return;

		var step = _steps[_currentStep];
		if (step.AutoAdvance != null && step.AutoAdvance())
		{
			_waitingForAutoAdvance = false;
			AdvanceStep();
		}
	}

	private void OnDialogDismissed()
	{
		if (_mainTutorialComplete)
		{
			_setPaused?.Invoke(false);
			if (_deferredSteps.Count == 0)
			{
				_dialog.Dismissed -= OnDialogDismissed;
				QueueFree();
			}
			return;
		}
		if (_waitingForAutoAdvance) return; // Don't advance on button click if waiting for condition
		AdvanceStep();
	}

	private void AdvanceStep()
	{
		_currentStep++;

		if (_currentStep >= _steps.Count)
		{
			// Main tutorial complete
			_mainTutorialComplete = true;
			_setPaused?.Invoke(false);
			_dialog.HideDialog();
			GD.Print("Tutorial complete!");

			if (_deferredSteps.Count == 0)
			{
				_dialog.Dismissed -= OnDialogDismissed;
				QueueFree();
			}
			return;
		}

		var step = _steps[_currentStep];
		_dialog.ShowMessage(step.Message, step.ButtonText);

		if (step.AutoAdvance != null)
		{
			_waitingForAutoAdvance = true;
			// Unpause so the player can interact during auto-advance steps
			_setPaused?.Invoke(!step.UnpauseDuringStep);
		}
		else
		{
			_setPaused?.Invoke(true);
		}
	}

	private bool HasAnyMarkedTile()
	{
		for (int x = 0; x < _session.Map.Width; x++)
		{
			for (int y = 0; y < _session.Map.Height; y++)
			{
				var tile = _session.Map.GetTile(new DungeonKeeper.Core.Common.TileCoordinate(x, y));
				if (tile != null && tile.IsMarkedForDigging) return true;
			}
		}
		return false;
	}

	private bool HasAnyDugTile()
	{
		// Check if the number of claimed path tiles increased (a tile was dug)
		if (_session.Players.Count == 0) return false;
		var player = _session.Players[0];

		int claimedCount = 0;
		for (int x = 0; x < _session.Map.Width; x++)
		{
			for (int y = 0; y < _session.Map.Height; y++)
			{
				var tile = _session.Map.GetTile(new DungeonKeeper.Core.Common.TileCoordinate(x, y));
				if (tile != null && tile.Type == TileType.ClaimedPath && tile.OwnerId == player.Id)
					claimedCount++;
			}
		}

		// Initial starting area is 5x5 = 25 claimed tiles (minus 9 room tiles = 16 claimed paths)
		return claimedCount > 16;
	}

	private bool HasAllRequiredRooms()
	{
		if (_session.Players.Count == 0) return false;
		var dungeon = _session.Players[0].Dungeon;
		return dungeon.HasRoom(RoomType.Lair)
			&& dungeon.HasRoom(RoomType.Hatchery)
			&& dungeon.HasRoom(RoomType.Treasury)
			&& dungeon.HasRoom(RoomType.Portal);
	}

	private bool HasAttractedCreature()
	{
		if (_session.Players.Count == 0) return false;
		return _session.Players[0].Dungeon.OwnedCreatureIds.Count > _startingCreatureCount;
	}

	private bool IsHeroWaveImminent()
	{
		if (_session.Players.Count == 0) return false;
		var scheduler = _session.Players[0].Dungeon.InvasionScheduler;
		int currentTick = _session.Clock.CurrentTick;
		var upcoming = scheduler.GetUpcomingWaves(currentTick);
		return upcoming.Count > 0 && upcoming[0].ScheduledTick <= currentTick + 100;
	}
}
