using Godot;
using System;
using Enemy;
using System.Threading.Tasks;

public class Flying : Base
{
    private PathFollow2D pathFollow;
    private int movementSpeed = 40;
    private int movement;

    public override void _Ready()
    {
        base._Ready();
        velocity.x = 0;
        pathFollow = GetParent<PathFollow2D>();

        movement = movementSpeed;
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        if (IsAlive)
        {
            // Not affected by gravity
            velocity.y = 0;
        }

        // Some logic to move the enemy up or down depending on where it was just
        // Starts at 100 (move up). When reaches the top it changes to -100 (move down). When it reaches the bottom it changes to 100 (move up).
        if (pathFollow.UnitOffset == 1 && movement > 0) // Top
        {
            PauseFlying(-movementSpeed);
        }
        else if (pathFollow.UnitOffset == 0 && movement > 0) // Bottom
        {
            PauseFlying(movementSpeed);
        }

        pathFollow.Offset += movement * delta;
    }

    private async void PauseFlying(int toBe)
    {
        movement = 0;
        // Wait for a moment
        await Task.Delay(500);
        GD.Print("Should be back already");
        movement = toBe;
    }
}
