using UnityEngine;

namespace ConnectFoods.Grid
{
    [RequireComponent(typeof(LineRenderer))]
    public class BondManager : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            ResetBond();

            GridManager.OnFirstCellSelected += Add;
            GridManager.OnCellConnected += Add;
            GridManager.OnConnectedCellsReset += ResetBond;
        }

        public void ResetBond()
        {
            _lineRenderer.positionCount = 0;
        }

        public void Add(ICell addedCell)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, addedCell.GetPosition());
        }
    }
}