using System;
using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class OutOfScreenDestroyer : MonoBehaviour, ITickable
    {
        private float _maxXCoordinate;
        private float _maxYCoordinate;
        private float _minXCoordinate;
        private float _minYCoordinate;

        [Space][SerializeField] private Entity controlledEntity;

        [SerializeField] private float outOfScreenMaxX = 2f;
        [SerializeField] private float outOfScreenMaxY = 1f;
        [SerializeField] private float outOfScreenMinX = -1f;
        [SerializeField] private float outOfScreenMinY = -1f;

        void ITickable.Tick(float deltaTime)
        {
            var coords = transform.position;

            if (coords.x > _maxXCoordinate) DestroyObject();
            if (coords.y > _maxYCoordinate) DestroyObject();
            if (coords.x < _minXCoordinate) DestroyObject();
            if (coords.y < _minYCoordinate) DestroyObject();

        }

        private void DestroyObject()
        {
            controlledEntity.GetEntityComponent<DestroyComponent>().Destroy();
        }

        private void Awake()
        {
            var mainCamera = Camera.main;
            if (!mainCamera) throw new Exception("No camera on scene!");

            var screenMinCoords = mainCamera.ScreenToWorldPoint(Vector3.zero);
            var screenMaxCoords = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            _minXCoordinate = screenMinCoords.x + outOfScreenMinX;
            _maxXCoordinate = screenMaxCoords.x + outOfScreenMaxX;
            _minYCoordinate = screenMinCoords.y + outOfScreenMinY;
            _maxYCoordinate = screenMaxCoords.y + outOfScreenMaxY;
        }
    }
}