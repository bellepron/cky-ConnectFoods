using ConnectFoods.Enums;
using ConnectFoods.Grid;
using UnityEngine;

namespace cky.Reuseables.Level
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        [field: Header("Grid")]
        [field: SerializeField] public GridSettings GridSettings { get; private set; }
        [field: SerializeField] public ItemType[] ItemTypesInThisLevel { get; private set; }
    }
}