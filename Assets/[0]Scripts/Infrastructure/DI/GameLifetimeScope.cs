using Game;
using Infrastructure.GameSystem;
using Infrastructure.ScenesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Infrastructure.DI
{
    internal sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TickableProcessor tickableProcessor;
        [SerializeField] private ScenesLoader scenesLoader;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(gameManager);
            builder.RegisterComponent(tickableProcessor);
            builder.RegisterComponent(scenesLoader);
        }
    }
}
