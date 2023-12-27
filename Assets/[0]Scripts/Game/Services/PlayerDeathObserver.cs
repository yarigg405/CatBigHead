using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using Game.Components;


namespace Game
{
    internal sealed class PlayerDeathObserver : MonoBehaviour
    {
        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly GameManager _gameManager;

        private void Start()
        {
            var player = _playerProvider.Player;
            player.Get<HealthComponent>().OnDeath += PlayerDie;
        }

        private void OnDestroy()
        {
            var player = _playerProvider.Player;
            player.Get<HealthComponent>().OnDeath -= PlayerDie;
        }

        private void PlayerDie()
        {
            Debug.Log("PlayerDie!!");
            _gameManager.PauseGame();
        }
    }
}
