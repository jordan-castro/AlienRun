using Godot;
using System;

namespace Character
{
    public class Animation : AnimatedSprite
    {
        // The animations that are available to the character.
        string idle = "Idle";
        string jump = "Jump";
        string run = "Run";
        string walk = "Walk";
        string attack = "Attack";

        public virtual void Jump()
        {
            Animate(jump);
        }

        public virtual void Idle()
        {
            Animate(idle);
        }

        public virtual void Walk()
        {
            Animate(walk);
        }

        public virtual void Run()
        {
            Animate(attack);
        }

        public virtual void Attack()
        {
            Animate(attack);
        }

        private void Animate(string animation)
        {
            int frame = 0;
            // If we are idle then we want to start at the 1 frame.
            if (Animation == idle)
            {
                frame = 1;
            }

            // Check if the animation exists
            if (Frames.HasAnimation(animation))
            {
                Animation = animation;
                if (frame != 0)
                {
                    Frame = frame;
                }
            }
        }

        /// <summary>
        /// Updates the animation of the Character.
        /// </summary>
        public virtual void UpdateAnimation(Vector2 velocity, State state)
        {
            // Check velocity and determine which direction the character is facing.
            if (velocity.x > 0)
            {
                FlipH = true; // Right
            }
            else if (velocity.x < 0)
            {
                FlipH = false; // Left
            }
            // else: we stopped moving so no flip needed

            // Check if we are running or walking
            if (state == State.Running)
            {
                Run();
            }
            else if (velocity.x != 0)
            {
                Walk();
            }
            else
            {
                Idle();
            }

            // If we are jumping, play the jump animation and set the next frame.
            if (state == State.InAir)
            {
                Jump();
            }
        }
    }
}