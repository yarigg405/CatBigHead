using UnityEngine;


namespace Infrastructure.LoadingPipeline
{
    public sealed class GameLoader : MonoBehaviour
    {
        [SerializeField] private LoadingTask[] loadingTasks;

        private void Start()
        {
            foreach (var task in loadingTasks)
            {
                task.Do();
            }
        }
    }
}
