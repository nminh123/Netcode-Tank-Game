using Unity.Netcode;
using UnityEngine;

using static Tank.Utils.Configuration.Player;

namespace Tank.Core.Combat
{
    public class DealDamageOnContact : MonoBehaviour
    {
        private ulong m_ownerClientId;
        public void SetOwner(ulong owner_client_id)
        {
            m_ownerClientId = owner_client_id;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.attachedRigidbody == null) return;

            if(collision.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject no))
            {
                if(m_ownerClientId == no.OwnerClientId) return;
                
            }

            if(collision.attachedRigidbody.TryGetComponent<Health>(out Health h))
            {
                h.TakeDamage(damage_value: DAMAGE);
            }
        }
    }
}