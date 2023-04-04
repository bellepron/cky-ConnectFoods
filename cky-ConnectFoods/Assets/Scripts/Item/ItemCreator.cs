using cky.Reuseables.Extension;
using cky.Reuseables.Level;
using ConnectFoods.Managers;
using ConnectFoods.Helpers;
using ConnectFoods.Enums;
using ConnectFoods.Grid;
using UnityEngine;
using EZ_Pooling;

namespace ConnectFoods.Item
{
    public class ItemCreator
    {
        private readonly ICell[,] _grid;
        private readonly int _rows;
        private readonly int _cols;
        private readonly LevelSettings _levelSettings;
        private readonly string _itemPrefabDirectory;
        private readonly Transform _itemPrefabTr;



        public ItemCreator(ICell[,] grid)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
            _levelSettings = LevelManager.Instance.LevelSettings;
            _itemPrefabDirectory = _levelSettings.ItemPrefabDirectory;
            _itemPrefabTr = Resources.Load<Transform>(_itemPrefabDirectory);
        }



        public void FillGridWithRandomRandomItems()
        {
            var itemTypesInThisLevel = _levelSettings.ItemTypesInThisLevel;

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    var cell = _grid[row, col];

                    var cellPosition = cell.GetPosition();
                    CreateItem(itemTypesInThisLevel.RandomFromArray(), cell, cellPosition);
                }
            }
        }



        public ItemAbstract CreateItem(ItemType itemType, ICell cell, Vector3 cellPosition)
        {
            GameObject item = EZ_PoolManager.Spawn(_itemPrefabTr, cellPosition, Quaternion.identity).gameObject;

            var scriptType = ItemScriptHelper.GetItemScript(itemType);
            var itemAbstract = item.AddComponent(scriptType) as ItemAbstract;
            itemAbstract.Initialize(itemType);

            cell.Item = itemAbstract;

            return itemAbstract;
        }
    }
}