using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;

namespace Game
{
    internal sealed class PlayerDeathObserver : MonoBehaviour
    {
        [Inject] private readonly GameMachine _gameMachine;
        [Inject] private readonly PlayerProvider _playerProvider;

        private void Start()
        {
            var player = _playerProvider.Player;
            player.GetEntityComponent<HealthComponent>().OnDeath += PlayerDie;
        }

        private void OnDestroy()
        {
            var player = _playerProvider.Player;
            player.GetEntityComponent<HealthComponent>().OnDeath -= PlayerDie;
        }

        private void PlayerDie()
        {
            Debug.Log("PlayerDie!!");
            _gameMachine.PauseGame();
        }
    }
}