using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game
{
    internal sealed class LoadGameSceneTest : MonoBehaviour
    {
        private void Start()
        {
            var loadSceneName = "GameScene";

            if (LastSceneLoaderTest.LastSceneName == null)
            {
                SceneManager.LoadScene(loadSceneName);
                return;
            }

            if (LastSceneLoaderTest.LastSceneName.Length == 0)
            {
                SceneManager.LoadScene(loadSceneName);
                return;
            }

            SceneManager.LoadScene(LastSceneLoaderTest.LastSceneName);
        }
    }
}