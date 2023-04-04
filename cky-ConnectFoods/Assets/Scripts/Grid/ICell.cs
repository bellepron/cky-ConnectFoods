using ConnectFoods.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectFoods.Grid
{
    public interface ICell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public List<ICell> Neighbours { get; set; }
        public ICell FirstCellBelow { get; set; }
        public ItemAbstract Item { get; set; }
        public IItem IItem { get; }
        public bool IsFull { get; }

        void Initialize(int row, int col, int sortingOrder, ICell[,] grid);
        Vector3 GetPosition();
        void Explode();
        void Selected();
        void DeSelected();
    }
}