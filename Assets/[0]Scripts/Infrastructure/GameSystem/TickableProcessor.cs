using System;
using System.Collections.Generic;
using UnityEngine;


namespace Infrastructure.GameSystem
{
    public sealed class TickableProcessor : MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener
    {
        private readonly LinkedList<ITickable> _tickables = new();
        private readonly List<ITickable> _tickablesForRemoving = new();

        private readonly LinkedList<IFixedTickable> _fixedTickables = new();
        private readonly List<IFixedTickable> _fixedTickablesForRemoving = new();

        private bool _isGameActive = false;


        public void AddTickable(ITickable tickable)
        {
            _tickables.AddLast(tickable);
        }

        public void RemoveTickable(ITickable tickable)
        {
            _tickablesForRemoving.Add(tickable);
        }


        public void AddFixedTickable(IFixedTickable tickable)
        {
            _fixedTickables.AddLast(tickable);
        }

        public void RemoveFixedTIckable(IFixedTickable tickable)
        {
            _fixedTickablesForRemoving.Add(tickable);
        }


        private void Update()
        {
            if (!_isGameActive) return;

            HandleRemovingTickables();
            HandleTick();
        }

        private void HandleTick()
        {
            var deltaTime = Time.deltaTime;
            foreach (var tick in _tickables)
            {
                tick.Tick(deltaTime);
            }
        }

        private void HandleRemovingTickables()
        {
            if (_tickablesForRemoving.Count > 0)
            {
                foreach (var remove in _tickablesForRemoving)
                {
                    _tickables.Remove(remove);
                }

                _tickablesForRemoving.Clear();
            }
        }


        private void FixedUpdate()
        {
            if (!_isGameActive) return;

            HandleRemovingFixedTickables();
            HandleFixedTick();
        }

        private void HandleFixedTick()
        {
            var deltaTime = Time.fixedDeltaTime;
            foreach (var tick in _fixedTickables)
            {
                tick.FixedTick(deltaTime);
            }
        }

        private void HandleRemovingFixedTickables()
        {
            if (_fixedTickablesForRemoving.Count > 0)
            {
                foreach (var remove in _fixedTickablesForRemoving)
                {
                    _fixedTickables.Remove(remove);
                }

                _tickablesForRemoving.Clear();
            }
        }


        void IGameStartListener.OnGameStart()
        {
            _isGameActive = true;
        }

        void IGameFinishListener.OnGameFinish()
        {
            _isGameActive = false;
        }

        void IGamePauseListener.OnGamePaused()
        {
            _isGameActive = false;
        }

        void IGamePauseListener.OnGameUnPaused()
        {
            _isGameActive = true;
        }
    }
}
