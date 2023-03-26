using System;
using System.Collections.Generic;
using ConnectFoods.Enums;
using ConnectFoods.Items;
using ConnectFoods.Managers;
using cky.Inputs;
using cky.Reuseables.Extension;
using cky.Reuseables.Level;
using UnityEngine;

namespace ConnectFoods.Grid
{
    public class GridManager : MonoBehaviour
    {
        public static event Action<ICell> OnFirstCellSelected;
        public static event Action<ICell> OnCellConnected;
        public static event Action OnConnectedCellsReset;

        private ItemType _collectedItemType;
        private List<ICell> _lastConnectedCellNeighbours;
        private List<ICell> _connectedCells = new List<ICell>();

        private LevelSettings _levelSettings;
        private GridCreator _gridCreator;
        private ItemCreator _itemCreator;
        private ICell[,] _grid;

        private void Start()
        {
            _levelSettings = LevelManager.Instance.levelSettings;

            _gridCreator = new GridCreator(_levelSettings.GridSettings);
            _grid = _gridCreator.GetGrid();
            _itemCreator = new ItemCreator(_grid, _levelSettings);

            TouchManager2D.HandleClick += HandleClick;
            TouchManager2D.HandleMove += HandleMove;
            TouchManager2D.HandleUp += HandleUp;
        }

        private void HandleClick(GameObject clickedGameObject)
        {
            if (clickedGameObject == null) return;

            if (clickedGameObject.TryGetComponent<ICell>(out var cell))
            {
                if (CellHasItem(cell))
                {
                    AddConnectedCell(cell);

                    _collectedItemType = cell.IItem.ItemType;

                    OnFirstCellSelected?.Invoke(cell);
                }
            }
        }

        private void HandleMove(GameObject objectTheCursorIsOn)
        {
            if (objectTheCursorIsOn == null) return;

            if (objectTheCursorIsOn.TryGetComponent<ICell>(out var cell))
            {
                if (IsCellConnected(cell) == false)
                {
                    if (IsLastConnectedCellNeighbours(cell))
                    {
                        if (IsEqualToCollectedItemType(cell))
                        {
                            AddConnectedCell(cell);

                            OnCellConnected?.Invoke(cell);
                        }
                    }
                }
            }
        }

        private List<ICell> CellNeighbours(ICell cell) => _grid.GetNeighboursInGrid(cell);

        private void HandleUp(GameObject obj)
        {
            if (_connectedCells.Count >= 3)
            {
                foreach (var cell in _connectedCells)
                {
                    cell.Explode();
                }
            }
            else
            {
                foreach (var cell in _connectedCells)
                {
                    cell.DeSelected();
                }
            }

            _connectedCells.Clear();
            OnConnectedCellsReset?.Invoke();
            _collectedItemType = ItemType.None;
        }

        private void AddConnectedCell(ICell cell)
        {
            cell.Selected();
            _connectedCells.Add(cell);
            _lastConnectedCellNeighbours = CellNeighbours(cell);
        }

        #region Conditions

        private bool CellHasItem(ICell cell) => cell.Item == null ? false : true;

        private bool IsCellConnected(ICell cell) => _connectedCells.Contains(cell);

        private bool IsLastConnectedCellNeighbours(ICell cell) => _lastConnectedCellNeighbours.Contains(cell);

        private bool IsEqualToCollectedItemType(ICell cell) => _collectedItemType == cell.IItem?.ItemType ? true : false;

        #endregion
    }
}
