using UnityEngine;

namespace Tank.Utils
{
    public class DestroySelfOnContact : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(this.gameObject);
        }
    }
}