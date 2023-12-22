using Game.Entities;
using System;
using UnityEngine;


namespace Game.Components
{
    internal sealed class DamageDealComponent : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private TeamComponent myTeam;

        public event Action OnDamageDealed;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IEntity>(out var entity))
            {
                if (entity.TryGet<TeamComponent>(out var otherTeam))
                {
                    if (myTeam.Team != otherTeam.Team)
                    {
                        if (entity.TryGet<DamageReceiveComponent>(out var otherReceiver))
                        {
                            DealDamage(otherReceiver);
                            OnDamageDealed?.Invoke();
                        }
                    }
                }
            }
        }

        private void DealDamage(DamageReceiveComponent receiver)
        {
            receiver.ReceiveDamage(damage);
        }
    }
}
