using Game.Player;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Game
{
    internal sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerEntity playerPrefab;
        [SerializeField] private Transform spawnRoot;
        [SerializeField] Vector3 spawnPosition;

        [Inject] private readonly IObjectResolver _resolver;
        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly TickableProcessor _tickableProcessor;
       


        internal void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, spawnRoot);
            _resolver.InjectGameObject(player.gameObject);
            _tickableProcessor.AddTickable(player);

            _playerProvider.Player = player;
        }
    }
}
