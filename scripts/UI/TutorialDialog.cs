using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class TutorialDialog : Control
{
    [Signal]
    public delegate void DismissedEventHandler();

    private PanelContainer _panel = null!;
    private RichTextLabel _messageLabel = null!;
    private Button _button = null!;

    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Ignore;
        SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);

        _panel = new PanelContainer();
        _panel.SetAnchorsAndOffsetsPreset(LayoutPreset.CenterBottom);
        _panel.GrowHorizontal = GrowDirection.Both;
        _panel.GrowVertical = GrowDirection.Begin;
        _panel.Position = new Vector2(0, 0);
        _panel.CustomMinimumSize = new Vector2(600, 0);

        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0.05f, 0.02f, 0.1f, 0.92f),
            ContentMarginLeft = 24,
            ContentMarginRight = 24,
            ContentMarginTop = 20,
            ContentMarginBottom = 20,
            CornerRadiusTopLeft = 10,
            CornerRadiusTopRight = 10,
            CornerRadiusBottomLeft = 10,
            CornerRadiusBottomRight = 10,
            BorderWidthTop = 2,
            BorderWidthBottom = 2,
            BorderWidthLeft = 2,
            BorderWidthRight = 2,
            BorderColor = new Color(0.6f, 0.4f, 0.8f, 0.8f)
        };
        _panel.AddThemeStyleboxOverride("panel", styleBox);

        var vbox = new VBoxContainer();
        vbox.AddThemeConstantOverride("separation", 12);

        // Title
        var title = new Label { Text = "Dungeon Keeper" };
        title.AddThemeFontSizeOverride("font_size", 14);
        title.AddThemeColorOverride("font_color", new Color(0.6f, 0.4f, 0.8f));
        title.HorizontalAlignment = HorizontalAlignment.Center;
        vbox.AddChild(title);

        // Message
        _messageLabel = new RichTextLabel
        {
            BbcodeEnabled = true,
            FitContent = true,
            ScrollActive = false,
            MouseFilter = MouseFilterEnum.Ignore,
            CustomMinimumSize = new Vector2(550, 0)
        };
        _messageLabel.AddThemeFontSizeOverride("normal_font_size", 16);
        _messageLabel.AddThemeColorOverride("default_color", new Color(0.9f, 0.88f, 0.95f));
        vbox.AddChild(_messageLabel);

        // Button
        _button = new Button { Text = "Got it!" };
        _button.CustomMinimumSize = new Vector2(120, 36);
        _button.SizeFlagsHorizontal = SizeFlags.ShrinkCenter;
        _button.Pressed += OnButtonPressed;
        vbox.AddChild(_button);

        _panel.AddChild(vbox);
        AddChild(_panel);

        // Center the panel at bottom of screen
        _panel.AnchorLeft = 0.5f;
        _panel.AnchorRight = 0.5f;
        _panel.AnchorTop = 1.0f;
        _panel.AnchorBottom = 1.0f;
        _panel.OffsetLeft = -300;
        _panel.OffsetRight = 300;
        _panel.OffsetTop = -200;
        _panel.OffsetBottom = -20;

        Hide();
    }

    public void ShowMessage(string message, string buttonText = "Got it!")
    {
        _messageLabel.Text = message;
        _button.Text = buttonText;
        Visible = true;
        MouseFilter = MouseFilterEnum.Stop;
    }

    public void HideDialog()
    {
        Visible = false;
        MouseFilter = MouseFilterEnum.Ignore;
    }

    private void OnButtonPressed()
    {
        HideDialog();
        EmitSignal(SignalName.Dismissed);
    }
}
