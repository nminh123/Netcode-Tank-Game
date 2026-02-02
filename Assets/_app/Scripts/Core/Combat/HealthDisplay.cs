using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Tank.Core.Combat
{
    public class HealthDisplay : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Health m_health;
        [SerializeField] private Image m_healthBarImage;

        public override void OnNetworkSpawn()
        {
            if(!IsClient) return;

            m_health.CurrentHealth.OnValueChanged += HandleHealthChanged;
            HandleHealthChanged(0, m_health.CurrentHealth.Value);
        }

        public override void OnNetworkDespawn()
        {
            if(!IsClient) return;

            m_health.CurrentHealth.OnValueChanged -= HandleHealthChanged;
        }

        public void HandleHealthChanged(int old_health, int new_health) => m_healthBarImage.fillAmount = (float)new_health / m_health.MaxHealth;
    }
}