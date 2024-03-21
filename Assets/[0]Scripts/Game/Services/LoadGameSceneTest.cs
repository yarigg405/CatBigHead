using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game
{
    internal sealed class LoadGameSceneTest : MonoBehaviour
    {
        private void Start()
        {
            if (LastSceneLoaderTest.LastSceneName == null || LastSceneLoaderTest.LastSceneName.Length == 0)
            {
                SceneManager.LoadScene("GameScene");
            }

            else
            {
                SceneManager.LoadScene(LastSceneLoaderTest.LastSceneName);
            }
        }
    }
}
