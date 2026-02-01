using Tank.Utils.Input;
using Unity.Netcode;
using UnityEngine;

namespace Tank.Core.Player
{
    public class PlayerAiming : NetworkBehaviour
    {
        [SerializeField] private InputReader m_input;
        [SerializeField] private Transform m_turretTransform;

        private void LateUpdate()
        {
            if(!IsOwner) return;

            Vector2 aim_screen_position = m_input.AimPosition;
            Vector2 aim_world_position = Camera.main.ScreenToWorldPoint(aim_screen_position);

            m_turretTransform.up = new Vector2(
                aim_world_position.x - m_turretTransform.position.x,
                aim_world_position.y - m_turretTransform.position.y
            );
        }
    }
}