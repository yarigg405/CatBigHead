using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal class GameStartup : MonoBehaviour
    {
        [Inject] private readonly PlayerInput playerInput;
        [Inject] private readonly TickableProcessor tickableProcessor;
        [Inject] private readonly GameManager gameManager;
        [SerializeField] private PlayerSpawner playerSpawner;


        private void Start()
        {
            //TODO - REMOVE THIS ON RELEASE
#if UNITY_EDITOR
            if (!tickableProcessor)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
                return;
            }
#endif

            tickableProcessor.AddTickable(playerInput);
            playerSpawner.SpawnPlayer();

            gameManager.StartGame();
        }
    }
}
