using System.Collections;
using Game.Player;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game.Components
{
    internal sealed class LookToPlayerComponent : MonoBehaviour, ITickable
    {
        private Transform _playerTransform;
        [SerializeField] private bool lookOnlyOnStart;

        void ITickable.Tick(float deltaTime)
        {
            if (!enabled) return;
            if (lookOnlyOnStart) return;
            Rotate();
        }

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

        private void Rotate()
        {
            var y = GetAngle() - 90;
            transform.rotation = Quaternion.Euler(0, 0, y);
        }

        private float GetAngle()
        {
            var p1 = transform.position;
            var p2 = _playerTransform.position;

            if (p1 == p2) return 0;

            Vector2 curPos = p1 - p2;
            var squareLenght = Mathf.Pow(curPos.x, 2) * Mathf.Pow(curPos.y, 2);
            var cos = curPos.y / Mathf.Sqrt(squareLenght);
            var angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
            if (curPos.x > 0) angle *= -1;

            return angle + 180;
        }
    }
}