using Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Infrastructure.DI
{
    internal sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ShootingSystem shootingSystem;
        [SerializeField] private EffectsSytem effectsSytem;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(playerInput);
            builder.RegisterComponent(shootingSystem);
            builder.RegisterComponent(effectsSytem);
            builder.Register<PlayerProvider>(Lifetime.Scoped);
        }
    }
}