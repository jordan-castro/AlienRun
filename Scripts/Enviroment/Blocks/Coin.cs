using Godot;
using System;

namespace Enviroment
{
    namespace Blocks
    {
        public class Coin : Base
        {
            private PathFollow2D path;
            private bool hasShot;

            public override void _Ready()
            {
                base._Ready();
                path = GetNode<Path2D>("Path2D").GetNode<PathFollow2D>("PathFollow2D");
            }

            public override void _PhysicsProcess(float delta)
            {
                base._PhysicsProcess(delta);

                if (hasShot)
                {
                    // Update the offset on the path
                    path.Offset += 100 * delta;

                    if (path.UnitOffset == 1)
                    {
                        // Queu the coin for removal
                        foreach (Node2D child in path.GetChildren())
                        {
                            child.QueueFree();
                        }
                        hasShot = false;
                    }
                }
            }

            // Override the on hit by player method
            protected override void OnHitByPlayer(Player.Base player)
            {
                if (isActive)
                {
                    ShootCoin();
                    isActive = false;
                    player.Coins++;
                }

                base.OnHitByPlayer(player);
            }

            // Shoot a coin
            private void ShootCoin()
            {
                // Load the coin object
                PackedScene scene = (PackedScene)ResourceLoader.Load("res://Scenes/Enviroment/Coin.tscn");
                var coin = (Enviroment.Coin)scene.Instance();
                coin.Name = "Coin";

                // add the coin
                path.AddChild(coin);
                hasShot = true;
            }
        }
    }
}