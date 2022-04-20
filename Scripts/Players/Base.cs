using Godot;
using System;
using CharacterNode;

namespace Player
{
    public class Base : Character
    {
        protected int jumpForce = 300;

        public bool canJump = false;

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);
            // Handle user input            
            if (Input.IsActionPressed("ui_right"))
            {
                MoveH(speed, delta);
            }
            if (Input.IsActionPressed("ui_left"))
            {
                MoveH(-speed, delta);
            }
            if (Input.IsActionPressed("ui_up"))
            {
                Jump();
            }
            if (Input.IsActionPressed("ui_down"))
            {
                // TODO: Slide
            }
        }

        /// <summary>
        /// Jump the Player. This can be overridden by sub Player classes.
        /// </summary>   
        protected virtual void Jump() 
        {
            // If player state = OnGround, set canJump to true.
            if (state == CharacterState.OnGround)
            {
                canJump = true;
            }

            if (canJump)
            {
                velocity.y = -jumpForce;
                canJump = false;
            }
        }
    }
}