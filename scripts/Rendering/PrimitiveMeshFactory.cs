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

    /// <summary>
    /// Creates a 3D box structure for a room tile, sitting on top of the ground.
    /// </summary>
    public static MeshInstance3D CreateRoomBlock(Color color, float height = 0.3f)
    {
        var mesh = new BoxMesh
        {
            Size = new Vector3(0.92f, height, 0.92f)
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.65f,
            Metallic = 0.05f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    /// <summary>
    /// Creates a tall, imposing Dungeon Heart structure with emission glow.
    /// </summary>
    public static MeshInstance3D CreateDungeonHeart(Color color)
    {
        var mesh = new BoxMesh
        {
            Size = new Vector3(0.85f, 0.6f, 0.85f)
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.4f,
            Metallic = 0.2f,
            Emission = new Color(0.6f, 0.0f, 0.0f),
            EmissionEnergyMultiplier = 0.5f
        };
        mesh.Material = material;

        return new MeshInstance3D { Mesh = mesh };
    }

    /// <summary>
    /// Creates a Portal structure — a glowing cylinder.
    /// </summary>
    public static MeshInstance3D CreatePortalStructure(Color color)
    {
        var mesh = new CylinderMesh
        {
            Height = 0.4f,
            TopRadius = 0.35f,
            BottomRadius = 0.42f
        };

        var material = new StandardMaterial3D
        {
            AlbedoColor = color,
            Roughness = 0.3f,
            Metallic = 0.15f,
            Emission = new Color(0.8f, 0.0f, 0.8f),
            EmissionEnergyMultiplier = 0.4f
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
