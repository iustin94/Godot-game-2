using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotCreaturePresenter : ICreaturePresenter
{
    private readonly Node3D _creaturesRoot;
    private readonly string _assetId;
    private readonly EntityId _entityId;
    private readonly Dictionary<EntityId, MeshInstance3D> _creatureNodes;
    private readonly Color _creatureColor;

    private static readonly Dictionary<string, Color> CreatureColors = new()
    {
        // Keeper creatures
        { "Imp", new Color(1.0f, 0.6f, 0.2f) },
        { "Goblin", new Color(0.4f, 0.7f, 0.2f) },
        { "Warlock", new Color(0.5f, 0.2f, 0.8f) },
        { "Troll", new Color(0.3f, 0.6f, 0.3f) },
        { "DarkElf", new Color(0.4f, 0.3f, 0.5f) },
        { "Salamander", new Color(1.0f, 0.4f, 0.1f) },
        { "BileDemon", new Color(0.6f, 0.5f, 0.1f) },
        { "Vampire", new Color(0.3f, 0.0f, 0.0f) },
        { "DarkMistress", new Color(0.6f, 0.0f, 0.3f) },
        { "Rogue", new Color(0.4f, 0.4f, 0.4f) },
        { "BlackKnight", new Color(0.15f, 0.15f, 0.2f) },
        { "Dragon", new Color(0.9f, 0.1f, 0.1f) },
        { "Firefly", new Color(1.0f, 1.0f, 0.3f) },
        { "Skeleton", new Color(0.85f, 0.85f, 0.8f) },
        { "Horny", new Color(0.7f, 0.0f, 0.0f) },
        // Hero creatures
        { "Knight", new Color(0.75f, 0.75f, 0.8f) },
        { "Wizard", new Color(0.2f, 0.3f, 0.9f) },
        { "Giant", new Color(0.6f, 0.5f, 0.4f) },
        { "Samurai", new Color(0.8f, 0.2f, 0.2f) },
        { "Guard", new Color(0.5f, 0.5f, 0.6f) },
        { "Monk", new Color(0.9f, 0.8f, 0.3f) },
        { "Thief", new Color(0.3f, 0.3f, 0.3f) },
        { "Fairy", new Color(0.8f, 0.9f, 1.0f) },
        { "ElvenArcher", new Color(0.2f, 0.7f, 0.3f) },
        { "Dwarf", new Color(0.7f, 0.5f, 0.3f) },
        { "RoyalGuard", new Color(0.9f, 0.85f, 0.1f) },
        { "Avatar", new Color(1.0f, 1.0f, 1.0f) },
    };

    public GodotCreaturePresenter(Node3D creaturesRoot, EntityId entityId, string assetId,
        Dictionary<EntityId, MeshInstance3D> sharedNodeMap)
    {
        _creaturesRoot = creaturesRoot;
        _entityId = entityId;
        _assetId = assetId;
        _creatureNodes = sharedNodeMap;
        _creatureColor = CreatureColors.GetValueOrDefault(assetId, new Color(0.5f, 0.5f, 0.5f));
    }

    public void OnSpawned(EntityId id, TileCoordinate position)
    {
        var mesh = PrimitiveMeshFactory.CreateCreatureCapsule(_creatureColor);
        mesh.Position = CoordinateHelper.TileToWorld(position, 0.35f);
        _creaturesRoot.AddChild(mesh);
        _creatureNodes[id] = mesh;
    }

    public void OnMoved(EntityId id, TileCoordinate from, TileCoordinate to)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        var targetPos = CoordinateHelper.TileToWorld(to, 0.35f);
        var tween = mesh.CreateTween();
        tween.TweenProperty(mesh, "position", targetPos, 0.1);
    }

    public void OnDestroyed(EntityId id)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;
        mesh.QueueFree();
        _creatureNodes.Remove(id);
    }

    public void OnStateChanged(EntityId id, string stateName)
    {
        // Visual state indicator could be added later (label, color tint, etc.)
        GD.Print($"Creature {_assetId} [{id}] state -> {stateName}");
    }

    public void OnDamageTaken(EntityId id, int amount, string damageType)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        var originalColor = _creatureColor;

        var tween = mesh.CreateTween();
        tween.TweenProperty(material, "albedo_color", new Color(1.0f, 0.0f, 0.0f), 0.05);
        tween.TweenProperty(material, "albedo_color", originalColor, 0.15);
    }

    public void OnHealed(EntityId id, int amount)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        var originalColor = _creatureColor;

        var tween = mesh.CreateTween();
        tween.TweenProperty(material, "albedo_color", new Color(0.0f, 1.0f, 0.3f), 0.05);
        tween.TweenProperty(material, "albedo_color", originalColor, 0.2);
    }

    public void OnLevelUp(EntityId id, int newLevel)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        var tween = mesh.CreateTween();
        tween.TweenProperty(mesh, "scale", new Vector3(1.3f, 1.3f, 1.3f), 0.2);
        tween.TweenProperty(mesh, "scale", Vector3.One, 0.3);
    }

    public void OnAbilityUsed(EntityId id, string abilityId, TileCoordinate? target)
    {
        GD.Print($"Creature {_assetId} [{id}] used ability: {abilityId}");
    }

    public void OnMoraleChanged(EntityId id, string newState)
    {
        GD.Print($"Creature {_assetId} [{id}] morale -> {newState}");
    }

    public void OnSlapped(EntityId id)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        var tween = mesh.CreateTween();
        tween.TweenProperty(mesh, "scale", new Vector3(1.2f, 0.5f, 1.2f), 0.05);
        tween.TweenProperty(mesh, "scale", Vector3.One, 0.15);
    }

    public void OnPickedUp(EntityId id)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;
        mesh.Visible = false;
    }

    public void OnDropped(EntityId id, TileCoordinate position, bool stunned)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        mesh.Position = CoordinateHelper.TileToWorld(position, 0.35f);
        mesh.Visible = true;

        if (stunned)
        {
            var tween = mesh.CreateTween();
            tween.TweenProperty(mesh, "rotation_degrees:z", 15.0f, 0.1);
            tween.TweenProperty(mesh, "rotation_degrees:z", -15.0f, 0.1);
            tween.TweenProperty(mesh, "rotation_degrees:z", 0.0f, 0.1);
        }
    }

    public void OnDeath(EntityId id)
    {
        if (!_creatureNodes.TryGetValue(id, out var mesh)) return;

        var tween = mesh.CreateTween();
        tween.TweenProperty(mesh, "scale", Vector3.Zero, 0.5);
        tween.TweenCallback(Callable.From(() =>
        {
            mesh.QueueFree();
            _creatureNodes.Remove(id);
        }));
    }
}
