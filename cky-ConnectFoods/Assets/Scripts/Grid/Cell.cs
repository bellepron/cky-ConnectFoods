using System.Collections.Generic;
using ConnectFoods.Helpers;
using ConnectFoods.Item;
using DG.Tweening;
using UnityEngine;

namespace ConnectFoods.Grid
{
    public class Cell : MonoBehaviour, ICell
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        public int Row { get; set; }
        public int Col { get; set; }

        private List<ICell> _neighbours;
        public List<ICell> Neighbours { get { return _neighbours; } set { _neighbours = value; } }

        private ICell _firstCellBelow;
        public ICell FirstCellBelow { get { return _firstCellBelow; } set { _firstCellBelow = value; } }

        private ItemAbstract _item;
        public ItemAbstract Item { get { return _item; } set { _item = value; } }
        public IItem IItem { get { return _item?.GetComponent<IItem>(); } }
        public bool IsFull => Item;

        private float _initItemScale = 0.2f;
        private Tween _tween = null;



        public void Initialize(int row, int col, int sortingOrder, ICell[,] grid)
        {
            Row = row;
            Col = col;

            SpriteRenderer.sortingOrder = sortingOrder;

            this.SetCellNeighbours(grid, ref _neighbours, ref _firstCellBelow);
        }



        public Vector3 GetPosition() => transform.position;



        public void Explode()
        {
            if (IsFull)
            {
                Item.transform.localScale = _initItemScale * Vector3.one;
                IItem.Explode();
                Item = null;

                SpriteRenderer.material.color = Color.white;

                if (_tween != null)
                    _tween.Kill();
            }
            else
            {
                Debug.LogWarning("No item!");
            }
        }



        public void Selected()
        {
            SpriteRenderer.material.color = Color.yellow;

            if (_tween != null)
                _tween.Kill();

            _tween = Item.transform.VibrateScaleContinuously(_initItemScale, _initItemScale * 1.25f, 0.25f);
        }



        public void DeSelected()
        {
            SpriteRenderer.material.color = Color.white;

            _tween.Kill();
            _tween = Item.transform.ResetScale(_initItemScale, 0.1f);
        }
    }
}
