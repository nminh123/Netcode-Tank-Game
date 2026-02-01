using Tank.Utils.Input;
using Unity.Netcode;
using UnityEngine;

using static Tank.Utils.Configuration.Player;

namespace Tank.Core.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader m_input;
        [SerializeField] private Transform m_bodyTransform;
        [SerializeField] private Rigidbody2D m_rb;

        private float m_movementSpeed;
        private float m_turningRate;

        private Vector2 m_previousMovementInput;

        void Awake()
        {
            m_movementSpeed = PlayerMovementSpeed;
            m_turningRate = TurningRate;    
        }

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;
            m_input.MoveEvent += MoveHandle;
        }

        private void Update()
        {
            if (!IsOwner) return;

            float z_rotation = m_previousMovementInput.x * -m_turningRate * Time.deltaTime;
            m_bodyTransform.Rotate(0f, 0f, z_rotation);
        }

        private void FixedUpdate()
        {
            if(!IsOwner) return;

            m_rb.linearVelocity = (Vector2)m_bodyTransform.up * m_previousMovementInput.y * m_movementSpeed;
        }

        public override void OnNetworkDespawn()
        {
            if(!IsOwner) return;
            m_input.MoveEvent -= MoveHandle;
        }

        private void MoveHandle(Vector2 move_input)
        {
            m_previousMovementInput = move_input;
        }
    }
}