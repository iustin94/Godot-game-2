using DungeonKeeper.Campaign;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class LevelSelectScreen : Control
{
    [Signal]
    public delegate void LevelSelectedEventHandler(int levelNumber);

    private CampaignDefinition _campaign = null!;

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

        // Center container
        var centerContainer = new VBoxContainer();
        centerContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.Center);
        centerContainer.GrowHorizontal = GrowDirection.Both;
        centerContainer.GrowVertical = GrowDirection.Both;
        centerContainer.AnchorLeft = 0.5f;
        centerContainer.AnchorRight = 0.5f;
        centerContainer.AnchorTop = 0.1f;
        centerContainer.AnchorBottom = 0.9f;
        centerContainer.OffsetLeft = -300;
        centerContainer.OffsetRight = 300;
        centerContainer.AddThemeConstantOverride("separation", 16);

        // Title
        var title = new Label { Text = "DUNGEON KEEPER" };
        title.AddThemeFontSizeOverride("font_size", 36);
        title.AddThemeColorOverride("font_color", new Color(0.8f, 0.3f, 0.3f));
        title.HorizontalAlignment = HorizontalAlignment.Center;
        centerContainer.AddChild(title);

        var subtitle = new Label { Text = "Select Your Realm" };
        subtitle.AddThemeFontSizeOverride("font_size", 18);
        subtitle.AddThemeColorOverride("font_color", new Color(0.6f, 0.5f, 0.7f));
        subtitle.HorizontalAlignment = HorizontalAlignment.Center;
        centerContainer.AddChild(subtitle);

        // Spacer
        var spacer = new Control { CustomMinimumSize = new Vector2(0, 10) };
        centerContainer.AddChild(spacer);

        AddChild(centerContainer);
    }

    public void SetCampaign(CampaignDefinition campaign)
    {
        _campaign = campaign;
        BuildLevelButtons();
    }

    private void BuildLevelButtons()
    {
        // Find the center container (3rd child — bg, then centerContainer)
        var centerContainer = GetChild(1) as VBoxContainer;
        if (centerContainer == null) return;

        // Remove old level buttons (keep title, subtitle, spacer)
        while (centerContainer.GetChildCount() > 3)
        {
            var child = centerContainer.GetChild(3);
            centerContainer.RemoveChild(child);
            child.QueueFree();
        }

        // Scroll container for level list
        var scroll = new ScrollContainer
        {
            CustomMinimumSize = new Vector2(580, 400),
            SizeFlagsVertical = SizeFlags.ExpandFill
        };

        var levelList = new VBoxContainer();
        levelList.AddThemeConstantOverride("separation", 8);
        levelList.SizeFlagsHorizontal = SizeFlags.ExpandFill;

        foreach (var level in _campaign.Levels)
        {
            var btn = CreateLevelButton(level);
            levelList.AddChild(btn);
        }

        scroll.AddChild(levelList);
        centerContainer.AddChild(scroll);
    }

    private Button CreateLevelButton(LevelDefinition level)
    {
        var btn = new Button
        {
            Text = $"Level {level.LevelNumber}: {level.Name}",
            CustomMinimumSize = new Vector2(560, 50),
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };

        var styleNormal = new StyleBoxFlat
        {
            BgColor = new Color(0.1f, 0.08f, 0.15f, 0.9f),
            ContentMarginLeft = 16,
            ContentMarginRight = 16,
            ContentMarginTop = 8,
            ContentMarginBottom = 8,
            CornerRadiusTopLeft = 6,
            CornerRadiusTopRight = 6,
            CornerRadiusBottomLeft = 6,
            CornerRadiusBottomRight = 6,
            BorderWidthTop = 1,
            BorderWidthBottom = 1,
            BorderWidthLeft = 1,
            BorderWidthRight = 1,
            BorderColor = new Color(0.4f, 0.3f, 0.5f, 0.6f)
        };
        btn.AddThemeStyleboxOverride("normal", styleNormal);

        var styleHover = (StyleBoxFlat)styleNormal.Duplicate();
        styleHover.BgColor = new Color(0.15f, 0.1f, 0.25f, 0.95f);
        styleHover.BorderColor = new Color(0.6f, 0.4f, 0.8f, 0.9f);
        btn.AddThemeStyleboxOverride("hover", styleHover);

        var stylePressed = (StyleBoxFlat)styleNormal.Duplicate();
        stylePressed.BgColor = new Color(0.2f, 0.12f, 0.3f, 1.0f);
        btn.AddThemeStyleboxOverride("pressed", stylePressed);

        btn.AddThemeFontSizeOverride("font_size", 18);
        btn.AddThemeColorOverride("font_color", new Color(0.9f, 0.85f, 0.95f));
        btn.AddThemeColorOverride("font_hover_color", new Color(1.0f, 0.9f, 1.0f));

        var levelNum = level.LevelNumber;
        btn.Pressed += () => EmitSignal(SignalName.LevelSelected, levelNum);

        return btn;
    }
}
