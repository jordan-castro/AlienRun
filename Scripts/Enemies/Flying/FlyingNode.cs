using Godot;
using System;

namespace Enemy
{
    namespace Flying
    {
        public class FlyingNode : Base
        {
            public override void _Ready()
            {
                base._Ready();
                velocity.x = 0;
            }

            protected override void PhysicsProcess(float delta)
            {
                LoadSprite();

                if (!IsAlive)
                {
                    base.PhysicsProcess(delta);
                }
            }
        }
    }
}
