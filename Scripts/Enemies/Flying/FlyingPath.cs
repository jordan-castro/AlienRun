using Godot;
using System;
using System.Threading.Tasks;

namespace Enemy
{
    namespace Flying
    {
        public class FlyingPath : Path2D
        {
            private bool moving = true;
            private PathFollow2D pathFollow;
            private int direction = 1;

            public override void _Ready()
            {
                pathFollow = GetNode<PathFollow2D>("PathFollow2D");
            }

            // Called every frame. 'delta' is the elapsed time since the previous frame.
            public override void _Process(float delta)
            {
                if (moving)
                {
                    // Add 40 to the pathOffset
                    pathFollow.Offset += (40 * direction) * delta;

                    // Check if the unit offset is 1 or 0 i.e. the end of the path
                    if (pathFollow.UnitOffset == 0 || pathFollow.UnitOffset == 1)
                    {
                        // Reverse the direction
                        direction *= -1;
                        // Pause movement
                        PauseMoving();
                    }
                }
            }

            // Stop moving for a moment
            private async void PauseMoving()
            {
                moving = false;
                await Task.Delay(500);
                moving = true;
            }
        }
    }
}