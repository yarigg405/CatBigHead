using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using Yrr.Utils;


namespace Game
{
    internal sealed class LevelDurationTimer : MonoBehaviour, ITickable
    {
        [SerializeField] private float levelDuration;
        [Inject] private readonly TickableProcessor _processor;
        private float _currentTime;
        public ReactiveInt CurrentTime = new(0);


        private void Start()
        {
            _processor.AddTickable(this);
            _currentTime = levelDuration;
        }

        private void OnDestroy()
        {
            _processor.RemoveTickable(this);
        }

        void ITickable.Tick(float deltaTime)
        {
            _currentTime -= deltaTime;
            if (_currentTime < 0)
                _currentTime = 0;

            CurrentTime.Value = (int)_currentTime;
        }
    }
}
