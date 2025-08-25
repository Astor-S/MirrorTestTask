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
            if(isOwned == false)
                return;
            
            Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
            Command(move);
        }

        [Command]
        private void Command(Vector3 move) =>
            RPCMovement(move);

        [ClientRpc]
        private void RPCMovement(Vector3 move) =>
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
    }
}