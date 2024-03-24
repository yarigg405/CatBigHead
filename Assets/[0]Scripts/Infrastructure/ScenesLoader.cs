using UnityEngine;
using UnityEngine.SceneManagement;


namespace Infrastructure.ScenesLoading
{
    internal sealed class ScenesLoader : MonoBehaviour
    {
        [SerializeField] private string menuSceneName = "MenuScene";

        internal void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(menuSceneName);
        }
    }
}