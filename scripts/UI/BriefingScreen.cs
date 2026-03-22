using DungeonKeeper.Campaign;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class BriefingScreen : Control
{
    [Signal]
    public delegate void StartLevelEventHandler();

    private RichTextLabel _briefingLabel = null!;
    private Label _titleLabel = null!;

    public override void _Ready()
    {
        SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        MouseFilter = MouseFilterEnum.Stop;

        // Background
        var bg = new ColorRect
        {
            Color = new Color(0.02f, 0.01f, 0.05f, 1.0f)
        };
        bg.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        AddChild(bg);

        // Center panel
        var panel = new PanelContainer();
        panel.AnchorLeft = 0.15f;
        panel.AnchorRight = 0.85f;
        panel.AnchorTop = 0.1f;
        panel.AnchorBottom = 0.9f;

        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0.06f, 0.03f, 0.1f, 0.95f),
            ContentMarginLeft = 40,
            ContentMarginRight = 40,
            ContentMarginTop = 30,
            ContentMarginBottom = 30,
            CornerRadiusTopLeft = 12,
            CornerRadiusTopRight = 12,
            CornerRadiusBottomLeft = 12,
            CornerRadiusBottomRight = 12,
            BorderWidthTop = 2,
            BorderWidthBottom = 2,
            BorderWidthLeft = 2,
            BorderWidthRight = 2,
            BorderColor = new Color(0.5f, 0.3f, 0.6f, 0.8f)
        };
        panel.AddThemeStyleboxOverride("panel", styleBox);

        var vbox = new VBoxContainer();
        vbox.AddThemeConstantOverride("separation", 20);

        // Level title
        _titleLabel = new Label { Text = "Level" };
        _titleLabel.AddThemeFontSizeOverride("font_size", 28);
        _titleLabel.AddThemeColorOverride("font_color", new Color(0.8f, 0.3f, 0.3f));
        _titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
        vbox.AddChild(_titleLabel);

        // Briefing text
        _briefingLabel = new RichTextLabel
        {
            BbcodeEnabled = true,
            FitContent = true,
            ScrollActive = true,
            SizeFlagsVertical = SizeFlags.ExpandFill,
        };
        _briefingLabel.AddThemeFontSizeOverride("normal_font_size", 18);
        _briefingLabel.AddThemeColorOverride("default_color", new Color(0.85f, 0.82f, 0.9f));
        vbox.AddChild(_briefingLabel);

        // Start button
        var startBtn = new Button { Text = "Begin Conquest" };
        startBtn.CustomMinimumSize = new Vector2(200, 50);
        startBtn.SizeFlagsHorizontal = SizeFlags.ShrinkCenter;
        startBtn.AddThemeFontSizeOverride("font_size", 20);

        var btnStyle = new StyleBoxFlat
        {
            BgColor = new Color(0.5f, 0.15f, 0.15f, 0.9f),
            ContentMarginLeft = 20,
            ContentMarginRight = 20,
            ContentMarginTop = 10,
            ContentMarginBottom = 10,
            CornerRadiusTopLeft = 8,
            CornerRadiusTopRight = 8,
            CornerRadiusBottomLeft = 8,
            CornerRadiusBottomRight = 8,
        };
        startBtn.AddThemeStyleboxOverride("normal", btnStyle);

        var btnHover = (StyleBoxFlat)btnStyle.Duplicate();
        btnHover.BgColor = new Color(0.65f, 0.2f, 0.2f, 0.95f);
        startBtn.AddThemeStyleboxOverride("hover", btnHover);

        startBtn.Pressed += () => EmitSignal(SignalName.StartLevel);
        vbox.AddChild(startBtn);

        panel.AddChild(vbox);
        AddChild(panel);
    }

    public void SetLevel(LevelDefinition level)
    {
        _titleLabel.Text = $"Level {level.LevelNumber}: {level.Name}";
        _briefingLabel.Text = level.BriefingText;
    }
}
