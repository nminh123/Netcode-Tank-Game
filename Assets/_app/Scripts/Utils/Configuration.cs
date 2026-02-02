using UnityEngine;

namespace Tank.Utils
{
    public class Configuration
    {
        /// <summary>
        /// Configuration của player
        /// </summary>
        public struct Player
        {
            public static float PLAYER_MOVEMENT_SPEED { get; } = 4f;
            public static float TURNING_RATE { get; } = 270f;
            public static int MAX_HEALTH { get; } = 100;
            public static int DAMAGE { get; } = 5;
        }

        /// <summary>
        /// Configuration của Projectile
        /// </summary>
        public struct Projectile
        {
            public static float TIME_TO_LIVE { get; } = 1f;
            public static float PROJECTILE_SPEED { get; } = 30f;
            public static float FIRE_RATE { get; } = 0.75f;
            public static float MUZZLE_FLASH_DURATION { get; } = 0.075f;
        }
    }
}