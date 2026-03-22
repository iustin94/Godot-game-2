using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;
using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Scripts.Presenters;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.UI;

public partial class CreatureViewerDialog : Control
{
    public event Action? Closed;

    private Node3D _creaturePivot = null!;
    private bool _dragging;
    private float _rotationY;

    public void Initialize(IEntity entity, CreatureDefinition? definition)
    {
        MouseFilter = MouseFilterEnum.Stop;
        SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);

        // Semi-transparent background overlay
        var bg = new ColorRect
        {
            Color = new Color(0, 0, 0, 0.6f),
            MouseFilter = MouseFilterEnum.Stop
        };
        bg.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        AddChild(bg);

        // Main panel centered on screen
        var panel = new PanelContainer();
        panel.SetAnchorsAndOffsetsPreset(LayoutPreset.Center);
        panel.GrowHorizontal = GrowDirection.Both;
        panel.GrowVertical = GrowDirection.Both;
        panel.CustomMinimumSize = new Vector2(700, 460);

        var style = new StyleBoxFlat
        {
            BgColor = new Color(0.05f, 0.02f, 0.1f, 0.95f),
            ContentMarginLeft = 20,
            ContentMarginRight = 20,
            ContentMarginTop = 16,
            ContentMarginBottom = 16,
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
        panel.AddThemeStyleboxOverride("panel", style);

        // Center the panel
        panel.AnchorLeft = 0.5f;
        panel.AnchorRight = 0.5f;
        panel.AnchorTop = 0.5f;
        panel.AnchorBottom = 0.5f;
        panel.OffsetLeft = -350;
        panel.OffsetRight = 350;
        panel.OffsetTop = -230;
        panel.OffsetBottom = 230;

        var outerVBox = new VBoxContainer();
        outerVBox.AddThemeConstantOverride("separation", 8);

        // Close button row
        var closeRow = new HBoxContainer();
        closeRow.Alignment = BoxContainer.AlignmentMode.End;
        var closeBtn = new Button { Text = "X" };
        closeBtn.CustomMinimumSize = new Vector2(32, 32);
        closeBtn.Pressed += OnClose;
        closeRow.AddChild(closeBtn);
        outerVBox.AddChild(closeRow);

        // Main content: two columns
        var hbox = new HBoxContainer();
        hbox.AddThemeConstantOverride("separation", 16);
        hbox.SizeFlagsVertical = SizeFlags.ExpandFill;

        // Left column: 3D viewport
        var viewportPanel = Create3DViewport(entity);
        hbox.AddChild(viewportPanel);

        // Right column: stats panel
        var statsPanel = CreateStatsPanel(entity, definition);
        hbox.AddChild(statsPanel);

        outerVBox.AddChild(hbox);
        panel.AddChild(outerVBox);
        AddChild(panel);
    }

    private Control Create3DViewport(IEntity entity)
    {
        var container = new SubViewportContainer
        {
            CustomMinimumSize = new Vector2(260, 380),
            SizeFlagsHorizontal = SizeFlags.Fill,
            SizeFlagsVertical = SizeFlags.ExpandFill,
            Stretch = true
        };

        var viewport = new SubViewport
        {
            Size = new Vector2I(260, 380),
            RenderTargetUpdateMode = SubViewport.UpdateMode.Always,
            TransparentBg = true,
            Msaa3D = SubViewport.Msaa.Msaa4X,
            OwnWorld3D = true
        };

        // Set up 3D scene inside viewport
        var scene = new Node3D();

        // Set up an environment with transparent background
        var env = new Godot.Environment();
        env.BackgroundMode = Godot.Environment.BGMode.Color;
        env.BackgroundColor = new Color(0, 0, 0, 0);
        env.AmbientLightSource = Godot.Environment.AmbientSource.Color;
        env.AmbientLightColor = new Color(0.3f, 0.3f, 0.35f);
        env.AmbientLightEnergy = 0.5f;

        var worldEnv = new WorldEnvironment();
        worldEnv.Environment = env;
        scene.AddChild(worldEnv);

        // Camera
        var camera = new Camera3D();
        camera.Position = new Vector3(0, 0.3f, 2.0f);
        camera.LookAt(new Vector3(0, 0.1f, 0));
        camera.Fov = 40;
        scene.AddChild(camera);

        // Lighting
        var light = new DirectionalLight3D();
        light.Position = new Vector3(2, 3, 2);
        light.LookAt(Vector3.Zero);
        light.ShadowEnabled = false;
        scene.AddChild(light);

        var fillLight = new DirectionalLight3D();
        fillLight.Position = new Vector3(-2, 1, -1);
        fillLight.LookAt(Vector3.Zero);
        fillLight.LightEnergy = 0.4f;
        scene.AddChild(fillLight);

        // Creature model on a pivot for rotation
        _creaturePivot = new Node3D();

        var identity = entity.TryGetComponent<CreatureIdentityComponent>();
        var typeName = identity?.CreatureType.ToString() ?? "Unknown";
        var color = GodotCreaturePresenter.CreatureColors.GetValueOrDefault(typeName, new Color(0.5f, 0.5f, 0.5f));

        // Create the creature capsule, scaled up for viewing
        var mesh = PrimitiveMeshFactory.CreateCreatureCapsule(color, 0.8f, 0.3f);
        mesh.Position = Vector3.Zero;
        _creaturePivot.AddChild(mesh);

        scene.AddChild(_creaturePivot);
        viewport.AddChild(scene);
        container.AddChild(viewport);

        // Handle mouse input for rotation
        container.GuiInput += (InputEvent @event) =>
        {
            if (@event is InputEventMouseButton mouseBtn)
            {
                if (mouseBtn.ButtonIndex == MouseButton.Left)
                    _dragging = mouseBtn.Pressed;
            }
            else if (@event is InputEventMouseMotion motion && _dragging)
            {
                _rotationY += motion.Relative.X * 0.01f;
                _creaturePivot.Rotation = new Vector3(0, _rotationY, 0);
            }
        };

        // Wrap in a styled panel
        var wrapper = new PanelContainer();
        var wrapperStyle = new StyleBoxFlat
        {
            BgColor = new Color(0.08f, 0.05f, 0.12f, 0.8f),
            CornerRadiusTopLeft = 6,
            CornerRadiusTopRight = 6,
            CornerRadiusBottomLeft = 6,
            CornerRadiusBottomRight = 6,
            ContentMarginLeft = 4,
            ContentMarginRight = 4,
            ContentMarginTop = 4,
            ContentMarginBottom = 4
        };
        wrapper.AddThemeStyleboxOverride("panel", wrapperStyle);
        wrapper.CustomMinimumSize = new Vector2(268, 388);
        wrapper.AddChild(container);

        // Add drag hint label
        var vbox = new VBoxContainer();
        vbox.AddChild(wrapper);

        var hint = new Label { Text = "Drag to rotate" };
        hint.AddThemeFontSizeOverride("font_size", 11);
        hint.AddThemeColorOverride("font_color", new Color(0.5f, 0.5f, 0.6f));
        hint.HorizontalAlignment = HorizontalAlignment.Center;
        vbox.AddChild(hint);

        return vbox;
    }

    private Control CreateStatsPanel(IEntity entity, CreatureDefinition? definition)
    {
        var scroll = new ScrollContainer();
        scroll.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        scroll.SizeFlagsVertical = SizeFlags.ExpandFill;

        var vbox = new VBoxContainer();
        vbox.AddThemeConstantOverride("separation", 8);
        vbox.SizeFlagsHorizontal = SizeFlags.ExpandFill;

        var identity = entity.TryGetComponent<CreatureIdentityComponent>();
        var stats = entity.TryGetComponent<StatsComponent>();

        // Creature name
        var nameLabel = new Label
        {
            Text = definition?.Name ?? identity?.CreatureType.ToString() ?? "Unknown Creature"
        };
        nameLabel.AddThemeFontSizeOverride("font_size", 22);
        nameLabel.AddThemeColorOverride("font_color", new Color(0.9f, 0.8f, 1.0f));
        vbox.AddChild(nameLabel);

        // Faction
        var factionColor = identity?.Faction == CreatureFaction.Hero
            ? new Color(0.4f, 0.6f, 1.0f)
            : new Color(1.0f, 0.5f, 0.3f);
        var factionLabel = new Label
        {
            Text = identity?.Faction == CreatureFaction.Hero ? "Hero" : "Keeper Creature"
        };
        factionLabel.AddThemeFontSizeOverride("font_size", 13);
        factionLabel.AddThemeColorOverride("font_color", factionColor);
        vbox.AddChild(factionLabel);

        // Separator
        vbox.AddChild(new HSeparator());

        // Stats grid
        if (stats != null)
        {
            var levelLabel = new Label { Text = $"Level {stats.Level}" };
            levelLabel.AddThemeFontSizeOverride("font_size", 14);
            levelLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.4f));
            vbox.AddChild(levelLabel);

            var grid = new GridContainer { Columns = 4 };
            grid.AddThemeConstantOverride("h_separation", 12);
            grid.AddThemeConstantOverride("v_separation", 4);

            AddStatRow(grid, "Health", $"{stats.CurrentHealth}/{stats.MaxHealth}", new Color(0.4f, 1.0f, 0.4f));
            AddStatRow(grid, "Attack", $"{stats.MeleeAttack}", new Color(1.0f, 0.6f, 0.3f));
            AddStatRow(grid, "Damage", $"{stats.MeleeDamage}", new Color(1.0f, 0.5f, 0.5f));
            AddStatRow(grid, "Defense", $"{stats.Defense}", new Color(0.5f, 0.7f, 1.0f));
            AddStatRow(grid, "Armor", $"{stats.Armor}", new Color(0.7f, 0.7f, 0.8f));
            AddStatRow(grid, "Speed", $"{stats.Speed:F1}", new Color(0.4f, 1.0f, 1.0f));
            if (stats.Luck > 0)
                AddStatRow(grid, "Luck", $"{stats.Luck}", new Color(1.0f, 1.0f, 0.5f));

            vbox.AddChild(grid);
        }

        // Definition-based info
        if (definition != null)
        {
            vbox.AddChild(new HSeparator());

            // Abilities
            if (definition.AbilitiesByLevel.Count > 0)
            {
                var abilitiesTitle = new Label { Text = "Abilities" };
                abilitiesTitle.AddThemeFontSizeOverride("font_size", 14);
                abilitiesTitle.AddThemeColorOverride("font_color", new Color(0.8f, 0.6f, 1.0f));
                vbox.AddChild(abilitiesTitle);

                foreach (var kvp in definition.AbilitiesByLevel)
                {
                    foreach (var ability in kvp.Value)
                    {
                        var abilityLabel = new Label
                        {
                            Text = $"  Lv{kvp.Key}: {FormatAbilityName(ability)}"
                        };
                        abilityLabel.AddThemeFontSizeOverride("font_size", 12);
                        abilityLabel.AddThemeColorOverride("font_color", new Color(0.7f, 0.7f, 0.8f));
                        vbox.AddChild(abilityLabel);
                    }
                }
            }

            // Traits
            var traits = new List<string>();
            if (definition.CanFly) traits.Add("Flying");
            if (definition.IsUndead) traits.Add("Undead");
            if (definition.ImmuneToPoison) traits.Add("Poison Immune");
            if (definition.IsElite) traits.Add("Elite");

            if (traits.Count > 0)
            {
                var traitsLabel = new Label { Text = $"Traits: {string.Join(", ", traits)}" };
                traitsLabel.AddThemeFontSizeOverride("font_size", 12);
                traitsLabel.AddThemeColorOverride("font_color", new Color(0.6f, 0.8f, 0.6f));
                vbox.AddChild(traitsLabel);
            }

            // Description
            if (!string.IsNullOrEmpty(definition.Description))
            {
                vbox.AddChild(new HSeparator());
                var descLabel = new RichTextLabel
                {
                    BbcodeEnabled = true,
                    FitContent = true,
                    ScrollActive = false,
                    MouseFilter = MouseFilterEnum.Ignore,
                    Text = $"[i]{definition.Description}[/i]"
                };
                descLabel.AddThemeFontSizeOverride("normal_font_size", 13);
                descLabel.AddThemeColorOverride("default_color", new Color(0.7f, 0.7f, 0.75f));
                vbox.AddChild(descLabel);
            }
        }

        scroll.AddChild(vbox);
        return scroll;
    }

    private static void AddStatRow(GridContainer grid, string label, string value, Color valueColor)
    {
        var nameLabel = new Label { Text = label };
        nameLabel.AddThemeFontSizeOverride("font_size", 13);
        nameLabel.AddThemeColorOverride("font_color", new Color(0.6f, 0.6f, 0.65f));
        grid.AddChild(nameLabel);

        var valLabel = new Label { Text = value };
        valLabel.AddThemeFontSizeOverride("font_size", 13);
        valLabel.AddThemeColorOverride("font_color", valueColor);
        grid.AddChild(valLabel);
    }

    private static string FormatAbilityName(string abilityId)
    {
        return abilityId.Replace("_", " ");
    }

    private void OnClose()
    {
        Closed?.Invoke();
        QueueFree();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Pressed && key.Keycode == Key.Escape)
        {
            OnClose();
            GetViewport().SetInputAsHandled();
        }
    }
}
