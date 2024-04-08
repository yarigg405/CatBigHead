using Infrastructure.GameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game
{
    internal class GameStartup : MonoBehaviour
    {
        [Inject] private readonly GameMachine _gameMachine;
        [Inject] private readonly PlayerInput _playerInput;
        [Inject] private readonly TickableProcessor _tickableProcessor;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private GameplayModificationsSystem gameplayModificationsSystem;


        private void Awake()
        {
            //TODO - REMOVE THIS ON RELEASE
#if UNITY_EDITOR
            if (!_tickableProcessor)
            {
                LastSceneLoaderTest.LastSceneName = gameObject.scene.name;
                SceneManager.LoadScene("StartScene");
                return;
            }
#endif

            _tickableProcessor.AddTickable(_playerInput);
            playerSpawner.SpawnPlayer();
            gameplayModificationsSystem.SetupSystem();

            _gameMachine.StartGame();
        }
    }
}