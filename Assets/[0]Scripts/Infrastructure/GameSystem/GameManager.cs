using System.Collections.Generic;
using UnityEngine;


namespace Infrastructure.GameSystem
{
    public sealed class GameManager : MonoBehaviour
    {
        private readonly List<IGameListener> _gameListeners = new(30);
        public GameState GameState => _gameState;
        private GameState _gameState;


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
            _gameState = GameState.PLAY;

            for (int i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGameStartListener stListener)
                {
                    stListener.OnGameStart();
                }
            }
        }

        public void PauseGame()
        {
            _gameState = GameState.PAUSE;

            for (int i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGamePauseListener pListener)
                {
                    pListener.OnGamePaused();
                }
            }
        }

        public void UnPauseGame()
        {
            _gameState = GameState.PLAY;

            for (int i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGamePauseListener pListener)
                {
                    pListener.OnGameUnPaused();
                }
            }
        }

        public void FinishGame()
        {
            _gameState = GameState.FINISH;

            for (int i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGameFinishListener fListener)
                {
                    fListener.OnGameFinish();
                }
            }
        }
    }

    public enum GameState
    {
        OFF,
        PLAY,
        PAUSE,
        FINISH,
    }
}
