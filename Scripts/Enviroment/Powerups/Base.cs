using Godot;
using System;

namespace Enviroment
{

    namespace Powerups
    {
        public class Base : Area2D 
        {
            public override void _Ready()
            {
                // Connect a signal
                Connect("body_entered", this, "On_body_entered");
            }

            // Self explanatory
            public void On_body_entered(Node body)
            {
                // Check if body is a player
                if (body is Player.Base)
                {
                    OnPlayerEntered(body as Player.Base);
                    QueueFree();
                }
            }

            /// <summary>
            /// This method fires whan a Player.Base object enters the Area2D. 
            /// Without overriding this method, the Area2D will throw an error.
            /// </summary>
            /// <param name="player">The Player.Base object that entered the Area2D.</param>
            protected virtual void OnPlayerEntered(Player.Base player)
            {
                throw new NotImplementedException("OnPlayerEntered() is not implemented in the Base class.");
            }
        }
    }
}