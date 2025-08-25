using UnityEngine;
using Mirror;

namespace PlayerComponents
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _moveSpeed = 5f;

        private void OnEnable()
        {
            _playerInput.Moving += Move;
        }

        private void OnDisable()
        {
            _playerInput.Moving -= Move;
        }

        private void Move(float horizontalInput, float verticalInput)
        {
            Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
        }
    }
}