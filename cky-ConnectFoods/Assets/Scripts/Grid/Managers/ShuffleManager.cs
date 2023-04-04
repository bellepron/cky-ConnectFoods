using cky.Reuseables.Extension;
using ConnectFoods.Item;
using DG.Tweening;

namespace ConnectFoods.Grid.Managers
{
    public class ShuffleManager
    {
        private readonly ICell[,] _grid;
        private readonly int _rows;
        private readonly int _cols;



        public ShuffleManager(ICell[,] grid)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
        }



        public void Shuffle()
        {
            var cachedItems = new ItemAbstract[_rows, _cols];

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    var cell = _grid[row, col];

                    if (cell.Item == null) continue;

                    cachedItems[row, col] = cell.Item;
                }
            }

            cachedItems.ShuffleMatrix();

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    var cell = _grid[row, col];
                    cell.Item = cachedItems[row, col];

                    if (cell.IsFull)
                    {
                        cachedItems[row, col].transform.DOMove(cell.GetPosition(), 1);
                    }
                }
            }
        }
    }
}