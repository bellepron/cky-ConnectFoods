using UnityEngine;
using ConnectFoods.Managers;
using cky.Reuseables.Level;

namespace ConnectFoods.Grid.Managers
{
    public class FallManager
    {
        private readonly ICell[,] _grid;
        private readonly int _rows;
        private readonly int _cols;
        private readonly LevelSettings _levelSettings;

        public bool IsFallable => _fallingItemCount == 0 ? false : true;
        private int _fallingItemCount = 0;



        public FallManager(ICell[,] grid)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
            _levelSettings = LevelManager.Instance.LevelSettings;
        }



        public void Fall()
        {
            var maxFallSpeed = _levelSettings.MaxFallSpeed;
            var acceleration = _levelSettings.FallAcceleration;
            var deltaTime = Time.deltaTime;

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    var cell = _grid[row, col];

                    if (cell.IsFull)
                    {
                        var cellItem = cell.Item;
                        var firstCellBelow = cell.FirstCellBelow;

                        if (cellItem.IsFallableItem && firstCellBelow != null && !firstCellBelow.IsFull)
                        {
                            firstCellBelow.Item = cellItem;
                            cell.Item = null;
                        }

                        cellItem.FallTo(cell, maxFallSpeed, acceleration, deltaTime);
                    }
                }
            }
        }
    }
}