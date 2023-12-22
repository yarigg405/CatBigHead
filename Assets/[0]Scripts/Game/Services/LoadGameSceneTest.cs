using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game
{
    internal sealed class LoadGameSceneTest : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
