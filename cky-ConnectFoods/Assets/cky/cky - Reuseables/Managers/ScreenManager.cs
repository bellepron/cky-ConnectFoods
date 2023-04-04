using UnityEngine;

namespace cky.Reuseables.Managers
{
    [RequireComponent(typeof(Camera))]
    public class ScreenManager : MonoBehaviour
    {
        private Camera cam;
        private float initOrthographicSize;
        private float orthographicSize;
        private float sizeDiff;

        [SerializeField] private Vector2 referenceResolution = new Vector2(1080, 1920);

        private void Awake()
        {
            PrepareCamera();
        }

        private void PrepareCamera()
        {
            cam = GetComponent<Camera>();
            SetSize();
            SetInitPosition();
        }

        private void SetSize()
        {
            float referenceAspect = referenceResolution.x / referenceResolution.y;
            float refToCurrentConstant = referenceAspect / cam.aspect;

            initOrthographicSize = cam.orthographicSize;
            orthographicSize = cam.orthographicSize * refToCurrentConstant;
            cam.orthographicSize = orthographicSize;

            sizeDiff = initOrthographicSize - orthographicSize;
        }

        private void SetInitPosition()
        {
            transform.position -= Vector3.up * sizeDiff;
        }
    }
}