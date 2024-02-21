using Game.Components;
using Infrastructure.GameSystem;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    internal sealed class PlayerHealthBar : MonoBehaviour, ITickable
    {
        [SerializeField] private HealthComponent playerHealth;
        [SerializeField] private Transform healthParent;
        [SerializeField] private GameObject healthPrefab;
        [SerializeField] private float healthObjectsSpeed = 6;

        private readonly float _deltaX = 0.5f;
        private readonly List<GameObject> _healths = new();


        private void Start()
        {
            var hp = playerHealth.MaxHealth;

            for (int i = 0; i < hp; i++)
            {
                var hpOb = Instantiate(healthPrefab);
                hpOb.transform.position = GetCoordinates(i);
                hpOb.GetComponent<SpriteRenderer>().sortingOrder = 2 - i;

                _healths.Add(hpOb);
            }

            playerHealth.OnDamageReceived += OnDamageReceived;
        }

        private void OnDestroy()
        {
            playerHealth.OnDamageReceived -= OnDamageReceived;
        }


        private Vector3 GetCoordinates(int i)
        {
            float x = healthParent.position.x;
            float y = healthParent.position.y;
            float z = healthParent.position.z;

            Vector3 result = new Vector3(x - _deltaX * (i + 1.75f), y, z);
            return result;
        }


        private void OnDamageReceived()
        {
            var current = _healths[_healths.Count - 1];
            current.SetActive(false);
            _healths.Remove(current);
        }


        void ITickable.Tick(float deltaTime)
        {
            for (int i = 0; i < _healths.Count; i++)
            {
                var dest = GetCoordinates(i);
                float spd =
                    (Vector3.Distance(transform.position, _healths[i].transform.position) - i * 2) +
                    healthObjectsSpeed;

                _healths[i].transform.position =
                    Vector3.Lerp(_healths[i].transform.position, dest, spd * deltaTime);
            }
        }
    }
}
