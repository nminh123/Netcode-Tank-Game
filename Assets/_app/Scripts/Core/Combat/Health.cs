using System;
using Unity.Netcode;
using UnityEngine;

using static Tank.Utils.Configuration.Player;

namespace Tank.Core.Combat
{
    public class Health : NetworkBehaviour
    {
        public Action<Health> OnDie;
        private int m_maxHealth = MAX_HEALTH;
        public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>();

        private bool m_isDead;

        public override void OnNetworkSpawn()
        {
            if(!IsServer) return;
            CurrentHealth.Value = m_maxHealth;
        }

        public void TakeDamage(int damage_value)
        {
            ModifyHealth(-damage_value);
        }

        public void RestoreHealth(int health_value)
        {
            ModifyHealth(health_value);
        }

        private void ModifyHealth(int val)
        {
            if(m_isDead) return;
            int new_health = CurrentHealth.Value + val;
            CurrentHealth.Value = Mathf.Clamp(new_health, 0, m_maxHealth);
            
            if(CurrentHealth.Value > 0)
            {
                OnDie?.Invoke(this);
                m_isDead = true;
            }
        }
    }
}