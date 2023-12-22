using System;
using UnityEngine;


namespace Game
{
    internal sealed class HealthComponent : MonoBehaviour
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        internal int CurrentHealth { get; private set; }

        public event Action OnDamageReceived;
        public event Action OnDeath;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        internal void GetDamage(int damage)
        {
            CurrentHealth -= damage;
            OnDamageReceived?.Invoke();

            if (CurrentHealth <= 0)
                OnDeath?.Invoke();
        }
    }
}
