using Godot;
using System;

namespace Observers
{
    namespace OffScreen
    {
        public class Observer : IObserver
        {
            private Character.Node character;
            public bool ShouldRemove { get; set; } = false;

            // Constructor
            public Observer(Character.Node character, ISubject subject)
            {
                this.character = character;
                subject.RegisterObserver(this);
            }

            public void Update(object data)
            {
                // Check if the character is dead
                if (character.Health <= 0) 
                {
                    // Grab bottom from data
                    int bottom = (int)data;

                    if (character.Position.y > bottom)
                    {
                        // Kill and remove from scene
                        character.Health = 0;
                        character.QueueFree();
                        // We should remove from the subject list
                        ShouldRemove = true;
                    }
                }
            }
        }
    }
}