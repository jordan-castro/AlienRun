using Godot;
using System;

namespace Character
{
    public enum State
    {
        OnGround,
        InAir
    }

    public class Node : KinematicBody2D
    {
        public int jumpForce = 300;

        /// <summary>
        /// Can the character currently jump?
        /// </summary>
        public bool canJump = false;

        /// <summary>
        /// The current state of the character.
        /// </summary>
        public State state { get; private set; }

        /// <summary>
        /// Does this Character slide or collide when moving?
        /// </summary>
        public bool slide = true;

        public Vector2 velocity = new Vector2();

        public int attackPower = 10;

        public int health = 100;

        public float speed = 200;

        public bool IsDead { get { return health <= 0; } }

        public override void _Ready()
        {
            base._Ready();
            Die();
        }

        /// <summary>
        /// Classes that inherit from Character.Node should override this method if they want to 
        /// run during _PhysicsProcess.
        /// </summary>
        protected virtual void PhysicsProcess(float delta)
        {
            CheckCharacterState();
            ApplyGravity(delta);
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            PhysicsProcess(delta);
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
            if (state == State.InAir)
            {
                velocity.y += delta * -9.8f;
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
                state = State.OnGround;
            }
            // If the character is not on the ground, set the state to InAir.
            else
            {
                state = State.InAir;
            }
        }

        /// <summary>
        /// Move the Character horizontally.
        /// </summary>
        /// <param name="movement">How to move the Character</param>
        /// <param name="delta">The delta time.</param>
        public void MoveH(float movement, float delta)
        {
            velocity.x = movement * delta;
            if (slide)
            {
                MoveAndSlide(velocity, new Vector2(0, -1));
            }
            else
            {
                MoveAndCollide(velocity);
            }
        }

        /// <summary>
        /// This gets called when the Character dies.
        /// </summary>
        protected virtual async void Die()
        {
            // Rotate the character
            Rotate(180);

            // Remove the CollisionShape
            RemoveChild(GetNode<CollisionShape2D>("CollisionShape2D"));

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
        /// Jump the Player. This can be overridden by sub Player classes.
        /// </summary>   
        protected virtual void Jump()
        {
            // If player state = OnGround, set canJump to true.
            if (state == State.OnGround)
            {
                canJump = true;
            }

            if (canJump)
            {
                velocity.y = -jumpForce;
                canJump = false;
            }
        }
    }
}