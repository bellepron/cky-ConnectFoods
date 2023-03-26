using ConnectFoods.Items;
using UnityEngine;

namespace ConnectFoods.Grid
{
    public class Cell : MonoBehaviour, ICell
    {
        [SerializeField] private int row;
        [SerializeField] private int col;

        [SerializeField] private GameObject item;
        private IItem _iItem;
        public IItem IItem { get { return _iItem; } set { _iItem = value; } }

        public GameObject Item
        {
            get { return item; }
            set
            {
                item = value;
                if (item == null)
                {
                    _iItem = null;
                }
                else
                {
                    _iItem = item.GetComponent<IItem>();
                }
            }
        }

        public void SetCellIndices(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public Vector3 GetPosition() => transform.position;

        public void Explode()
        {
            IItem.Explode();
            Item = null;

            DeSelected();
        }

        public void Selected()
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        public void DeSelected()
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
