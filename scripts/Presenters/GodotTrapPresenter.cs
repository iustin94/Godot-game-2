using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Scripts.Rendering;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotTrapPresenter : ITrapPresenter
{
    private readonly Node3D _mapRoot;
    private readonly Dictionary<EntityId, MeshInstance3D> _trapNodes = new();
    private readonly Dictionary<EntityId, MeshInstance3D> _doorNodes = new();

    public GodotTrapPresenter(Node3D mapRoot)
    {
        _mapRoot = mapRoot;
    }

    public void OnTrapPlaced(EntityId trapId, TileCoordinate position)
    {
        var mesh = PrimitiveMeshFactory.CreateCylinder(new Color(0.6f, 0.1f, 0.1f), 0.08f, 0.35f);
        mesh.Position = CoordinateHelper.TileToWorld(position, 0.04f);
        _mapRoot.AddChild(mesh);
        _trapNodes[trapId] = mesh;
    }

    public void OnTrapTriggered(EntityId trapId)
    {
        if (!_trapNodes.TryGetValue(trapId, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        var tween = mesh.CreateTween();
        tween.TweenProperty(material, "albedo_color", new Color(1.0f, 1.0f, 1.0f), 0.05);
        tween.TweenProperty(material, "albedo_color", new Color(0.6f, 0.1f, 0.1f), 0.3);
    }

    public void OnTrapRearmed(EntityId trapId)
    {
        if (!_trapNodes.TryGetValue(trapId, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        material.AlbedoColor = new Color(0.6f, 0.1f, 0.1f);
    }

    public void OnDoorStateChanged(EntityId doorId, bool isOpen)
    {
        if (!_doorNodes.TryGetValue(doorId, out var mesh))
        {
            // Door not yet visualized — create a thin tall box
            mesh = PrimitiveMeshFactory.CreateTileBox(new Color(0.4f, 0.25f, 0.1f), 0.8f);
            _mapRoot.AddChild(mesh);
            _doorNodes[doorId] = mesh;
        }

        mesh.RotationDegrees = isOpen ? new Vector3(0, 90, 0) : Vector3.Zero;
    }

    public void OnDoorDamaged(EntityId doorId, float healthPercent)
    {
        if (!_doorNodes.TryGetValue(doorId, out var mesh)) return;

        var material = PrimitiveMeshFactory.GetMaterial(mesh);
        material.AlbedoColor = new Color(0.4f, 0.25f, 0.1f).Lerp(new Color(0.8f, 0.0f, 0.0f), 1f - healthPercent);
    }
}
