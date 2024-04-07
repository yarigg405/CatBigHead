using Game.Components;
using Game.Entities;


namespace Game.Fx
{
    internal sealed class EffectEntity : TickableEntity
    {
        internal void Construct()
        {
            var destroy = new DestroyComponent();
            AddEntityComponent(destroy);
        }

        private void OnDestroy()
        {
            GetEntityComponent<DestroyComponent>().Clear();
        }
    }
}