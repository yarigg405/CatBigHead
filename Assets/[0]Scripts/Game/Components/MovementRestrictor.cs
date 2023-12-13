using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class MovementRestrictor : MonoBehaviour, ITickable
    {
        [SerializeField] private float horizontalMaxDistance = 1;
        [SerializeField] private float verticalMaxDistance = 1;

        private float _maxXcoordinate;
        private float _minXcoordinate;
        private float _maxYcoordinate;
        private float _minYcoordinate;


        private void Start()
        {
            var screenMinCoords = Camera.main.ScreenToWorldPoint(Vector3.zero);
            var screenMaxCoords = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            _minXcoordinate = screenMinCoords.x + horizontalMaxDistance;
            _maxXcoordinate = screenMaxCoords.x - horizontalMaxDistance;
            _minYcoordinate = screenMinCoords.y + verticalMaxDistance;
            _maxYcoordinate = screenMaxCoords.y - verticalMaxDistance;
        }

        void ITickable.Tick(float deltaTime)
        {
            var coords = transform.position;
            coords.x = Mathf.Clamp(coords.x, _minXcoordinate, _maxXcoordinate);
            coords.y = Mathf.Clamp(coords.y, _minYcoordinate, _maxYcoordinate);

            transform.position = coords;
        }
    }
}
