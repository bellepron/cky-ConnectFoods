using cky.Reuseables.Level;
using ConnectFoods.Managers;
using EZ_Pooling;
using UnityEngine;

namespace ConnectFoods.Grid.Managers
{
    public class GridCreator
    {
        private LevelSettings _levelSettings;
        private string _cellPrefabDirectory;
        private Transform _cellPrefabTr;
        private int _rows;
        private int _columns;
        private float _cellSize;
        private float _cellSpacing;
        private Vector3 _gridCenter;
        private ICell[,] _grid;



        public GridCreator()
        {
            _levelSettings = LevelManager.Instance.LevelSettings;

            _cellPrefabDirectory = _levelSettings.CellPrefabDirectory;
            _cellPrefabTr = Resources.Load<Transform>(_cellPrefabDirectory);

            _rows = _levelSettings.Rows;
            _columns = _levelSettings.Columns;
            _cellSize = _levelSettings.CellSize;
            _cellSpacing = _levelSettings.CellSpacing;
        }



        public ICell[,] Create()
        {
            _grid = new ICell[_rows, _columns];

            _gridCenter = new Vector3((_columns - 1) * (_cellSize + _cellSpacing) / 2f, (_rows - 1) * (_cellSize + _cellSpacing) / 2f, 0f) * -1f;

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _columns; col++)
                {
                    var cellPosition = new Vector3(col * (_cellSize + _cellSpacing), row * (_cellSize + _cellSpacing), 0f) + _gridCenter;
                    var cellGameObject = EZ_PoolManager.Spawn(_cellPrefabTr, cellPosition, Quaternion.identity);

                    _grid[row, col] = cellGameObject.GetComponent<ICell>();

                    cellGameObject.name = $"Cell ({row}, {col})";


                }
            }

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _columns; col++)
                {
                    _grid[row, col].Initialize(row, col, _rows - row, _grid);
                }
            }

            return _grid;
        }
    }
}