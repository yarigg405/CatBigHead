using UnityEngine;


namespace Yrr.Utils
{
    internal sealed class SimpleFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - target.position;
        }

        private void Update()
        {
            transform.position = target.transform.position + _offset;
        }
    }
}
