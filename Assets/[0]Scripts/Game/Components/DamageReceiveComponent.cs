using System;
using UnityEngine;

namespace Game.Components
{
    internal sealed class DamageReceiveComponent : MonoBehaviour, IComponent
    {
        public event Action<int> OnDamageReceived;

        internal void ReceiveDamage(int damage)
        {
            OnDamageReceived?.Invoke(damage);
        }
    }
}