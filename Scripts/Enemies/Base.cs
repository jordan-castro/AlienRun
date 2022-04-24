using Godot;
using System;

namespace Enemy
{
    public class Base : Character.Node
    {
        public override void _Ready()
        {
            // Start towards the player
            speed = 25;
            velocity.x = -speed;
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

        // /// <summary>
        // /// Handle collisions.
        // /// </summary>
        // protected override void HandleCollision(Godot.Object collider)
        // {
        //     // Check if collider is player
        //     if (collider is Player.Base) 
        //     {
        //         Player.Base player = collider as Player.Base;
        //         // Damage the player if they touched the enemy to the side or from the bottom
        //         if (IsOnFloor() && IsOnWall() && !IsOnCeiling())
        //         {
        //             player.Health -= AttackPower;
        //         }
        //         else if (IsOnCeiling()) 
        //         {
        //             // This means the player has stomped on the enemy
        //             Health -= player.AttackPower;
        //         }
        //     }
        // }
    }
}