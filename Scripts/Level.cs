using Godot;
using System;
using Observers.Collision;

namespace Level
{
    public class Level : Node2D
    {
        public Subject collision;

        public override void _Ready()
        {
            // Setup the collision "model"
            collision = new Subject();
            var children = GetChildren();

            foreach (Node2D child in children)
            {
                // Only charactes are added to the collision model
                if (child is Character.Node)
                {
                    GD.Print("Child is: " + child.Name); // TODO: Remove this before production

                    Observer observer = new Observer(child as Character.Node, collision);
                }
            }
        }

        public override void _Process(float delta)
        {
            collision.NotifyObservers();
        }
    }
}