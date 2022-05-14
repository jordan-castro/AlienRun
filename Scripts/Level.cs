using Godot;
using System;
using Observers;
using System.Collections.Generic;

namespace AlienRun
{
    public class Level : Node2D
    {
        public Observers.Collision.Subject collision;
        public Observers.OffScreen.Subject offScreen;

        public override void _Ready()
        {
            var children = GetNodes();

            collision = new Observers.Collision.Subject();
            offScreen = new Observers.OffScreen.Subject(500);

            foreach (Node2D child in children)
            {
                // Check if Character.Node
                if (child is Character.Node)
                {
                    CollisionSetup(child as Character.Node);
                    // Player does not worry about being off the screen
                    if (!(child is Player.Base))
                    {
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

        public void CollisionSetup(Character.Node child)
        {
            GD.Print("Child is: " + child.Name); // TODO: Remove this before production

            var observer = new Observers.Collision.Observer(child as Character.Node, collision);
        }

        private void OffScreenSetup(Character.Node child)
        {
            var observer = new Observers.OffScreen.Observer(child, offScreen);
        }
    
        // Get the player and enemies of the level
        private List<Node2D> GetNodes()
        {
            var player = GetNode<Node2D>("Player");
            var enemies = GetNode<Node2D>("Enemies").GetChildren();

            var children = new List<Node2D> { player };

            // Must add this way because of the way the compiler works
            foreach (Node2D child in enemies)
            {
                children.Add(child);
            }

            return children;
        }
    }
}