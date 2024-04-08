using Game.Player;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Game
{
    internal sealed class PlayerSpawner : MonoBehaviour
    {
        [Inject] private readonly PlayerProvider _playerProvider;

        [Inject] private readonly IObjectResolver _resolver;
        [Inject] private readonly TickableProcessor _tickableProcessor;
        [SerializeField] private PlayerEntity playerPrefab;
        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private Transform spawnRoot;


        internal void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, spawnRoot);
            player.SetupEntity();
            _resolver.InjectGameObject(player.gameObject);
            _tickableProcessor.AddTickable(player);

            _playerProvider.Player = player;
        }
    }
}