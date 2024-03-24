using Infrastructure.GameSystem;
using UnityEngine;

namespace Game
{
    internal sealed class PlayerInput : MonoBehaviour, ITickable
    {
        private Vector2 _inputData;
        public Vector2 InputData => _inputData;


        void ITickable.Tick(float deltaTime)
        {
            var horizontal = Input.GetAxis(Constants.Input.HorizontalAxis);
            var vertical = Input.GetAxis(Constants.Input.VerticalAxis);


            _inputData.x = horizontal;
            _inputData.y = vertical;
        }
    }
}