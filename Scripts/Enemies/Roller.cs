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
            JumpPlayer = true;
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);

            // Find out the direction.
            int direction = 0; // 0 is stationary
            // Check direction


            // Apply rotation
            GetNode<Sprite>("Sprite").Rotate(direction * walkingSpeed);
        }
    }
}