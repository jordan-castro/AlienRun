using Godot;
using System;

namespace Character
{
    public class Animation : AnimatedSprite
    {
        // The animations that are available to the character.
        string idle = "Idle";
        string jump = "Jump";
        string walk = "Walk";
        string run = "Run";
        string attack = "Attack";

        int GetNextFrame(string ani)
        {
            if (Animation == ani)
            {
                if (Frame == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (Animation == idle)
            {
                return 1;
            }

            return -1;
        }

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
            int frame = GetNextFrame(run);

            Animate(walk);
            if (frame > -1)
            {
                Frame = frame;
            }
        }

        public virtual void Run()
        {
            int frame = GetNextFrame(walk);

            Animate(run);
            if (frame > -1)
            {
                Frame = frame;
            }
        }

        public virtual void Attack()
        {
            Animate(attack);
        }

        private void Animate(string animation)
        {
            // Check if the animation exists
            if (Frames.HasAnimation(animation))
            {
                Animation = animation;
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
            else if (state == State.Walking)
            {
                Walk();
            }
            else if (state == State.Idle)
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