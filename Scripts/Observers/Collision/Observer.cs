using Godot;
using System;

namespace Observer
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

            public void Update(object data)
            {
                // Check for collisions
                var collisionCount = character.GetSlideCount();

                for (int i = 0; i < collisionCount; i++)
                {
                    var collision = character.GetSlideCollision(i);

                    // Check if we are colliding with a Character.Node
                    if (collision.Collider is Character.Node)
                    {
                        CollideCharacter(collision.Collider as Character.Node);
                    }
                }
            }

            private void CollideCharacter(Character.Node character)
            {
                // Check if one is a PLayer
                if (this.character is Player.Base)
                {
                    CollidePlayerWithEnemy(this.character, character);
                } else if (character is Player.Base)
                {
                    CollidePlayerWithEnemy(character, this.character);
                }
            }

            private void CollidePlayerWithEnemy(Character.Node ch1, Character.Node ch2)
            {
                // Cast
                Player.Base player = ch1 as Player.Base;
                Enemy.Base enemy = ch2 as Enemy.Base;

                // If the enemy is on the ceiling this means the player has stomped on the enemy
                if (enemy.IsOnCeiling())
                {
                    enemy.Health -= player.AttackPower;
                }
                else
                {
                    // The player has been attacked
                    player.Health -= enemy.AttackPower;
                }
            }
        }
    }
}