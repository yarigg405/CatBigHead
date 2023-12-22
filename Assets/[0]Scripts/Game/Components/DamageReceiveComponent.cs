using UnityEngine;


namespace Game.Components
{
    internal sealed class DamageReceiveComponent : MonoBehaviour, IComponent
    {
        [SerializeField] private HealthComponent health;

        internal void ReceiveDamage(int damage)
        {
            health.GetDamage(damage);
        }
    }
}
