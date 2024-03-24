using Infrastructure.GameSystem;
using Infrastructure.LoadingPipeline;
using VContainer;


namespace Game.LoadingTasks
{
    internal sealed class TickableInitTask : LoadingTask
    {
        [Inject] private readonly GameMachine _gameMachine;
        [Inject] private readonly TickableProcessor _tickableProcessor;

        internal override void DoTask()
        {
            _gameMachine.AddListener(_tickableProcessor);
        }
    }
}