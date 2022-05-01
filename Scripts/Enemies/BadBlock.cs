using Godot;
using System;
using Enemy;

namespace Enemy
{
    public class BadBlock : Base
    {
        private RayCast2D bottomCast;
        private float oy;

        public override void _Ready()
        {
            base._Ready();
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

            if (bottomCast.IsColliding())
            {
                // Check if colliding node is a Player.Base
                if (bottomCast.GetCollider() is Player.Base)
                {
                    // Apply the regular PhysicsProcess
                    base.PhysicsProcess(delta);
                }
            }
            else if (oy < Position.y)
            {
                // Move back up to original position
                MoveAndSlide(new Vector2(0, -walkingSpeed), Vector2.Up);
            }
        }
    }
}