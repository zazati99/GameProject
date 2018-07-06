using System;

namespace GameProject.GameUtils
{
    public class GameMath
    {

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

    }
}
