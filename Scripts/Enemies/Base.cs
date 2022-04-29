using Godot;
using System;

namespace Enemy
{
    public class Base : Character.Node
    {
        private RayCast2D topCast;
        
        /// <summary>
        /// Can this enemy be stomped on?
        /// </summary>
        public bool Stompable { get; protected set; } = true;
        /// <summary>
        /// When the player tries to stomp this enemy, should the player jump if Stompable is false?
        /// </summary>
        public bool JumpPlayer { get; protected set; } = false;

        public override void _Ready()
        {
            // Start towards the player
            walkingSpeed = 25;
            velocity.x = -walkingSpeed;

            // Setup topCast
            topCast = GetNode<RayCast2D>("TopCast");
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);

            // Check if the enemy is on a wall and the wall is not a plauer.
            if (IsOnWall() && !IsOnPlayer())
            {
                velocity.x = -velocity.x;
            }
        }

        /// <summary>
        /// Check if the enemy has an Player.Base on top of it.
        /// </summary>
        public bool PlayerIsOnTop()
        {
            // Check cast
            if (topCast.IsColliding())
            {
                // Check collider
                if (topCast.GetCollider() is Player.Base)
                {
                    return true;
                }
            }
            return false;
        }

        // Check if colliding with player
        private bool IsOnPlayer()
        {
            // Don't even bother if the count is 0
            if (GetSlideCount() > 0)
            {
                for (int i = 0; i < GetSlideCount(); i++)
                {
                    if (GetSlideCollision(i).Collider is Player.Base)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}