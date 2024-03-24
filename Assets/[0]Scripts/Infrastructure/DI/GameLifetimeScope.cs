using Infrastructure.GameSystem;
using Infrastructure.ScenesLoading;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    internal sealed class GameLifetimeScope : LifetimeScope
    {
        [FormerlySerializedAs("gameManager")] [SerializeField] private GameMachine gameMachine;
        [SerializeField] private ScenesLoader scenesLoader;
        [SerializeField] private TickableProcessor tickableProcessor;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(gameMachine);
            builder.RegisterComponent(tickableProcessor);
            builder.RegisterComponent(scenesLoader);
        }
    }
}