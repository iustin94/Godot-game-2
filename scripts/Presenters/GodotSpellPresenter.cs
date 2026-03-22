using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotSpellPresenter : ISpellPresenter
{
    private readonly Node3D _effectsRoot;

    public GodotSpellPresenter(Node3D effectsRoot)
    {
        _effectsRoot = effectsRoot;
    }

    public void OnSpellCast(string spellId, TileCoordinate? targetTile, EntityId? targetCreature)
    {
        if (targetTile == null) return;

        var sphere = PrimitiveMeshFactory.CreateSphere(new Color(0.3f, 0.6f, 1.0f), 0.3f);
        var material = PrimitiveMeshFactory.GetMaterial(sphere);
        material.Transparency = BaseMaterial3D.TransparencyEnum.Alpha;
        sphere.Position = CoordinateHelper.TileToWorld(targetTile.Value, 0.5f);
        _effectsRoot.AddChild(sphere);

        var tween = sphere.CreateTween();
        tween.TweenProperty(sphere, "scale", new Vector3(3.0f, 3.0f, 3.0f), 0.5);
        tween.Parallel().TweenProperty(
            PrimitiveMeshFactory.GetMaterial(sphere), "albedo_color",
            new Color(0.3f, 0.6f, 1.0f, 0.0f), 0.5);
        tween.TweenCallback(Callable.From(() => sphere.QueueFree()));
    }

    public void OnSpellResearched(string spellId)
    {
        GD.Print($"Spell researched: {spellId}");
    }
}
