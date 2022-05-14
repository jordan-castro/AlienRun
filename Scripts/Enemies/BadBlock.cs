using Godot;
using System;

namespace Enemy
{
    public class BadBlock : Base
    {
        private RayCast2D bottomCast;
        private float oy;
        private bool canFall = true;

        public override void _Ready()
        {
            base._Ready();
            Stompable = false;
            bottomCast = GetNode<RayCast2D>("BottomCast");
            velocity.x = 0;
            oy = Position.y;
        }

        protected override void PhysicsProcess(float delta)
        {
            // When block is dead, it should just fall.
            if (!IsAlive)
            {
                base.PhysicsProcess(delta);
                return;
            }

            if (IsOnTopOfPlayer() && canFall)
            {
                // Apply the regular PhysicsProcess
                base.PhysicsProcess(delta);
            }
            else if (oy < Position.y)
            {
                canFall = false;
                // Move back up to original position
                MoveAndSlide(new Vector2(0, -walkingSpeed), Vector2.Up);
                this.State = Character.State.Idle;
            }
            else
            {
                canFall = true;
                velocity.y = 0;
                this.State = Character.State.Idle;
            }
        }

        /// <summary>
        /// Check if the block is on top of the player.
        /// </summary>
        public bool IsOnTopOfPlayer()
        {
            // Check bottomCast
            if (bottomCast.IsColliding())
            {
                // Check if colliding node is a Player.Base
                if (bottomCast.GetCollider() is Player.Base)
                {
                    return true;
                }
            }

            return false;
        }
    }
}