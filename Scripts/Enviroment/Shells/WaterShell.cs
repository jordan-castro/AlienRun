using Godot;
using System;
using Enemy;

namespace Enviroment
{
    namespace Shells
    {
        public class WaterShell : Base
        {
            public override void On_body_entered(Node body)
            {
                // Kill the Character when they touch water, so long as no a fish.
                if (body is Character.Node && !(body is Fish))
                {
                    (body as Character.Node).Health = -1;
                }
            }
        }
    }
}