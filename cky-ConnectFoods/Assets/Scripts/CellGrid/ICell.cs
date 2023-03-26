using ConnectFoods.Items;
using UnityEngine;

namespace ConnectFoods.Grid
{
    public interface ICell
    {
        public IItem IItem { get; set; }

        public GameObject Item { get; set; }

        Vector3 GetPosition();
        void Explode();
        void Selected();
        void DeSelected();
    }
}