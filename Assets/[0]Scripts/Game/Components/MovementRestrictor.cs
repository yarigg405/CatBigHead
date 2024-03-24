using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class MovementRestrictor : MonoBehaviour, ITickable
    {
        private float _maxXCoordinate;
        private float _maxYCoordinate;
        private float _minXCoordinate;
        private float _minYCoordinate;
        [SerializeField] private float horizontalMaxDistance = 1;
        [SerializeField] private float verticalMaxDistance = 1;

        void ITickable.Tick(float deltaTime)
        {
            var coords = transform.position;
            coords.x = Mathf.Clamp(coords.x, _minXCoordinate, _maxXCoordinate);
            coords.y = Mathf.Clamp(coords.y, _minYCoordinate, _maxYCoordinate);

            transform.position = coords;
        }


        private void Start()
        {
            var mainCamera = Camera.main;

            var screenMinCoords = mainCamera.ScreenToWorldPoint(Vector3.zero);
            var screenMaxCoords = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            _minXCoordinate = screenMinCoords.x + horizontalMaxDistance;
            _maxXCoordinate = screenMaxCoords.x - horizontalMaxDistance;
            _minYCoordinate = screenMinCoords.y + verticalMaxDistance;
            _maxYCoordinate = screenMaxCoords.y - verticalMaxDistance;
        }
    }
}