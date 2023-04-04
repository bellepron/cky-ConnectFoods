using cky.Reuseables.Extension;
using UnityEngine;
using System;

namespace cky.Inputs
{
    public class TouchManager2D : MonoBehaviour
    {
        public static event Action<GameObject> HandleClick;
        public static event Action<GameObject> HandleMove;
        public static event Action<GameObject> HandleUp;

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
        {if (Input.GetMouseButtonUp(0))
            {
                Up(Input.mousePosition);
            }
           else if (Input.GetMouseButtonDown(0))
            {
                Click(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Move(Input.mousePosition);
            }
        }

        private void Click(Vector3 mousePosition)
        {
            var objectTheMouseIsOn = _camera.GetGameObjectFromMousePosition2D(layerMask);

            HandleClick?.Invoke(objectTheMouseIsOn);
        }

        private void Move(Vector3 mousePosition)
        {
            var objectTheMouseIsOn = _camera.GetGameObjectFromMousePosition2D(layerMask);

            HandleMove?.Invoke(objectTheMouseIsOn);
        }

        private void Up(Vector3 mousePosition)
        {
            var objectTheMouseIsOn = _camera.GetGameObjectFromMousePosition2D(layerMask);

            HandleUp?.Invoke(objectTheMouseIsOn);
        }

        #endregion
    }
}