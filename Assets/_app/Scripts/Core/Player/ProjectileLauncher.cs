using Tank.Core.Combat;
using Tank.Utils.Input;
using Unity.Netcode;
using UnityEngine;

using static Tank.Utils.Configuration.Projectile;

namespace Tank.Core.Player
{
    public class ProjectileLauncher : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader m_input;
        [SerializeField] private Transform m_projectileSpawnPoint;
        [SerializeField] private GameObject m_serverProjectilePrefab, m_clientProjectilePrefab;
        [SerializeField] private GameObject m_muzzleFlash;
        [SerializeField] private Collider2D m_playerCollider;

        //Settings
        private float m_projectileSpeed;
        private float m_fireRate;
        private float m_muzzleFlashDuration;

        private bool m_shouldFire;
        private float m_previousFireTime;
        private float m_muzzleFlashTimer;

        private void Awake()
        {
            m_projectileSpeed = PROJECTILE_SPEED;
            m_fireRate = FIRE_RATE;
            m_muzzleFlashDuration = MUZZLE_FLASH_DURATION;
        }

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;
            m_input.PrimaryFireEvent += HandlePrimaryFire;
        }

        private void Update()
        {
            if(m_muzzleFlashTimer > 0f)
            {
                m_muzzleFlashTimer -= Time.deltaTime;

                if(m_muzzleFlashTimer <= 0f)
                {
                    m_muzzleFlash.SetActive(false);
                }
            }
            if(!IsOwner) return;
            if(!m_shouldFire) return;
            if(Time.time < (1 / m_fireRate) + m_previousFireTime) return;

            PrimaryFireServerRpc(m_projectileSpawnPoint.position, m_projectileSpawnPoint.up);
            SpawnDummyProjectile(m_projectileSpawnPoint.position, m_projectileSpawnPoint.up);
            m_previousFireTime = Time.time;
        }

        private void HandlePrimaryFire(bool should_fire)
        {
            m_shouldFire = should_fire;
        }

        private void SpawnDummyProjectile(Vector3 spawn_pos, Vector3 direction)
        {
            m_muzzleFlash.SetActive(true);
            m_muzzleFlashTimer = m_muzzleFlashDuration;

            GameObject projectile_instance = Instantiate(
                m_clientProjectilePrefab, 
                spawn_pos, 
                Quaternion.identity);

            projectile_instance.transform.up = direction;
            Physics2D.IgnoreCollision(m_playerCollider, projectile_instance.GetComponent<Collider2D>());
            if(projectile_instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = rb.transform.up * m_projectileSpeed;
            }
        }

        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
        private void PrimaryFireServerRpc(Vector3 spawn_pos, Vector3 direction)
        {
            GameObject projectile_instance = Instantiate(
                m_serverProjectilePrefab, 
                spawn_pos, 
                Quaternion.identity);

            projectile_instance.transform.up = direction;
            Physics2D.IgnoreCollision(m_playerCollider, projectile_instance.GetComponent<Collider2D>());
            if(projectile_instance.TryGetComponent<DealDamageOnContact>(out DealDamageOnContact d))
            {
                d.SetOwner(this.OwnerClientId);
            }
            SpawnDummyProjectileClientRpc(spawn_pos, direction);
        }

        [Rpc(SendTo.Owner, InvokePermission = RpcInvokePermission.Owner)]
        private void SpawnDummyProjectileClientRpc(Vector3 spawn_pos, Vector3 direction)
        {
            if(IsOwner) return;
            SpawnDummyProjectile(spawn_pos, direction);
        }

        public override void OnNetworkDespawn()
        {
            if(!IsOwner) return;
            m_input.PrimaryFireEvent -= HandlePrimaryFire;
        }
    }
}