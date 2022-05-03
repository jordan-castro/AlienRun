using Godot;
using System;

namespace Enviroment
{
    namespace Blocks
    {
        public class Base : StaticBody2D
        {
            private RayCast2D bottomCast;
            private Sprite onSprite;
            private Sprite offSprite;

            [Export]
            public bool isActive;

            public override void _Ready()
            {
                bottomCast = GetNode<RayCast2D>("BottomCast");
                onSprite = GetNode<Sprite>("OnSprite");
                offSprite = GetNode<Sprite>("OffSprite");

                // Change the sprite depending on the isActive value set from the editor
                ChangeSprite();
            }


            public override void _PhysicsProcess(float delta)
            {
                base._PhysicsProcess(delta);

                // Check if the raycast hit the player
                if (bottomCast.IsColliding() && bottomCast.GetCollider() is Player.Base)
                {
                    OnHitByPlayer(bottomCast.GetCollider() as Player.Base);
                }
            }

            /// <summary>
            /// Fires when the block is hit from the bottom by a player.
            /// </summary>
            protected virtual void OnHitByPlayer(Player.Base player)
            {
                ChangeSprite();
            }

            // Change the sprite depending on the state of the block
            private void ChangeSprite()
            {
                if (isActive)
                {
                    onSprite.Visible = true;
                    offSprite.Visible = false;
                }
                else
                {
                    onSprite.Visible = false;
                    offSprite.Visible = true;
                }
            }
        }
    }
}