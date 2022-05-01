using Godot;
using System;

namespace Observers
{
    namespace Collision
    {
        public class Observer : IObserver
        {
            private Character.Node character;

            // Constructor
            public Observer(Character.Node character, ISubject subject)
            {
                this.character = character;
                subject.RegisterObserver(this);
            }

            public bool ShouldRemove { get => !character.IsAlive; set => throw new NotImplementedException(); }

            public void Update(object data)
            {
                // Check for collisions
                if (character.GetSlideCount() > 0)
                {
                    for (int i = 0; i < character.GetSlideCount(); i++)
                    {
                        var collision = character.GetSlideCollision(i);

                        // Check if we are colliding with a Character.Node
                        if (collision.Collider is Character.Node)
                        {
                            CollideCharacter(collision.Collider as Character.Node);
                        }
                    }
                }
            }

            private void CollideCharacter(Character.Node character)
            {
                // Checking what type of collision we have
                if (character is Player.Base)
                {
                    CollidePlayerWithEnemy(character, this.character);
                }
                else if (this.character is Player.Base)
                {
                    CollidePlayerWithEnemy(this.character, character);
                }
                else if (character is Enemy.Base && this.character is Enemy.Base)
                {
                    CollideEnemyWithEnemy(this.character, character);
                }
            }

            // Fires when an enemy has collided with a enemy.
            private void CollideEnemyWithEnemy(Character.Node ch1, Character.Node ch2)
            {
                Enemy.Base enemy1 = ch1 as Enemy.Base;
                Enemy.Base enemy2 = ch2 as Enemy.Base;

                // Check if both are Rollers
                if (enemy1 is Enemy.Roller && enemy2 is Enemy.Roller)
                {
                    // Kill both
                    enemy1.Health = -1;
                    enemy2.Health = -1;
                }
            }

            // Fires when a player collides with an enemy.
            private void CollidePlayerWithEnemy(Character.Node ch1, Character.Node ch2)
            {
                // Cast
                Player.Base player = ch1 as Player.Base;
                Enemy.Base enemy = ch2 as Enemy.Base;

                // Did the player just stomp the enemy?
                if (enemy.PlayerIsOnTop() && enemy.Stompable)
                {
                    enemy.Health -= 1;
                }
                else
                {
                    if (enemy is Enemy.BadBlock)
                    {
                        enemy.Jump();

                        // So long as the player jumps before the block hits, the player is the one attacking.
                        if (player.velocity.y < enemy.velocity.y)
                        {
                            enemy.Health = -1;
                            return;
                        }   
                    }
                    player.Health -= 1;
                    player.TakeDamage(enemy.PlayerIsOnTop());

                    // Check that player is on top again to jump if jump is set.
                    if (enemy.PlayerIsOnTop() && enemy.JumpPlayer)
                    {
                        player.Jump();
                    }
                }
            }
        }
    }
}
