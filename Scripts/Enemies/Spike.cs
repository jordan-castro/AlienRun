using Godot;
using System;

namespace Enemy
{

    public class Spike : Base
    {
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            velocity.x = 0;

            // Set jump and stomp
            JumpPlayer = true;
            Stompable = false;
        }
    }
}