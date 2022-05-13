using Godot;
using System;
using Enemy;

public class Flying : Base
{
    public override void _Ready()
    {
        base._Ready();
        velocity.x = 0;
    }

    protected override void PhysicsProcess(float delta)
    {
        LoadSprite();

        if (!IsAlive)
        {
            base.PhysicsProcess(delta);
        }
    }
}
