using Godot;
using System;

namespace Enemy
{
    public class Needle : Base
    {
        public override void _Ready()
        {
            base._Ready();
            Stompable = false;
        }
    }
}