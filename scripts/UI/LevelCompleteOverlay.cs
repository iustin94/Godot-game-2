using DungeonKeeper.Campaign.Conditions;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class LevelCompleteOverlay : Control
{
    [Signal]
    public delegate void ContinueEventHandler();

    private Label _titleLabel = null!;
    private RichTextLabel _messageLabel = null!;
    private Button _button = null!;

    public override void _Ready()
    {
        SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        MouseFilter = MouseFilterEnum.Stop;
        Visible = false;

        // Semi-transparent background
        var bg = new ColorRect
        {
            Color = new Color(0, 0, 0, 0.7f)
        };
        bg.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        AddChild(bg);

        // Center panel
        var panel = new PanelContainer();
        panel.AnchorLeft = 0.25f;
        panel.AnchorRight = 0.75f;
        panel.AnchorTop = 0.25f;
        panel.AnchorBottom = 0.75f;

        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0.05f, 0.02f, 0.1f, 0.95f),
            ContentMarginLeft = 30,
            ContentMarginRight = 30,
            ContentMarginTop = 30,
            ContentMarginBottom = 30,
            CornerRadiusTopLeft = 12,
            CornerRadiusTopRight = 12,
            CornerRadiusBottomLeft = 12,
            CornerRadiusBottomRight = 12,
            BorderWidthTop = 3,
            BorderWidthBottom = 3,
            BorderWidthLeft = 3,
            BorderWidthRight = 3,
        };
        panel.AddThemeStyleboxOverride("panel", styleBox);

        var vbox = new VBoxContainer();
        vbox.AddThemeConstantOverride("separation", 20);
        vbox.Alignment = BoxContainer.AlignmentMode.Center;

        _titleLabel = new Label();
        _titleLabel.AddThemeFontSizeOverride("font_size", 32);
        _titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
        vbox.AddChild(_titleLabel);

        _messageLabel = new RichTextLabel
        {
            BbcodeEnabled = true,
            FitContent = true,
            ScrollActive = false,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
        };
        _messageLabel.AddThemeFontSizeOverride("normal_font_size", 18);
        _messageLabel.AddThemeColorOverride("default_color", new Color(0.85f, 0.82f, 0.9f));
        vbox.AddChild(_messageLabel);

        _button = new Button();
        _button.CustomMinimumSize = new Vector2(180, 45);
        _button.SizeFlagsHorizontal = SizeFlags.ShrinkCenter;
        _button.AddThemeFontSizeOverride("font_size", 18);
        _button.Pressed += () => EmitSignal(SignalName.Continue);
        vbox.AddChild(_button);

        panel.AddChild(vbox);
        AddChild(panel);
    }

    public void ShowOutcome(LevelOutcome outcome, string? debriefingText = null)
    {
        if (outcome == LevelOutcome.Victory)
        {
            _titleLabel.Text = "VICTORY";
            _titleLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.84f, 0.0f));
            _messageLabel.Text = debriefingText ?? "This realm is yours, Keeper.";
            _button.Text = "Continue";

            // Gold border
            var panel = GetChild(1) as PanelContainer;
            if (panel != null)
            {
                var style = panel.GetThemeStylebox("panel") as StyleBoxFlat;
                if (style != null)
                    style.BorderColor = new Color(1.0f, 0.84f, 0.0f, 0.8f);
            }
        }
        else
        {
            _titleLabel.Text = "DEFEAT";
            _titleLabel.AddThemeColorOverride("font_color", new Color(0.8f, 0.2f, 0.2f));
            _messageLabel.Text = "Your dungeon heart has been destroyed.\nThe forces of good have prevailed... for now.";
            _button.Text = "Return";

            var panel = GetChild(1) as PanelContainer;
            if (panel != null)
            {
                var style = panel.GetThemeStylebox("panel") as StyleBoxFlat;
                if (style != null)
                    style.BorderColor = new Color(0.8f, 0.2f, 0.2f, 0.8f);
            }
        }

        Visible = true;
    }
}
