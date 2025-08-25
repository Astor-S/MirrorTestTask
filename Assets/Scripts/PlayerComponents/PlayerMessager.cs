using UnityEngine;
using Mirror;

namespace PlayerComponents
{
    public class PlayerMessager : NetworkBehaviour
    {
        [SerializeField] private PlayerSetup _playerSetup;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private string _playerMessageText = "Привет от ";

        private void OnEnable()
        {
            _playerInput.MessageKeyPressed += SendMessage;
        }

        private void OnDisable()
        {
            _playerInput.MessageKeyPressed -= SendMessage;
        }

        private void SendMessage() =>
            CmdSendMessage(_playerMessageText + _playerSetup.PlayerName);

        [Command]
        private void CmdSendMessage(string message) =>
            RpcReceiveMessage(message);

        [ClientRpc]
        private void RpcReceiveMessage(string message) =>
            Debug.Log(message);
    }
}