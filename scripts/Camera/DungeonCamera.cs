using Godot;

namespace DungeonKeeper.Scripts.Camera;

public partial class DungeonCamera : Camera3D
{
    [Export] public float PanSpeed = 30.0f;
    [Export] public float ZoomSpeed = 5.0f;
    [Export] public float MinZoom = 10.0f;
    [Export] public float MaxZoom = 80.0f;
    [Export] public float MapSize = 85.0f;

    public override void _Ready()
    {
        // Start centered above the map, looking straight down
        float center = MapSize / 2f;
        Position = new Vector3(center, 40f, center + 15f);
        RotationDegrees = new Vector3(-70f, 0f, 0f);
    }

    public override void _Process(double delta)
    {
        var direction = Vector3.Zero;
        float dt = (float)delta;

        // Scale pan speed with zoom height
        float speedScale = Position.Y / 40f;
        float currentSpeed = PanSpeed * speedScale * dt;

        if (Godot.Input.IsActionPressed("ui_up") || Godot.Input.IsKeyPressed(Key.W))
            direction.Z -= 1;
        if (Godot.Input.IsActionPressed("ui_down") || Godot.Input.IsKeyPressed(Key.S))
            direction.Z += 1;
        if (Godot.Input.IsActionPressed("ui_left") || Godot.Input.IsKeyPressed(Key.A))
            direction.X -= 1;
        if (Godot.Input.IsActionPressed("ui_right") || Godot.Input.IsKeyPressed(Key.D))
            direction.X += 1;

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            var newPos = Position + direction * currentSpeed;

            // Clamp to map bounds with some padding
            newPos.X = Mathf.Clamp(newPos.X, -5f, MapSize + 5f);
            newPos.Z = Mathf.Clamp(newPos.Z, -5f, MapSize + 5f);
            newPos.Y = Position.Y; // Preserve height

            Position = newPos;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            float zoomDelta = ZoomSpeed;
            if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                Position = new Vector3(Position.X, Mathf.Max(Position.Y - zoomDelta, MinZoom), Position.Z);
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                Position = new Vector3(Position.X, Mathf.Min(Position.Y + zoomDelta, MaxZoom), Position.Z);
            }
        }
    }
}
