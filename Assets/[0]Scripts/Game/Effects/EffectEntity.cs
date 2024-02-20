using Game.Components;
using Game.Entities;


namespace Game.Fx
{
    internal sealed class EffectEntity : TickableEntity
    {
        internal void Construct()
        {
            var destroy = new DestroyComponent();
            Add(destroy);
        }

        private void OnDestroy()
        {
            Get<DestroyComponent>().Clear();
        }
    }
}
