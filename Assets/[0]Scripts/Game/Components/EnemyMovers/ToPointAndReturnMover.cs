using Infrastructure.GameSystem;
using UnityEngine;
using UnityEngine.Events;


namespace Game
{
    internal sealed class ToPointAndReturnMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float targetPosX;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float delayToReturn;
        private float _currentDelay;

        [SerializeField] private UnityEvent onSpawned;
        [SerializeField] private UnityEvent onTargetReached;
        [SerializeField] private UnityEvent onDelayAwaited;

        private ToPointAndReturnState _state;



        private void OnEnable()
        {
            transform.rotation = Quaternion.identity;
            onSpawned?.Invoke();
            _state = ToPointAndReturnState.Move;
            _currentDelay = delayToReturn;
        }


        void ITickable.Tick(float deltaTime)
        {
            switch (_state)
            {
                case ToPointAndReturnState.Move:
                    {
                        Move(deltaTime);
                    }; break;

                case ToPointAndReturnState.Return:
                    {
                        Return(deltaTime);
                    }; break;

                case ToPointAndReturnState.Await:
                    {
                        Await(deltaTime);
                    }; break;
            }
        }

        private void Move(float deltaTime)
        {
            transform.position = transform.position - Vector3.right * deltaTime * moveSpeed;

            if (transform.position.x <= targetPosX)
            {
                onTargetReached?.Invoke();
                _state = ToPointAndReturnState.Await;
            }
        }

        private void Await(float deltaTime)
        {
            _currentDelay -= deltaTime;

            if (_currentDelay <= 0)
            {
                onDelayAwaited?.Invoke();
                _state = ToPointAndReturnState.Return;
            }
        }

        private void Return(float deltaTime)
        {
            transform.position = transform.position + Vector3.right * deltaTime * moveSpeed;
        }

        private enum ToPointAndReturnState
        {
            Move,
            Await,
            Return,
        }
    }
}
