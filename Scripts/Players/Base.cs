using Godot;
using System;
using Character;

namespace Player
{
    public class Base : Character.Node
    {
        public override void _Ready()
        {
            speed = 100;
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);
            // Handle user input            
            if (Input.IsActionPressed("ui_right"))
            {
                velocity.x = speed;
            }
            else if (Input.IsActionPressed("ui_left"))
            {
                velocity.x = -speed;
            }
            else 
            {
                velocity.x = 0;
            }
            if (Input.IsActionJustPressed("ui_up"))
            {
                Jump();
            }
            if (Input.IsActionPressed("ui_down"))
            {
                // TODO: Slide
            }
        }
    }
}