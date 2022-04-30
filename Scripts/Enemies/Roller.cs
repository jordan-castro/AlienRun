using Godot;
using System;

namespace Enemy
{
    public class Roller : Base
    {
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            Stompable = false;
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);

            // Decide direction to rotate in.
            int direction = 0; // 0 stays stilll

            if (velocity.x > 0)
            {
                direction = 1; // Go right
            }
            else if (velocity.x < 0)
            {
                direction = -1; // Go left
            }

            // Apply rotation
            sprite.Rotate((5 * direction) * delta);
        }
    }
}