using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotCombatPresenter : ICombatPresenter
{
    private readonly Node3D _effectsRoot;
    private readonly Dictionary<EntityId, MeshInstance3D> _creatureNodes;

    public GodotCombatPresenter(Node3D effectsRoot, Dictionary<EntityId, MeshInstance3D> creatureNodes)
    {
        _effectsRoot = effectsRoot;
        _creatureNodes = creatureNodes;
    }

    public void OnAttack(EntityId attackerId, EntityId defenderId, bool hit, int damage, bool critical)
    {
        if (!hit) return;
        if (!_creatureNodes.TryGetValue(defenderId, out var defenderMesh)) return;

        var size = critical ? 0.25f : 0.15f;
        var color = critical ? new Color(1.0f, 0.0f, 0.0f) : new Color(1.0f, 0.3f, 0.0f);
        var sphere = PrimitiveMeshFactory.CreateSphere(color, size);
        sphere.Position = defenderMesh.Position + new Vector3(0, 0.5f, 0);
        _effectsRoot.AddChild(sphere);

        var tween = sphere.CreateTween();
        tween.TweenProperty(sphere, "scale", Vector3.Zero, 0.3);
        tween.TweenCallback(Callable.From(() => sphere.QueueFree()));
    }

    public void OnStatusEffectApplied(EntityId targetId, string effectType)
    {
        GD.Print($"Status effect {effectType} applied to {targetId}");
    }

    public void OnStatusEffectRemoved(EntityId targetId, string effectType)
    {
        GD.Print($"Status effect {effectType} removed from {targetId}");
    }
}
