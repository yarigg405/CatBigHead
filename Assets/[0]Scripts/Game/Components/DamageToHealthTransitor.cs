using UnityEngine;


namespace Game.Components
{
    internal sealed class DamageToHealthTransitor : MonoBehaviour
    {
        [SerializeField] private HealthComponent health;
        [SerializeField] private DamageReceiveComponent damageReceiver;

        private void Awake()
        {
            damageReceiver.OnDamageReceived += TransiteDamage;
        }

        private void OnDestroy()
        {
            if (damageReceiver)
                damageReceiver.OnDamageReceived -= TransiteDamage;
        }

        private void TransiteDamage(int dmg)
        {
            health.GetDamage(dmg);
        }
    }
}
