using Godot;
using System;
using Character;

namespace Enemy
{
    public class Base : Character.Node
    {
        public override void _Ready()
        {
            speed = 75;
            // Randomly choose a direction to move in
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                speed *= -1;
            }
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);
            
            // Check if the enemy is on a wall
            if (IsOnWall())
            {
                speed = -speed;
            }

            velocity.x = speed;
        }
    }
}