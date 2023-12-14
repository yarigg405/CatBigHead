using System;
using System.Collections.Generic;
using UnityEngine;


namespace Infrastructure.GameSystem
{
    public sealed class TickableProcessor : MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener
    {
        private readonly LinkedList<ITickable> _tickables = new();
        private readonly List<ITickable> _ticlablesForAdding = new();
        private readonly List<ITickable> _tickablesForRemoving = new();
        

        private readonly LinkedList<IFixedTickable> _fixedTickables = new();
        private readonly List<IFixedTickable> _fixedTickablesForAdding = new();
        private readonly List<IFixedTickable> _fixedTickablesForRemoving = new();

        private bool _isGameActive = false;


        public void AddTickable(ITickable tickable)
        {
            _ticlablesForAdding.Add(tickable);
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

            HandleAddingTickales();
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

        private void HandleAddingTickales()
        {
            for (int i = 0; i < _ticlablesForAdding.Count; i++)
            {
                _tickables.AddLast(_ticlablesForAdding[i]);
            }

            _ticlablesForAdding.Clear();
        }

        private void HandleRemovingTickables()
        {
            for (int i = 0; i < _tickablesForRemoving.Count; i++)
            {
                _tickables.Remove(_tickablesForRemoving[i]);
            }

            _tickablesForRemoving.Clear();
        }


        private void FixedUpdate()
        {
            if (!_isGameActive) return;

            HandleAddingFixedTickables();
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

        private void HandleAddingFixedTickables()
        {
            for (int i = 0; i < _fixedTickablesForAdding.Count; i++)
            {
                _fixedTickables.AddLast(_fixedTickablesForAdding[i]);
            }

            _fixedTickablesForAdding.Clear();
        }

        private void HandleRemovingFixedTickables()
        {
            for (int i = 0; i < _fixedTickablesForRemoving.Count; i++)
            {
                _fixedTickables.Remove(_fixedTickablesForRemoving[i]);
            }

            _tickablesForRemoving.Clear();
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
