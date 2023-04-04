using cky.Inputs;
using cky.Reuseables.Extension;
using ConnectFoods.Grid.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectFoods.Grid.Managers
{
    [RequireComponent(typeof(LineRenderer))]
    public class BondManager : MonoBehaviour
    {
        [SerializeField] private float maxLineLength = 1.4f;
        private LineRenderer _lineRenderer;
        private Camera _cam;



        private void Start()
        {
            _cam = Camera.main;
            _lineRenderer = GetComponent<LineRenderer>();

            ResetBond();

            SubscribeEvents();
        }



        private void Set(List<ICell> connectedCells)
        {
            var length = connectedCells.Count;
            _lineRenderer.positionCount = length + 1;

            for (int i = 0; i < length; i++)
            {
                _lineRenderer.SetPosition(i, connectedCells[i].GetPosition());
            }

            UpdateLineRendererLastPosition();
        }



        public void ResetBond()
        {
            _lineRenderer.positionCount = 0;
        }



        public void Add(ICell addedCell)
        {
            if (_lineRenderer.positionCount == 0)
            {
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, addedCell.GetPosition());
            }
            else
            {
                _lineRenderer.positionCount++;

                _lineRenderer.SetPosition(_lineRenderer.positionCount - 2, addedCell.GetPosition());
            }

            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _cam.ScreenToWorldPoint(Input.mousePosition));
        }



        private void OnMove(GameObject obj)
        {
            UpdateLineRendererLastPosition();
        }



        private void UpdateLineRendererLastPosition()
        {
            if (_lineRenderer.positionCount < 2) return;

            var lastAddedPos = _lineRenderer.GetPosition(_lineRenderer.positionCount - 2);
            var targetPos = lastAddedPos.ClampedPositionRelativeToTheCursorInWorldSpace(_cam, 0, maxLineLength);
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, targetPos);
        }



        #region Event

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            Grid_StateMachine.OnCellConnected += Add;
            Grid_StateMachine.OnConnectedCellsUpdate += Set;
            Grid_StateMachine.OnConnectedCellsReset += ResetBond;

            TouchManager2D.HandleMove += OnMove;
        }

        private void UnsubscribeEvents()
        {
            Grid_StateMachine.OnCellConnected -= Add;
            Grid_StateMachine.OnConnectedCellsUpdate -= Set;
            Grid_StateMachine.OnConnectedCellsReset -= ResetBond;

            TouchManager2D.HandleMove -= OnMove;
        }

        #endregion
    }
}