using Godot;
using System;

namespace Player
{
    public class Jumper : Base
    {
        private int jumpCount = 0;

        public override void Jump()
        {
            GD.Print(jumpCount);
            if (this.State == Character.State.Walking || this.State == Character.State.Running || this.State == Character.State.Idle)
            {
                // Set the jump count back to 0
                jumpCount = 0;
            }

            if (jumpCount < 2)
            {
                canJump = true;
                jumpCount++;
            }

            base.Jump();
        }

        /// <summary>
        /// Load in this powered up player.
        /// </summary>
        public void Load(Base player)
        {
            // Replace the player with the correct scene.
            PackedScene scene = GD.Load<PackedScene>("res://Scenes/Players/Jumper.tscn");

            // Get the current Level scene
            AlienRun.Level level = (AlienRun.Level)player.GetParent();

            // Get current position and remove the old player.
            Vector2 position = player.GlobalPosition;
            player.QueueFree();

            // Add new player.
            Node2D newPlayer = scene.Instance() as Node2D;
            level.AddChild(newPlayer);
            level.CollisionSetup(newPlayer as Character.Node);
            newPlayer.GlobalPosition = position;
        }
    }
}