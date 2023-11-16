using UnityEngine;


namespace Game
{
    internal sealed class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool useCameraForward;
        [SerializeField] private bool flat;
        [SerializeField] private bool inverted;
        private Transform _mainCam;


        void Update()
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main.transform;
            }
            Vector3 Dir = Vector3.zero;
            if (useCameraForward)
            {
                Dir = _mainCam.forward;
            }
            else
            {
                Dir = _mainCam.transform.position - transform.position;
            }
            if (flat)
            {
                Dir.y = 0;
            }
            transform.rotation = Quaternion.LookRotation(inverted ? -Dir : Dir);
        }
    }
}
