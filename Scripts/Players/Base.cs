using Godot;
using System;
using System.Threading.Tasks;

namespace Player
{
    public class Base : Character.Node
    {
        public int Coins { get; set; } = 0;

        private bool isBlinking = false;

        public override int Health
        {
            get => base.Health; set
            {
                if (!isBlinking)
                {
                    base.Health = value;
                }
            }
        }

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

            if (Input.IsActionJustPressed("ui_up") && State != Character.State.Climbing)
            {
                Jump();
            }
            
            if (Input.IsActionPressed("ui_up"))
            {
                if (State == Character.State.Climbing)
                {
                    // Move up based on speed.
                    velocity.y = -Speed;
                }
            }
            else if (Input.IsActionPressed("ui_down"))
            {
                if (State == Character.State.Climbing)
                {
                    // Move down based on speed.
                    velocity.y = Speed;
                }
                // TODO: Slide
            }
            else if (State == Character.State.Climbing)
            {
                velocity.y = 0;
            }
        }

        /// <summary>
        /// When the player takes damage. Needed because sometimes the player needs to jump when attacked.
        /// </summary>
        /// <param name="isOnTop"> Is the player on top of the enemy? </param>
        public void TakeDamage(bool isOnTop)
        {
            // Take health regardless of alive or not.
            Health -= 1;

            // Only blink if alive.
            if (IsAlive)
            {
                if (isOnTop)
                {
                    canJump = true;
                }

                Blink();
            }
        }

        private async void Blink()
        {
            // Change collision settings.
            SetCollisionLayerBit(0, false);
            SetCollisionMaskBit(1, false);

            isBlinking = true;

            int amount = 0;
            while (amount < 20)
            {
                // Wait for a third of a second
                await Task.Delay(150);
                // Toggle visibility
                Visible = !Visible;
                amount++;
            }

            isBlinking = false;
            // Change collision settings back to normal.
            SetCollisionLayerBit(0, true);
            SetCollisionMaskBit(1, true);
        }

        public void StartClimb()
        {
            State = Character.State.Climbing;
        }

        public void StopClimb()
        {
            CheckCharacterState();
        }
    }
}