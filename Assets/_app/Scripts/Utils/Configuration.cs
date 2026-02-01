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
            public static float PlayerMovementSpeed { get; } = 4f;
            public static float TurningRate { get; } = 270f;
        }
    }
}