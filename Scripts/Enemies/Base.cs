using Godot;
using System;
using CharacterNode;

namespace Enemy
{
    public class Base : Character
    {
        private RayCast2D leftCast = null;
        private RayCast2D rightCast = null;

        public override void _Ready()
        {
            // Get the RayCasts
            leftCast = GetNode<RayCast2D>("RayCast2D/Left"); // TODO: Not sure about these names.
            rightCast = GetNode<RayCast2D>("RayCast2D/Right"); // TODO: Not sure about these names.
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);
            BounceOff(delta);
        }

        /// <summary>
        /// The enemy bounces off walls and Enemies.
        /// </summary>
        private void BounceOff(float delta)
        {
            // Check which cast is colliding
            int direction = 0;

            if (leftCast.IsColliding())
            {
                direction = -1;
            }
            else if (rightCast.IsColliding())
            {
                direction = 1;
            }

            // Maybe we are not colliding with anything?
            if (direction == 0) return;

            MoveH(direction * speed, delta);
        }
    }
}