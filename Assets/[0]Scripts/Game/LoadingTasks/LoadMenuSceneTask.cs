using Infrastructure.LoadingPipeline;
using Infrastructure.ScenesLoading;
using VContainer;


namespace Game.LoadingTasks
{
    internal sealed class LoadMenuSceneTask : LoadingTask
    {
        [Inject] private readonly ScenesLoader _scenesLoader;

        internal override void Do()
        {
            _scenesLoader.LoadMenuScene();
        }
    }
}
