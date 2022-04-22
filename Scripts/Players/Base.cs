using Godot;
using System;
using Character;

namespace Player
{
    public class Base : Character.Node
    {
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
    }
}