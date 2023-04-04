using cky.Reuseables.Extension;
using cky.Reuseables.Level;
using ConnectFoods.Item;
using ConnectFoods.Managers;
using UnityEngine;

namespace ConnectFoods.Grid.Managers
{
    public class FillManager
    {
        private ICell[,] _grid;
        private int _rows;
        private int _cols;
        private ItemCreator _itemCreator;
        private LevelSettings _levelSettings;

        private float _fillingStartOffsetY;
        private float _increaseQuantityY;
        private ICell[] _topCells;



        public FillManager(ICell[,] grid, ItemCreator itemCreator)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
            _itemCreator = itemCreator;
            _levelSettings = LevelManager.Instance.LevelSettings;

            _fillingStartOffsetY = _grid[_rows - 1, 0].GetPosition().y + 2;
            _increaseQuantityY = _levelSettings.CellSize + _levelSettings.CellSpacing;

            GetTopCells();
        }



        private void GetTopCells()
        {
            _topCells = new ICell[_cols];

            for (int i = 0; i < _cols; i++)
            {
                _topCells[i] = _grid[_rows - 1, i];
            }
        }



        public void Fill()
        {
            var fallSpeed = _levelSettings.MaxFallSpeed * Time.deltaTime;

            for (int col = 0; col < _cols; col++)
            {
                var cell = _topCells[col];

                if (!cell.IsFull)
                {
                    var initPos = cell.GetPosition();
                    var initPosY = _fillingStartOffsetY;

                    if (cell.FirstCellBelow.IsFull && cell.FirstCellBelow.Item.transform.position.y + _increaseQuantityY > _fillingStartOffsetY)
                    {
                        initPosY = cell.FirstCellBelow.Item.transform.position.y + _increaseQuantityY;
                    }

                    initPos.y = initPosY;

                    cell.Item = _itemCreator.CreateItem(_levelSettings.ItemTypesInThisLevel.RandomFromArray(), cell, initPos);
                }
            }
        }
    }
}