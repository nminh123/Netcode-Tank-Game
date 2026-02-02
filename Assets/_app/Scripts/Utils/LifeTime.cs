using UnityEngine;

using static Tank.Utils.Configuration.Projectile;

namespace Tank.Utils
{
    public class LifeTime : MonoBehaviour
    {
        private void Start()
        {
            Destroy(this.gameObject, TimeToLive);
        }
    }
}