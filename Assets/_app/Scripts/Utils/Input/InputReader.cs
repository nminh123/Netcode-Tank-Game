using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Tank.Utils.Input.Controls;

namespace Tank.Utils.Input
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        /// <summary>
        /// The move event, invoke the move event, returns a Vector2 direction.
        /// </summary>
        public event Action<Vector2> MoveEvent;
        /// <summary>
        /// Fire Event, invoke the event, returns true or false (a bool)
        /// </summary>
        public event Action<bool> PrimaryFireEvent;
        /// <summary>
        /// Aiming position, move with the cursor.
        /// </summary>
        public Vector2 AimPosition;
        
        /// input actions refs
        /// </summary>
        private Controls m_controls;

        private void OnEnable()
        {
            if(m_controls == null)
            {
                m_controls = new Controls();
                m_controls.Player.SetCallbacks(this);
            }    

            m_controls.Player.Enable();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            AimPosition = context.ReadValue<Vector2>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnPrimaryFire(InputAction.CallbackContext context)
        {
            if(context.performed)
                PrimaryFireEvent?.Invoke(true);
            else if (context.canceled)
                PrimaryFireEvent?.Invoke(false);
        }
    }
}