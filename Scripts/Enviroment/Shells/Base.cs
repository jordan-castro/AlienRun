using Godot;
using System;

namespace Enviroment
{
    namespace Shells 
    {
        public class Base : Area2D
        {
            public override void _Ready()
            {
                // Connect signals
                Connect("body_entered", this, "On_body_entered");
                Connect("body_exited", this, "On_body_exited");
            }

            public virtual void On_body_entered(Node body)
            {
            }

            public virtual void On_body_exited(Node body)
            {
            }
        }
    }
}