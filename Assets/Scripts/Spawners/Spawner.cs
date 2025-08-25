using UnityEngine;
using Mirror;
using PlayerComponents;
using SpawnableObjects;

namespace Spawners
{
    public class Spawner : NetworkBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private SpawnableCube _cubePrefab;
        [SerializeField] private float _spawnDistance = 2f;

        private void OnEnable()
        {
            _playerInput.SpawnCubePressed += SpawnCube;
        }

        private void OnDisable()
        {
            _playerInput.SpawnCubePressed -= SpawnCube;
        }

        private void SpawnCube()
        {
            if (_cubePrefab == null)
                return;

            CmdSpawnCube();
        }

        [Command]
        private void CmdSpawnCube()
        {
            SpawnableCube cube = Instantiate(_cubePrefab, transform.position + transform.forward * _spawnDistance, Quaternion.identity);
            NetworkServer.Spawn(cube.gameObject);
        }
    }
}