using Godot;
using System;

namespace Enviroment
{
    namespace Blocks
    {
        public enum BlockPowerUp
        {
            Jump
        }

        public class PowerBlock : Base
        {
            [Export]
            private BlockPowerUp powerUp;

            protected override void OnHitByPlayer(Player.Base player)
            {
                if (isActive)
                {
                    // Load in the powerup ontop of the block
                    var node = LoadFromBP();
                    AddChild(node);
                    node.GlobalPosition = new Vector2(GlobalPosition.x, GlobalPosition.y - 18);
                    isActive = false;
                }

                base.OnHitByPlayer(player);
            }

            // Load scene based on block powerup
            private Node2D LoadFromBP()
            {
                Node2D buildNode(string scenePath)
                {
                    var scene = (PackedScene)ResourceLoader.Load(scenePath);
                    return (Node2D)scene.Instance();
                }

                switch (powerUp)
                {
                    case BlockPowerUp.Jump:
                        return buildNode("res://Scenes/Enviroment/Powerups/JumpPower.tscn");
                    default:
                        return buildNode("res://Scenes/Enviroment/Powerups/JumpPower.tscn");
                }
            }
        }
    }
}