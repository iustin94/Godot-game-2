using Godot;

namespace DungeonKeeper.Scripts.Rendering;

public static class PrimitiveMeshFactory
{
    public static MeshInstance3D CreateTileBox(Color color, float height = 0.1f)
    {
        var mesh = new BoxMesh
        {
            Size = new Vector3(1.0f, height, 1.0f)
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.8f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    public static MeshInstance3D CreateCreatureCapsule(Color color, float height = 0.6f, float radius = 0.2f)
    {
        var mesh = new CapsuleMesh
        {
            Height = height,
            Radius = radius
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.6f,
            Metallic = 0.1f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    public static MeshInstance3D CreateFloorPlane(Color color)
    {
        var mesh = new BoxMesh
        {
            Size = new Vector3(0.95f, 0.04f, 0.95f)
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.7f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    public static MeshInstance3D CreateSphere(Color color, float radius = 0.15f)
    {
        var mesh = new SphereMesh
        {
            Radius = radius,
            Height = radius * 2
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.5f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    public static MeshInstance3D CreateCylinder(Color color, float height = 0.1f, float radius = 0.4f)
    {
        var mesh = new CylinderMesh
        {
            Height = height,
            TopRadius = radius,
            BottomRadius = radius
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.7f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    public static StandardMaterial3D GetMaterial(MeshInstance3D meshInstance)
    {
        return (StandardMaterial3D)((PrimitiveMesh)meshInstance.Mesh).Material;
    }
}
