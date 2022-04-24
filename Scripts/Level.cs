using Godot;
using System;
using Observer.Collision;

namespace Level
{
    public class Level : Node2D
    {
        private Subject collision;

        public override void _Ready()
        {
            // Setup the collision
            collision = new Subject();
            var children = GetChildren();

            foreach (Node2D child in children)
            {
                if (child is Character.Node)
                {
                    GD.Print("Child is: " + child.Name);

                    Observer.Collision.Observer observer = new Observer.Collision.Observer(child as Character.Node, collision);
                }
            }
        }

        public override void _Process(float delta)
        {
            collision.NotifyObservers();
        }
    }
}