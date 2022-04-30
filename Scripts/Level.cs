using Godot;
using System;
using Observers;

namespace AlienRun
{
    public class Level : Node2D
    {
        public Observers.Collision.Subject collision;
        public Observers.OffScreen.Subject offScreen;

        public override void _Ready()
        {
            var children = GetChildren();

            collision = new Observers.Collision.Subject();
            offScreen = new Observers.OffScreen.Subject(500);

            foreach (Node2D child in children)
            {
                // Check if Character.Node
                if (child is Character.Node)
                {
                    // The Player does not need to worry about this.
                    if (child is not Player.Base)
                    {
                        CollisionSetup(child as Character.Node);
                        OffScreenSetup(child as Character.Node);
                    }
                }
            }
        }

        public override void _Process(float delta)
        {
            collision.NotifyObservers();
            offScreen.NotifyObservers();
        }

        private void CollisionSetup(Character.Node child)
        {
            GD.Print("Child is: " + child.Name); // TODO: Remove this before production

            var observer = new Observers.Collision.Observer(child as Character.Node, collision);
        }

        private void OffScreenSetup(Character.Node child)
        {
            var observer = new Observers.OffScreen.Observer(child, offScreen);
        }
    }
}