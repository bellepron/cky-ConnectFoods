using ConnectFoods.Enums;

namespace ConnectFoods.Item
{
    public interface IItem
    {
        public ItemType ItemType { get; set; }
        public MatchType MatchType { get; set; }
        public bool IsFallableItem { get; set; }
        public bool IsMatchableItem { get; set; }

        void Explode();
    }
}