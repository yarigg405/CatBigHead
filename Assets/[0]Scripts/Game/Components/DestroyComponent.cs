using System;

namespace Game.Components
{
    internal class DestroyComponent
    {
        public event Action OnDestroy;

        internal virtual void Destroy()
        {
            OnDestroy?.Invoke();
        }

        internal void Clear()
        {
            OnDestroy = null;
        }
    }
}