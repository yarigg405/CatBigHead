using Game.Player;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerEntity playerPrefab;
        [SerializeField] private Transform spawnRoot;
        [SerializeField] Vector3 spawnPosition;

        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly PlayerInput _playerInput;
        [Inject] private readonly TickableProcessor _tickableProcessor;


        internal void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, spawnRoot);
            player.Construct(_playerInput, _tickableProcessor);


            _playerProvider.Player = player;
        }
    }
}
