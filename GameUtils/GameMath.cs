using System;

using Microsoft.Xna.Framework;

namespace GameProject.GameUtils
{
    public class GameMath
    {
        static Random random = new Random();

        // Approach a value
        public static float Approach(float value, float target, float speed)
        {
            if (value == target) return target;
            if (target > value)
                return (value + speed > target) ? target : value + speed;
            else
                return (value - speed < target) ? target : value - speed;
        }

        // LErp a value
        public static float Lerp(float value, float target, float speed)
        {
            return value + (target - value) * speed;
        }

        // LErp a vector
        public static Vector2 Lerp(Vector2 value, Vector2 target, float speed)
        {
            value.X = Lerp(value.X, target.X, speed);
            value.Y = Lerp(value.Y, target.Y, speed);
            return value;
        }

        // Random in range
        public static int RandomRange(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
