using UnityEngine;

namespace cky.Reuseables.Level
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        [field: Header("Board")]
        [field: SerializeField] public int BoardWidth { get; private set; } = 10;
        [field: SerializeField] public int BoardHeight { get; private set; } = 12;

    }
}