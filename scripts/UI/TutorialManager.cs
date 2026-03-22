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
			// Step 0: Welcome / Dungeon Heart — explicit lose condition
			new(
				"[b]Welcome, Keeper![/b]\n\n" +
				"You are an evil overlord building an underground dungeon. " +
				"The [color=#ff4444]red structure[/color] at the center is your [b]Dungeon Heart[/b].\n\n" +
				"[color=#ff4444]If your Dungeon Heart is destroyed, you lose![/color] Protect it at all costs.",
				"Next"
			),
			// Step 1: Camera Controls
			new(
				"[b]Camera Controls[/b]\n\n" +
				"Use [b]WASD[/b] or arrow keys to move the camera.\n" +
				"Use the [b]scroll wheel[/b] to zoom in and out.\n" +
				"Try it now!",
				"Next"
			),
			// Step 2: Imps
			new(
				"[b]Your Imps[/b]\n\n" +
				"Those [color=#ff9933]orange creatures[/color] are your [b]Imps[/b]. " +
				"They are your workers — they dig tunnels, mine gold, and claim territory for you automatically.",
				"Next"
			),
			// Step 3: Mark for digging (auto-advance)
			new(
				"[b]Digging[/b]\n\n" +
				"[b]Left-click[/b] on the brown [color=#8B4513]earth tiles[/color] to mark them for digging. " +
				"Your imps will walk over and dig them out.\n\n" +
				"[color=#ffff66]Try it now! Click on an earth tile nearby.[/color]",
				"Waiting...",
				AutoAdvance: () => HasAnyMarkedTile(),
				UnpauseDuringStep: true
			),
			// Step 4: Watch digging complete (auto-advance)
			new(
				"[b]Good job![/b]\n\n" +
				"Your imps are on their way to dig! Watch them work.\n" +
				"[color=#FFD700]Gold tiles[/color] yield gold when mined.\n\n" +
				"[color=#ffff66]Wait for the digging to complete...[/color]",
				"Waiting...",
				AutoAdvance: () => HasAnyDugTile(),
				UnpauseDuringStep: true
			),
			// Step 5: Resources & HUD overview
			new(
				"[b]Your Resources[/b]\n\n" +
				"Check the [b]top-left corner[/b] of your screen:\n" +
				"- [color=#FFD700]Gold[/color] — spent to build rooms. Mine gold tiles to earn more.\n" +
				"- [color=#66ccff]Mana[/color] — regenerates over time. Used for spells.\n" +
				"- [color=#cc9944]Creatures[/color] — how many creatures you own.\n" +
				"- Time — the game clock.\n\n" +
				"[b]Right-click[/b] a marked tile to unmark it.",
				"Next"
			),
			// Step 6: Mana & Create Imp (NEW)
			new(
				"[b]Mana & Spells[/b]\n\n" +
				"Your [color=#66ccff]mana[/color] regenerates automatically over time. " +
				"You start with [b]500 mana[/b] and can store up to [b]5000[/b].\n\n" +
				"The [b]Create Imp[/b] spell lets you summon additional workers " +
				"— useful if you need more diggers.\n\n" +
				"For now, your 4 imps are plenty. Let's build some rooms!",
				"Next"
			),
			// Step 7: Pause mechanic
			new(
				"[b]One more thing...[/b]\n\n" +
				"Press [b]P[/b] to pause the game at any time.\n\n" +
				"Now let's build some rooms!",
				"Next"
			),
			// Step 8: Build a Treasury — guided first build (NEW, auto-advance)
			new(
				"[b]Building Your First Room[/b]\n\n" +
				"Let's start with a [b]Treasury[/b] to increase your gold storage.\n\n" +
				"1. Click [b]Treasury[/b] in the room panel (right side)\n" +
				"2. Click on [color=#cccccc]claimed path tiles[/color] (the light tiles near your heart)\n" +
				"Each tile costs [color=#FFD700]50 gold[/color].\n\n" +
				"[color=#ffff66]Build a Treasury to continue.[/color]",
				"Waiting...",
				AutoAdvance: () => HasTreasury(),
				UnpauseDuringStep: true
			),
			// Step 9: Build remaining rooms with costs & sizes (auto-advance)
			new(
				"[b]Build Your Dungeon[/b]\n\n" +
				"Now build the remaining three rooms:\n" +
				"- [b]Lair[/b] ([color=#FFD700]150g/tile[/color]) — creatures sleep here\n" +
				"- [b]Hatchery[/b] ([color=#FFD700]150g/tile[/color], min 3×3) — feeds your creatures\n" +
				"- [b]Portal[/b] ([color=#FFD700]free![/color], min 3×3) — attracts creatures\n\n" +
				"Dig more tunnels if you need space!\n\n" +
				"[color=#ffff66]Build all three rooms to continue.[/color]",
				"Waiting...",
				AutoAdvance: () => HasAllRequiredRooms(),
				UnpauseDuringStep: true
			),
			// Step 10: Creatures arriving (auto-advance)
			new(
				"[b]Creatures Incoming![/b]\n\n" +
				"Now that you have a [b]Portal[/b], [b]Lair[/b], and [b]Hatchery[/b], " +
				"creatures will arrive through the portal every [b]30 seconds[/b].\n\n" +
				"Watch the [color=#cc9944]creature counter[/color] in the top-left.\n\n" +
				"[color=#ffff66]Wait for your first creature to arrive...[/color]",
				"Waiting...",
				AutoAdvance: () => HasAttractedCreature(),
				UnpauseDuringStep: true
			),
			// Step 11: Heroes & Defense (NEW)
			new(
				"[b]Beware of Heroes![/b]\n\n" +
				"[color=#4488ff]Heroes[/color] will invade your dungeon in escalating waves:\n" +
				"- [b]Wave 1[/b] at ~[b]5 minutes[/b] — 2 Knights\n" +
				"- Later waves are larger and stronger\n\n" +
				"Heroes march toward your [color=#ff4444]Dungeon Heart[/color] " +
				"and attack anything in their path.\n\n" +
				"Your creatures [b]fight automatically[/b] when heroes get close. " +
				"The more creatures you attract, the better your defense!",
				"Next"
			),
			// Step 12: Win/Lose conditions
			new(
				"[b]Your Goal[/b]\n\n" +
				"Attract creatures until you have [b]8 total[/b] " +
				"(you start with 4 imps — attract [b]4 more[/b] through the portal).\n\n" +
				"[color=#ff4444]Lose:[/color] Your Dungeon Heart is destroyed.\n" +
				"[color=#00ff66]Win:[/color] Own 8 creatures.\n\n" +
				"Good luck, Keeper!",
				"Start playing!"
			),
		};

		_deferredSteps = new List<DeferredTutorialStep>
		{
			new(
				"[b]Heroes Approaching![/b]\n\n" +
				"A band of [color=#4488ff]heroes[/color] will arrive soon! " +
				"They will march toward your [b]Dungeon Heart[/b].\n\n" +
				"Your creatures will fight them automatically when close. " +
				"Make sure you have enough creatures to defend!\n\n" +
				"[color=#ff4444]Defend your heart![/color]",
				"Defend!",
				Trigger: () => IsHeroWaveImminent()
			),
			new(
				"[b]Wave Survived![/b]\n\n" +
				"Well done, Keeper! But more heroes will come.\n\n" +
				"Each wave is [b]stronger[/b] than the last. Keep attracting creatures " +
				"and expand your dungeon to stay ahead.\n\n" +
				"The next wave arrives around the [b]10-minute[/b] mark.",
				"Understood!",
				Trigger: () => HasPassedFirstWaveWindow()
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

	private bool HasTreasury()
	{
		if (_session.Players.Count == 0) return false;
		return _session.Players[0].Dungeon.HasRoom(RoomType.Treasury);
	}

	private bool IsHeroWaveImminent()
	{
		if (_session.Players.Count == 0) return false;
		var scheduler = _session.Players[0].Dungeon.InvasionScheduler;
		int currentTick = _session.Clock.CurrentTick;
		var upcoming = scheduler.GetUpcomingWaves(currentTick);
		return upcoming.Count > 0 && upcoming[0].ScheduledTick <= currentTick + 500;
	}

	private bool HasPassedFirstWaveWindow()
	{
		// Trigger ~50 seconds after wave 1 (tick 3000) as a "you survived" checkpoint
		return _session.Clock.CurrentTick >= 3500;
	}
}
