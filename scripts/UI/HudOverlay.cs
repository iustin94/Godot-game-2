using DungeonKeeper.GameState;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class HudOverlay : Control
{
    private Label _goldLabel = null!;
    private Label _manaLabel = null!;
    private Label _timeLabel = null!;
    private Label _creatureLabel = null!;
    private GameSession? _session;

    public void Initialize(GameSession session)
    {
        _session = session;
    }

    public override void _Ready()
    {
        // Create a dark background panel
        var panel = new PanelContainer();
        panel.SetAnchorsAndOffsetsPreset(LayoutPreset.TopLeft);
        panel.Position = new Vector2(10, 10);

        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0, 0, 0, 0.7f),
            ContentMarginLeft = 12,
            ContentMarginRight = 12,
            ContentMarginTop = 8,
            ContentMarginBottom = 8,
            CornerRadiusTopLeft = 6,
            CornerRadiusTopRight = 6,
            CornerRadiusBottomLeft = 6,
            CornerRadiusBottomRight = 6
        };
        panel.AddThemeStyleboxOverride("panel", styleBox);

        var vbox = new VBoxContainer();
        vbox.AddThemeConstantOverride("separation", 4);

        _goldLabel = CreateLabel(new Color(1.0f, 0.843f, 0.0f));
        _manaLabel = CreateLabel(new Color(0.4f, 0.8f, 1.0f));
        _timeLabel = CreateLabel(new Color(0.9f, 0.9f, 0.9f));
        _creatureLabel = CreateLabel(new Color(0.8f, 0.6f, 0.2f));

        vbox.AddChild(_goldLabel);
        vbox.AddChild(_manaLabel);
        vbox.AddChild(_timeLabel);
        vbox.AddChild(_creatureLabel);

        panel.AddChild(vbox);
        AddChild(panel);
    }

    public override void _Process(double delta)
    {
        if (_session == null || _session.Players.Count == 0) return;

        var player = _session.Players[0];
        var dungeon = player.Dungeon;

        _goldLabel.Text = $"Gold: {dungeon.Gold.Current} / {dungeon.Gold.Capacity}";
        _manaLabel.Text = $"Mana: {dungeon.Mana.Current} / {dungeon.Mana.Capacity} (net: {dungeon.Mana.NetRate:+0.0;-0.0}/s)";
        _timeLabel.Text = $"Tick: {_session.Clock.CurrentTick} | Time: {_session.Clock.TotalElapsedSeconds:F1}s";
        _creatureLabel.Text = $"Creatures: {dungeon.OwnedCreatureIds.Count}";
    }

    private static Label CreateLabel(Color color)
    {
        var label = new Label();
        label.AddThemeFontSizeOverride("font_size", 16);
        label.AddThemeColorOverride("font_color", color);
        return label;
    }
}
