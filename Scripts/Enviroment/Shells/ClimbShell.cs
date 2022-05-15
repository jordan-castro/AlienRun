using Godot;
using System;

namespace Enviroment
{
    namespace Shells
    {
        public class ClimbShell : Base
        {
            public override void On_body_entered(Node body)
            {
                if (body is Player.Base)
                {
                    // Set climbing to true
                    (body as Player.Base).StartClimb();
                }
            }

            public override void On_body_exited(Node body)
            {
                if (body is Player.Base)
                {
                    // Set climbing to false
                    (body as Player.Base).StopClimb();
                }
            }
        }
    }
}