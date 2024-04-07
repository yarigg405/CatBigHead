using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Entities
{
    internal class TickableEntity : Entity, ITickable
    {
        private ITickable[] _tickables;

        void ITickable.Tick(float deltaTime)
        {
            if (!isActiveAndEnabled) return;

            for (var i = 1; i < _tickables.Length; i++)
                _tickables[i].Tick(deltaTime);
        }

        internal void SetupEntity()
        {
            _tickables = GetComponentsInChildren<ITickable>(true);

            var childrenComponents = GetComponentsInChildren<IComponent>();
            foreach (var child in childrenComponents) AddEntityComponent(child);
        }
    }
}