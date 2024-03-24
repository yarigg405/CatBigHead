using System;
using Game.Entities;
using UnityEngine;


namespace Game.Components
{
    internal sealed class DamageDealComponent : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private TeamComponent myTeam;

        public event Action OnDamageDealt;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IEntity>(out var entity)) return;
            if (!entity.TryGetEntityComponent<TeamComponent>(out var otherTeam)) return;
            if (myTeam.Team == otherTeam.Team) return;
            if (!entity.TryGetEntityComponent<DamageReceiveComponent>(out var otherReceiver)) return;

            DealDamage(otherReceiver);
            OnDamageDealt?.Invoke();
        }


        private void DealDamage(DamageReceiveComponent receiver)
        {
            receiver.ReceiveDamage(damage);
        }
    }
}