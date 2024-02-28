using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal class GameStartup : MonoBehaviour
    {
        [Inject] private readonly PlayerInput _playerInput;
        [Inject] private readonly TickableProcessor _tickableProcessor;
        [Inject] private readonly GameManager _gameManager;
        [SerializeField] private PlayerSpawner playerSpawner;


        private void Awake()
        {
            //TODO - REMOVE THIS ON RELEASE
#if UNITY_EDITOR
            if (!_tickableProcessor)
            {
                LastSceneLoaderTest.LastSceneName = gameObject.scene.name;
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
                return;
            }
#endif

            _tickableProcessor.AddTickable(_playerInput);
            playerSpawner.SpawnPlayer();

            _gameManager.StartGame();
        }
    }
}
