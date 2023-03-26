using cky.Reuseables.Extension;
using cky.Reuseables.Level;
using ConnectFoods.Helpers;
using ConnectFoods.Enums;
using ConnectFoods.Grid;
using CKY.Pooling;
using UnityEngine;

namespace ConnectFoods.Items
{
    public class ItemCreator
    {
        private string _itemPrefabDirectory = "Prefabs/Item";
        private Transform _itemPrefabTr;
        private ICell[,] _grid;

        public ItemCreator(ICell[,] grid, LevelSettings levelSettings)
        {
            _grid = grid;

            _itemPrefabTr = Resources.Load<Transform>(_itemPrefabDirectory);

            CreateRandomItems(levelSettings);
        }

        public void CreateRandomItems(LevelSettings levelSettings)
        {
            var rows = levelSettings.GridSettings.Rows;
            var columns = levelSettings.GridSettings.Columns;
            var itemTypesInThisLevel = levelSettings.ItemTypesInThisLevel;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var cell = _grid[row, column];

                    CreateItem(itemTypesInThisLevel.RandomFromArray(), cell);
                }
            }
        }

        public void CreateItem(ItemType itemType, ICell cell)
        {
            var cellPosition = cell.GetPosition();
            GameObject item = PoolManager.Instance.Spawn(_itemPrefabTr, cellPosition, Quaternion.identity);
            var itemScript = item.AddComponent(ItemScriptHelper.GetItemScript(itemType));

            cell.Item = item;

            if (itemScript.TryGetComponent<IItem>(out var iItem))
            {
                iItem.ItemType = itemType;
                iItem.SetSprite(itemType);
            }
        }
    }
}