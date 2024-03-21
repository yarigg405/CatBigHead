using Game.Player;
using Infrastructure.GameSystem;
using System.Collections;
using UnityEngine;
using VContainer;


namespace Game.Components
{
    internal sealed class LookToPlayerComponent : MonoBehaviour, ITickable
    {
        [SerializeField] private bool lookOnlyOnStart;
        private Transform _playerTransform;

        [Inject]
        private void Construct(PlayerEntity player)
        {
            _playerTransform = player.transform;
        }

        private void OnEnable()
        {
            if (lookOnlyOnStart)
                StartCoroutine(SetPlayerDirectionDelay());
        }

        private IEnumerator SetPlayerDirectionDelay()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            Rotate();
        }

        void ITickable.Tick(float deltaTime)
        {
            if (!enabled) return;
            if (lookOnlyOnStart) return;
            Rotate();
        }

        private void Rotate()
        {
            float y = GetAngle() - 90;
            transform.rotation = Quaternion.Euler(0, 0, y);
        }

        private float GetAngle()
        {
            var p1 = transform.position;
            var p2 = _playerTransform.position;

            if (p1 == p2) return 0;

            Vector2 curPos = p1 - p2;
            float cos = curPos.y / Mathf.Sqrt((curPos.x * curPos.x + curPos.y * curPos.y));
            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
            if (curPos.x > 0) angle *= -1;

            return angle + 180;
        }
    }
}
