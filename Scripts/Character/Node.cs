using Godot;
using System;

namespace Character
{

    /// <summary>
    /// Possible states of the character.
    /// </summary>
    public enum State
    {
        Idle,
        Walking,
        InAir,
        Running,
    }

    /// <summary>
    /// Base class for most movable entities in the game.
    /// </summary>
    public class Node : KinematicBody2D
    {
        private int health = 1;

        /// <summary>
        /// The characters walking speed.
        /// </summary>
        protected float walkingSpeed;

        public int JumpForce { get; set; } = -225;

        /// <summary>
        /// Can the character currently jump?
        /// </summary>
        protected bool canJump = false;

        /// <summary>
        /// The current state of the character.
        /// </summary>
        public State State { get; protected set; }

        public Vector2 velocity = new Vector2();

        public virtual int Health
        {
            get => health;
            set
            {
                health = value;
                GD.Print(Name + "'s Health: " + health); // TODO: remove this before production.
                if (health < 0)
                {
                    Die();
                }
            }
        }

        /// <summary>
        /// Check if the character is alive.
        /// </summary>
        public bool IsAlive => Health >= 0;

        protected Animation sprite;

        /// <summary>
        /// Classes that inherit from Character.Node should override this method if they want to 
        /// run during _PhysicsProcess.
        /// </summary>
        protected virtual void PhysicsProcess(float delta)
        {
            LoadSprite();
            CheckCharacterState();
            ApplyGravity(delta);

            // Do not move if we are dead.
            if (!IsAlive)
            {
                velocity.x = 0;
            }
            // We can however, fall.
            MoveAndSlide(velocity, Vector2.Up);
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            PhysicsProcess(delta);

            // Apply animation to the Character if alive.
            if (sprite != null && IsAlive)
            {
                sprite.UpdateAnimation(velocity, State);
            }
        }

        private void ApplyGravity(float delta)
        {
            // If we are touching a ceiling, we should set the velocity to 1, 
            // this stops the character from getting stuck in the ceiling.
            if (IsOnCeiling())
            {
                velocity.y = 1;
            }

            // Only apply gravity if the character is InAir.
            if (this.State == State.InAir)
            {
                velocity.y += Utils.Globals.Gravity * delta;
            }
        }

        // Checks what state the Character should be in.
        private void CheckCharacterState()
        {
            // If the character is on the ground, set the state to OnGround.
            if (IsOnFloor())
            {
                // Check if walking or running
                if (Math.Abs(velocity.x) > walkingSpeed)
                {
                    this.State = State.Running;
                }
                else if (velocity.x != 0)
                {
                    this.State = State.Walking;
                }
                else
                {
                    this.State = State.Idle;
                }
            }
            // If the character is not on the ground, set the state to InAir.
            else
            {
                this.State = State.InAir;
            }
        }

        /// <summary>
        /// This gets called when the Character dies.
        /// </summary>
        protected virtual void Die()
        {
            // Rotate the character
            Rotate(180);

            // Remove the CollisionShape
            RemoveChild(GetNode("Collider"));

            sprite.Idle();
        }

        /// <summary>
        /// Jump the Character. This should be overriden by classes that want a more impressive jump.
        /// </summary>   
        public virtual void Jump()
        {
            // If character state is on ground, then we can jump!
            if (this.State == State.Walking || this.State == State.Running || this.State == State.Idle)
            {
                canJump = true;
            }

            if (canJump)
            {
                velocity.y = JumpForce;
                canJump = false;
            }
        }

        // Sprite takes a moment to load, so we load it once the game starts and the player is spawned.
        private void LoadSprite()
        {
            if (sprite == null)
            {
                sprite = GetNode<Animation>("Animation");
            }
        }
    }
}