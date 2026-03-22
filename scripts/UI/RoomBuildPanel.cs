using DungeonKeeper.Dungeon.Rooms;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class RoomBuildPanel : Control
{
    [Signal]
    public delegate void RoomSelectedEventHandler(int roomTypeIndex);

    [Signal]
    public delegate void BuildCancelledEventHandler();

    private VBoxContainer _buttonList = null!;
    private Label _titleLabel = null!;
    private RoomType? _selectedRoom;
    private readonly Dictionary<RoomType, Button> _roomButtons = new();

    public RoomType? SelectedRoom => _selectedRoom;

    public override void _Ready()
    {
        SetAnchorsAndOffsetsPreset(LayoutPreset.RightWide);
        AnchorLeft = 1.0f;
        AnchorRight = 1.0f;
        OffsetLeft = -220;
        OffsetRight = -10;
        AnchorTop = 0.0f;
        AnchorBottom = 1.0f;
        OffsetTop = 10;
        OffsetBottom = -10;
        MouseFilter = MouseFilterEnum.Ignore;

        var panel = new PanelContainer();
        panel.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0, 0, 0, 0.75f),
            ContentMarginLeft = 10,
            ContentMarginRight = 10,
            ContentMarginTop = 10,
            ContentMarginBottom = 10,
            CornerRadiusTopLeft = 6,
            CornerRadiusTopRight = 6,
            CornerRadiusBottomLeft = 6,
            CornerRadiusBottomRight = 6,
        };
        panel.AddThemeStyleboxOverride("panel", styleBox);
        panel.MouseFilter = MouseFilterEnum.Stop;

        var scrollContainer = new ScrollContainer();
        scrollContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);

        var vbox = new VBoxContainer();
        vbox.AddThemeConstantOverride("separation", 6);
        vbox.SizeFlagsHorizontal = SizeFlags.ExpandFill;

        _titleLabel = new Label { Text = "BUILD ROOM" };
        _titleLabel.AddThemeFontSizeOverride("font_size", 16);
        _titleLabel.AddThemeColorOverride("font_color", new Color(0.8f, 0.6f, 0.2f));
        _titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
        vbox.AddChild(_titleLabel);

        var separator = new HSeparator();
        vbox.AddChild(separator);

        _buttonList = new VBoxContainer();
        _buttonList.AddThemeConstantOverride("separation", 4);
        vbox.AddChild(_buttonList);

        // Cancel button at bottom
        var cancelBtn = new Button { Text = "Cancel (Esc)" };
        cancelBtn.CustomMinimumSize = new Vector2(0, 32);
        cancelBtn.AddThemeFontSizeOverride("font_size", 13);
        cancelBtn.Pressed += () =>
        {
            _selectedRoom = null;
            UpdateSelection();
            EmitSignal(SignalName.BuildCancelled);
        };
        vbox.AddChild(cancelBtn);

        scrollContainer.AddChild(vbox);
        panel.AddChild(scrollContainer);
        AddChild(panel);
    }

    public void SetAvailableRooms(IReadOnlyList<RoomDefinition> rooms)
    {
        // Clear existing buttons
        foreach (var child in _buttonList.GetChildren())
        {
            child.QueueFree();
        }
        _roomButtons.Clear();

        foreach (var roomDef in rooms)
        {
            // Skip DungeonHeart — can't be built by player
            if (roomDef.Type == RoomType.DungeonHeart) continue;

            var btn = new Button
            {
                Text = $"{roomDef.Name}\n{roomDef.GoldCostPerTile}g/tile",
                CustomMinimumSize = new Vector2(0, 44),
                ToggleMode = true,
            };
            btn.AddThemeFontSizeOverride("font_size", 13);

            var roomType = roomDef.Type;
            btn.Toggled += (pressed) =>
            {
                if (pressed)
                {
                    _selectedRoom = roomType;
                    UpdateSelection();
                    EmitSignal(SignalName.RoomSelected, (int)roomType);
                }
                else if (_selectedRoom == roomType)
                {
                    _selectedRoom = null;
                    UpdateSelection();
                }
            };

            _roomButtons[roomDef.Type] = btn;
            _buttonList.AddChild(btn);
        }
    }

    private void UpdateSelection()
    {
        foreach (var (type, btn) in _roomButtons)
        {
            btn.SetPressedNoSignal(type == _selectedRoom);
        }
    }

    public void ClearSelection()
    {
        _selectedRoom = null;
        UpdateSelection();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Pressed && key.Keycode == Key.Escape)
        {
            if (_selectedRoom != null)
            {
                _selectedRoom = null;
                UpdateSelection();
                EmitSignal(SignalName.BuildCancelled);
                GetViewport().SetInputAsHandled();
            }
        }
    }
}
