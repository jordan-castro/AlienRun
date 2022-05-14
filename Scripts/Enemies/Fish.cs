using Godot;
using System;

namespace Enemy
{

    public class Fish : Base
    {
        public override void _Ready()
        {
            base._Ready();
            JumpForce = -250;
            velocity.x = 0;
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);

            if (this.State != Character.State.InAir)
            {
                // Jump after a random amount of time
                if (GD.Randf() < 0.1f)
                {
                    Jump();
                }
            }
        }
    }
}