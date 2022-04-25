using Godot;
using System;

namespace Enemy
{
    public class Base : Character.Node
    {
        private RayCast2D topCast;

        public bool Stompable { get; protected set; } = true;

        public override void _Ready()
        {
            // Start towards the player
            speed = 25;
            velocity.x = -speed;

            // Setup topCast
            topCast = GetNode<RayCast2D>("TopCast");
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);

            // Check if the enemy is on a wall
            if (IsOnWall())
            {
                velocity.x = -velocity.x;
            }
        }

        /// <summary>
        /// Check if the enemy has an Character.Node on top of it.
        /// </summary>
        public bool CharacterIsOnTop()
        {
            // Check cast
            if (topCast.IsColliding())
            {
                // Check collider
                if (topCast.GetCollider() is Character.Node)
                {
                    return true;
                }
            }
            return false;
        }
    }
}