using Infrastructure.GameSystem;
using UnityEngine;
using UnityEngine.Events;


namespace Game
{
    internal sealed class ToPointAndReturnMover : MonoBehaviour, ITickable
    {
        private float _currentDelay;

        private ToPointAndReturnState _state;
        [SerializeField] private float delayToReturn;
        [SerializeField] private float moveSpeed;
        [SerializeField] private UnityEvent onDelayAwaited;

        [SerializeField] private UnityEvent onSpawned;
        [SerializeField] private UnityEvent onTargetReached;
        [SerializeField] private float targetPosX;


        void ITickable.Tick(float deltaTime)
        {
            switch (_state)
            {
                case ToPointAndReturnState.Move:
                    {
                        Move(deltaTime);
                    }
                    break;

                case ToPointAndReturnState.Return:
                    {
                        Return(deltaTime);
                    }
                    break;

                case ToPointAndReturnState.Await:
                    {
                        Await(deltaTime);
                    }
                    break;
            }
        }


        private void OnEnable()
        {
            transform.rotation = Quaternion.identity;
            onSpawned?.Invoke();
            _state = ToPointAndReturnState.Move;
            _currentDelay = delayToReturn;
        }

        private void Move(float deltaTime)
        {
            transform.position -= deltaTime * moveSpeed * Vector3.right;

            if (transform.position.x > targetPosX) return;
            onTargetReached?.Invoke();
            _state = ToPointAndReturnState.Await;
        }

        private void Await(float deltaTime)
        {
            _currentDelay -= deltaTime;

            if (_currentDelay > 0) return;

            onDelayAwaited?.Invoke();
            _state = ToPointAndReturnState.Return;

        }

        private void Return(float deltaTime)
        {
            var tr = transform;
            var delta = Vector3.right * deltaTime * moveSpeed;
            tr.position += delta;
        }

        private enum ToPointAndReturnState
        {
            Move,
            Await,
            Return
        }
    }
}