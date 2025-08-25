using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace PlayerComponents
{
    public class PlayerSetup : NetworkBehaviour
    {
        private const string DefaultPlayerPrefix = "Player";

        [SerializeField] private TextMesh _playerNameText;
        [SerializeField] private InputField _nameInputField;

        [SyncVar(hook = nameof(OnNameChanged))]
        private string _playerName;

        public string PlayerName => _playerName;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            if (isLocalPlayer)
            {
                _nameInputField.gameObject.SetActive(true);
                _nameInputField.onEndEdit.AddListener(HandleNameInputEnd);
            }
        }

        private void HandleNameInputEnd(string enteredName)
        {
            if (isLocalPlayer && _nameInputField != null && _nameInputField.IsActive())
            {
                string nameToSet;

                int minPlayerNumber = 100;
                int maxPlayerNumber = 999;

                if (string.IsNullOrEmpty(enteredName))
                    nameToSet = DefaultPlayerPrefix + Random.Range(minPlayerNumber, maxPlayerNumber);
                else
                    nameToSet = enteredName;

                CmdSetPlayerName(nameToSet);

                _nameInputField.gameObject.SetActive(false);
                _nameInputField.onEndEdit.RemoveListener(HandleNameInputEnd);
            }
        }

        [Command]
        private void CmdSetPlayerName(string name) =>
            _playerName = name;

        private void OnNameChanged(string _, string newName) =>
            _playerNameText.text = newName;
    }
}