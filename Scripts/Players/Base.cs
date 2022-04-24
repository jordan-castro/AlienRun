using Godot;
using System;
using Character;

namespace Player
{
    public class Base : Character.Node
    {
        public override void _Ready()
        {
            Speed = 100;
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);
            // Handle user input            
            if (Input.IsActionPressed("ui_right"))
            {
                velocity.x = Speed;
            }
            else if (Input.IsActionPressed("ui_left"))
            {
                velocity.x = -Speed;
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