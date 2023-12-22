using Game.Components;
using Infrastructure.GameSystem;


namespace Game.Entities
{
    internal class TickableEntity : Entity, ITickable
    {
        private ITickable[] _tickables;

        protected virtual void Awake()
        {
            _tickables = GetComponentsInChildren<ITickable>();

            var childrenComponents = GetComponentsInChildren<IComponent>();
            foreach (var child in childrenComponents)
            {
                Add(child);
            }
        }

        void ITickable.Tick(float deltaTime)
        {
            if (!isActiveAndEnabled) return;

            for (int i = 1; i < _tickables.Length; i++)
            {
                _tickables[i].Tick(deltaTime);
            }
        }
    }
}
