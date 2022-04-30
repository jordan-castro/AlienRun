using Godot;
using System;
using System.Threading.Tasks;

namespace Player
{
    public class Base : Character.Node
    {
        protected float Speed
        {
            get
            {
                if (Input.IsActionPressed("ui_sprint"))
                {
                    return walkingSpeed + 50;
                }

                return walkingSpeed;
            }
        }

        public override void _Ready()
        {
            walkingSpeed = 100;
            Health = 2;
        }

        protected override void PhysicsProcess(float delta)
        {
            base.PhysicsProcess(delta);

            // Handle user input
            if (Input.IsActionPressed("ui_right"))
            {
                velocity.x = Speed;
            }
            else if (Input.IsActionPressed("ui_left"))
            {
                velocity.x = -Speed;
            }
            else
            {
                velocity.x = 0;
            }
            if (Input.IsActionJustPressed("ui_up"))
            {
                Jump();
            }
            if (Input.IsActionPressed("ui_down"))
            {
                // TODO: Slide
            }
        }

        /// <summary>
        /// When the player takes damage.
        /// </summary>
        /// <param name="isOnTop"> Is the player on top of the enemy? </param>
        public async void TakeDamage(bool isOnTop)
        {
            // Only if alive.
            if (Health > 0)
            {
                if (isOnTop)
                {
                    canJump = true;
                }

                // Change collision settings.
                SetCollisionLayerBit(0, false);
                SetCollisionMaskBit(1, false);

                GD.Print("Before blinking");

                // Wait for 2 seconds
                await Blink();
                // Change collision settings back to normal.
                SetCollisionLayerBit(0, true);
                SetCollisionMaskBit(1, true);
            }
        }

        private async Task Blink()
        {
            int amount = 0;
            while (amount < 20)
            {
                // Wait for a third of a second
                await Task.Delay(150);
                // Toggle visibility
                Visible = !Visible;
                amount++;
            }
        }
    }
}