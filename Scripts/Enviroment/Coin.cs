using Godot;
using System;

namespace Enviroment
{
    public class Coin : Area2D
    {
        public void On_Coin_body_entered(Node body)
        {
            // Check if the node is a Player
            if (body is Player.Base)
            {
                var player = body as Player.Base;
                // Add to player coins
                player.Coins++;
                // Remove from scene
                QueueFree();
            }
        }
    }
}