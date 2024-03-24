using System.Collections.Generic;
using UnityEngine;


namespace Infrastructure.GameSystem
{
    public sealed class GameMachine : MonoBehaviour
    {
        private readonly List<IGameListener> _gameListeners = new(30);
        public GameState GameState { get; private set; }


        public void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            _gameListeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null) return;

            _gameListeners.Remove(listener);
        }


        public void StartGame()
        {
            GameState = GameState.Play;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGameStartListener stListener) stListener.OnGameStart();
            }
        }

        [ContextMenu("Pause")]
        public void PauseGame()
        {
            GameState = GameState.Pause;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGamePauseListener pListener) pListener.OnGamePaused();
            }
        }

        [ContextMenu("UnPause")]
        public void UnPauseGame()
        {
            GameState = GameState.Play;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGamePauseListener pListener) pListener.OnGameUnPaused();
            }
        }

        public void FinishGame()
        {
            GameState = GameState.Finish;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGameFinishListener fListener) fListener.OnGameFinish();
            }
        }
    }

    public enum GameState
    {
        Off,
        Play,
        Pause,
        Finish
    }
}