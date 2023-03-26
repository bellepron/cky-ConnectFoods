using UnityEngine;

namespace ConnectFoods.Grid
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GridSettings")]
    public class GridSettings : ScriptableObject
    {
        [SerializeField] private string cellPrefabDirectory = "Prefabs/CellPrefab";
        [SerializeField] private int rows = 4;
        [SerializeField] private int columns = 4;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private float cellSpacing = 0.1f;

        public string CellPrefabDirectory => cellPrefabDirectory;

        public int Rows
        {
            get => rows;
            set { rows = Mathf.Clamp(value, 4, 9); }
        }

        public int Columns
        {
            get => columns;
            set { columns = Mathf.Clamp(value, 4, 9); }
        }

        public float CellSize => cellSize;
        public float CellSpacing => cellSpacing;
    }
}