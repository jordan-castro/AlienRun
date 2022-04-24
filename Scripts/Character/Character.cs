using Godot;
using System;
using Utils;

/// <summary>
/// Base class for most movable entities in the game.
/// </summary>
namespace Character
{
    public enum State
    {
        OnGround,
        InAir
    }

    public class Node : KinematicBody2D
    {
        public int jumpForce = 200;

        /// <summary>
        /// Can the character currently jump?
        /// </summary>
        public bool canJump = false;

        /// <summary>
        /// The current state of the character.
        /// </summary>
        public State State { get; private set; }

        public Vector2 velocity = new Vector2();

        public int attackPower = 10;

        public int health = 100;

        public float speed;

        public bool IsDead { get { return health <= 0; } }

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
            Move(delta);
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            PhysicsProcess(delta);

            // Apply animation to the Character
            if (sprite != null)
            {
                sprite.UpdateAnimation(velocity, State, speed);
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

        /// <summary>
        /// Checks what state the Character should be in.
        /// </summary>
        private void CheckCharacterState()
        {
            // If the character is on the ground, set the state to OnGround.
            if (IsOnFloor())
            {
                this.State = State.OnGround;
            }
            // If the character is not on the ground, set the state to InAir.
            else
            {
                this.State = State.InAir;
            }
        }

        private void Move(float delta)
        {
            MoveAndSlide(velocity, Vector2.Up);
        }

        /// <summary>
        /// This gets called when the Character dies.
        /// </summary>
        protected virtual async void Die()
        {
            // Rotate the character
            Rotate(180);

            // Remove the CollisionShape
            RemoveChild(GetNode<CollisionShape2D>("Collision"));

            // Wait a second and then QueueFree the Character.
            await System.Threading.Tasks.Task.Delay(1000);
            QueueFree();
        }

        /// <summary>
        ///     Attack another character.
        /// </summary>
        /// <param name="target">The character to attack.</param>
        /// <param name="damageToGive">The amount of damage to give to the character, if null, the attackPower is used.</param>
        public void Attack(Character.Node target, int? damageToGive)
        {
            if (damageToGive == null)
            {
                damageToGive = attackPower;
            }
            target.health -= damageToGive.Value;
            // Check if the target is dead.
            if (target.IsDead)
            {
                // Run the Die code
                target.Die();
            }
        }

        /// <summary>
        /// Jump the Player. This should be overriden by classes that want a more impressive jump.
        /// </summary>   
        protected virtual void Jump()
        {
            // If player state = OnGround, set canJump to true.
            if (this.State == State.OnGround)
            {
                canJump = true;
            }

            if (canJump)
            {
                velocity.y = -jumpForce;
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