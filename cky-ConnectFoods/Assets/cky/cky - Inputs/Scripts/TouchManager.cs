using cky.Reuseables.Extension;
using UnityEngine;
using System;

namespace cky.Inputs
{
    public class TouchManager : MonoBehaviour
    {
        public static event Action<GameObject> OnClick;
        public static event Action<GameObject> OnMove;
        public static event Action<GameObject> OnUp;

        [SerializeField] private LayerMask layerMask;
        Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            GetTouch();
        }

        #region Core

        private void GetTouch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Move(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Up(Input.mousePosition);
            }
        }

        private void Click(Vector3 mousePosition)
        {
            var objectTheMouseIsOn = _camera.GetGameObjectFromMousePosition(layerMask);

            OnClick?.Invoke(objectTheMouseIsOn);
        }

        private void Move(Vector3 mousePosition)
        {
            var objectTheMouseIsOn = _camera.GetGameObjectFromMousePosition(layerMask);

            OnMove?.Invoke(objectTheMouseIsOn);
        }

        private void Up(Vector3 mousePosition)
        {
            var objectTheMouseIsOn = _camera.GetGameObjectFromMousePosition(layerMask);

            OnUp?.Invoke(objectTheMouseIsOn);
        }

        #endregion
    }
}