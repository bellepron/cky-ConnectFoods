using CKY.Pooling;
using UnityEngine;

namespace ConnectFoods.Grid
{
    public class GridCreator
    {
        private string _cellPrefabDirectory;
        private Transform _cellPrefabTr;

        private int _rows;
        private int _columns;
        private float _cellSize;
        private float _cellSpacing;

        private Vector3 _gridCenter;
        private ICell[,] _grid;

        public GridCreator(GridSettings gridSettings)
        {
            _cellPrefabDirectory = gridSettings.CellPrefabDirectory;
            _cellPrefabTr = Resources.Load<Transform>(_cellPrefabDirectory);
            _rows = gridSettings.Rows;
            _columns = gridSettings.Columns;
            _cellSize = gridSettings.CellSize;
            _cellSpacing = gridSettings.CellSpacing;

            Create();
        }

        private void Create()
        {
            _grid = new ICell[_rows, _columns];

            _gridCenter = new Vector3((_columns - 1) * (_cellSize + _cellSpacing) / 2f, (_rows - 1) * (_cellSize + _cellSpacing) / 2f, 0f) * -1f;

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _columns; col++)
                {
                    var cellPosition = new Vector3(col * (_cellSize + _cellSpacing), row * (_cellSize + _cellSpacing), 0f) + _gridCenter;
                    var cell = PoolManager.Instance.Spawn(_cellPrefabTr, cellPosition, Quaternion.identity);
                    cell.GetComponent<Cell>().SetCellIndices(row, col);

                    cell.name = $"Cell ({row}, {col})";
                    _grid[row, col] = cell.GetComponent<ICell>();
                }
            }
        }

        public ICell[,] GetGrid()
        {
            return _grid;
        }
    }
}