using System.Collections.Generic;

namespace ConnectFoods.Grid.Managers
{
    public class HintManager
    {
        private readonly ICell[,] _grid;
        private readonly int _rows;
        private readonly int _cols;
        private readonly MatchManager _matchFinder;

        private readonly bool[,] _visitedCells;
        private readonly List<ICell> _hintedCells = new List<ICell>();



        public HintManager(ICell[,] grid, MatchManager matchFinder)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
            _matchFinder = matchFinder;
            _visitedCells = new bool[_rows, _cols];
        }



        public bool IsThereAnyHint(int minMatchLimit)
        {
            ResetVisitedCells();

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    var cell = _grid[row, col];

                    if (!_visitedCells[row, col] && cell.IsFull)
                    {
                        var matchedCells = FindMatchedCells(cell);

                        if (cell.IItem.IsMatchableItem)
                        {
                            if (matchedCells.Count >= minMatchLimit)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }



        public void GiveHint(int minMatchLimit)
        {
            ResetVisitedCells();

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    var cell = _grid[row, col];

                    if (!_visitedCells[row, col] || cell.IsFull)
                    {
                        var matchedCells = FindMatchedCells(cell);

                        if (cell.IItem.IsMatchableItem)
                        {
                            GiveHintForOrdinaryItems(matchedCells, minMatchLimit);
                        }
                    }
                }
            }
        }



        private List<ICell> FindMatchedCells(ICell cell)
        {
            var matchedCells = _matchFinder.FindMatchedNeighbours(cell);

            foreach (var item in matchedCells)
            {
                _visitedCells[item.Row, item.Col] = true;
            }

            return matchedCells;
        }



        private void GiveHintForOrdinaryItems(List<ICell> matchedCells, int minMatchLimit)
        {
            if (matchedCells.Count < minMatchLimit) return;

            foreach (var cell in matchedCells)
            {
                cell.Selected();

                _hintedCells.Add(cell);
            }
        }



        #region Reset

        private void ResetVisitedCells()
        {
            for (int j = 0; j < _cols; j++)
                for (int i = 0; i < _rows; i++)
                    _visitedCells[i, j] = false;
        }

        public void ResetHints()
        {
            foreach (var cell in _hintedCells)
            {
                cell.DeSelected();
            }

            _hintedCells.Clear();
        }

        #endregion
    }
}