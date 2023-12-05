using Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Infrastructure.DI
{
    internal sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerInput playerInput;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(playerInput);
        }
    }
}
