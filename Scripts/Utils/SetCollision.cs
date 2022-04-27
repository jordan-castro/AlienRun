using Godot;
using System;

namespace Utils
{
    public class SetCollision
    {
        public static uint PlayerLayer = 1;
        public static uint EnemyLayer = 2;
        public static uint TerrainLayer = 3;

        public static uint[] PlayerMask = new [] {
            EnemyLayer,
            TerrainLayer
        };
        public static uint[] EnemyMask = new [] {
            PlayerLayer,
            EnemyLayer,
            TerrainLayer
        };
        public static uint[] TerrainMask = new [] {
            PlayerLayer,
            EnemyLayer
        };

        public static void Set(Area2D area, string name)
        {
            uint layer;
            uint[] mask;

            switch (name)
            {
                case "Player":
                    layer = PlayerLayer;
                    mask = PlayerMask;
                    break;
                case "Enemy":
                    layer = EnemyLayer;
                    mask = EnemyMask;
                    break;
                case "Terrain":
                    layer = TerrainLayer;
                    mask = TerrainMask;
                    break;
                default:
                    throw new ArgumentException("Invalid name: " + name);
            }

            area.CollisionLayer = GetBitValue(new uint[] { layer });
            area.CollisionMask = GetBitValue(mask);
        }

        private static uint GetBitValue(uint[] values)
        {
            uint value = 0;
            for (int i = 0; i < values.Length; i++)
            {
                value += (uint)Math.Pow(2, values[i]);
            }

            return value;
        }
    }
}