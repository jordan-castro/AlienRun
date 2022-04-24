using Godot;
using System;
using Character;

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
    }
}