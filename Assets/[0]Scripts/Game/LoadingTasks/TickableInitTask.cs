using Infrastructure.GameSystem;
using Infrastructure.LoadingPipeline;
using VContainer;


namespace Game.LoadingTasks
{
    internal sealed class TickableInitTask : LoadingTask
    {
        [Inject] private readonly TickableProcessor _tickableProcessor;
        [Inject] private readonly GameManager _gameManager;

        internal override void Do()
        {
            _gameManager.AddListener(_tickableProcessor);
        }
    }
}
