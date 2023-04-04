using ConnectFoods.Enums;
using System.Collections.Generic;

namespace ConnectFoods.Grid.Managers
{
    public class MatchManager
    {
        #region Preparing

        private readonly ICell[,] _grid;
        private readonly int _rows;
        private readonly int _cols;
        private readonly bool[,] _visitedCells;



        public MatchManager(ICell[,] grid)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
            _visitedCells = new bool[_rows, _cols];
        }

        #endregion



        #region Finding

        public List<ICell> FindMatchedNeighbours(ICell cell)
        {
            ResetVisitedCells();

            var matchType = cell.IsFull ? cell.IItem.MatchType : MatchType.None;
            var resultCells = new List<ICell>();

            FindMatchedNeighboursRecursive(cell, matchType, resultCells);

            return resultCells;
        }



        private void FindMatchedNeighboursRecursive(ICell cell, MatchType matchType, List<ICell> resultCells)
        {
            var row = cell.Row;
            var col = cell.Col;

            if (!IsCellVisited(row, col))
            {
                _visitedCells[row, col] = true;

                if (cell.IsFull &&
                    cell.Item.IsMatchableItem &&
                    cell.IItem.MatchType == matchType &&
                    !resultCells.Contains(cell))
                {
                    resultCells.Add(cell);
                    var neighbours = cell.Neighbours;

                    for (var i = 0; i < neighbours.Count; i++)
                    {
                        if (!IsCellVisited(neighbours[i].Row, neighbours[i].Col))
                        {
                            FindMatchedNeighboursRecursive(neighbours[i], matchType, resultCells);
                        }
                    }

                }
            }
        }

        #endregion



        #region Reset

        private void ResetVisitedCells()
        {
            for (int j = 0; j < _cols; j++)
                for (int i = 0; i < _rows; i++)
                    _visitedCells[i, j] = false;
        }

        #endregion



        #region Coditions

        private bool IsCellVisited(int row, int col) => _visitedCells[row, col];

        #endregion
    }
}