using ConnectFoods.Enums;
using UnityEngine;

namespace cky.Reuseables.Level
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        [field: Header("Grid")]
        [SerializeField] private string _cellPrefabDirectory = "Prefabs/Cell";
        [SerializeField] private int _rows = 4;
        [SerializeField] private int _columns = 4;
        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private float _cellSpacing = 0.1f;

        public string CellPrefabDirectory => _cellPrefabDirectory;

        public int Rows
        {
            get => _rows;
            set { _rows = Mathf.Clamp(value, 4, 9); }
        }

        public int Columns
        {
            get => _columns;
            set { _columns = Mathf.Clamp(value, 4, 9); }
        }

        public float CellSize => _cellSize;
        public float CellSpacing => _cellSpacing;



        [field: Header("Item")]
        [SerializeField] private string _itemPrefabDirectory = "Prefabs/Item";
        public string ItemPrefabDirectory => _itemPrefabDirectory;
        [field: SerializeField] public ItemType[] ItemTypesInThisLevel { get; private set; }



        [field: Header("Fall")]
        [field: SerializeField] public float MaxFallSpeed { get; private set; } = 10.0f;
        [field: SerializeField] public float FallAcceleration { get; private set; } = 9.8f;


        [field: Header("Game")]
        [field: SerializeField] public int MinMatchLimit { get; private set; } = 3;
        [field: SerializeField] public float ShuffleTime { get; private set; } = 1.0f;
        [field: SerializeField] public float ExplodeWithOrderDelay { get; private set; } = 0.05f;
    }
}