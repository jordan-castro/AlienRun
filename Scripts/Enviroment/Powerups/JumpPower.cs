using Godot;
using System;

namespace Enviroment
{
    namespace Powerups
    {
        public class JumpPower : Base
        {
            // Override the OnBodyEntered method
            protected override void OnPlayerEntered(Player.Base player)
            {
                // Add the power up
                new Player.Jumper().Load(player);
            }
        }
    }
}
