using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class OutOfScreenDestroyer : MonoBehaviour, ITickable
    {
        [SerializeField] private float outOfScreenMinX = -1f;
        [SerializeField] private float outOfScreenMaxX = 2f;
        [SerializeField] private float outOfScreenMinY = -1f;
        [SerializeField] private float outOfScreenMaxY = 1f;

        private float _maxXcoordinate;
        private float _minXcoordinate;
        private float _maxYcoordinate;
        private float _minYcoordinate;

        [Space]
        [SerializeField] private Entity controlledEntity;

        private void Awake()
        {
            var screenMinCoords = Camera.main.ScreenToWorldPoint(Vector3.zero);
            var screenMaxCoords = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            _minXcoordinate = screenMinCoords.x + outOfScreenMinX;
            _maxXcoordinate = screenMaxCoords.x + outOfScreenMaxX;
            _minYcoordinate = screenMinCoords.y + outOfScreenMinY;
            _maxYcoordinate = screenMaxCoords.y + outOfScreenMaxY;
        }

        void ITickable.Tick(float deltaTime)
        {
            var coords = transform.position;

            if (coords.x > _maxXcoordinate ||
                coords.y > _maxYcoordinate ||
                coords.x < _minXcoordinate ||
                coords.y < _minYcoordinate)
            {
                controlledEntity.Get<DestroyComponent>().Destroy();
            }
        }
    }
}
