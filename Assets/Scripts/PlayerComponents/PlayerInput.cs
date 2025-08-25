using System;
using UnityEngine;
using Mirror;

namespace PlayerComponents
{
    public class PlayerInput : NetworkBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        [SerializeField] private KeyCode _messageKey = KeyCode.Space;
        [SerializeField] private KeyCode _spawnCubeKey = KeyCode.F;

        public event Action<float, float> Moving;
        public event Action MessageKeyPressed;
        public event Action SpawnCubePressed;

        private void Update()
        {
            if (isLocalPlayer == false)
                return;

            float horizontalInput = Input.GetAxis(HorizontalAxis);
            float verticalInput = Input.GetAxis(VerticalAxis);

            if (horizontalInput != 0 || verticalInput != 0)
                Moving?.Invoke(horizontalInput, verticalInput); 

            if (Input.GetKeyDown(_messageKey))
                MessageKeyPressed?.Invoke();

            if (Input.GetKeyDown(_spawnCubeKey))
                SpawnCubePressed?.Invoke();
        }
    }
}