using ConnectFoods.Enums;

namespace ConnectFoods.Items
{
    public interface IItem
    {
        public ItemType ItemType { get; set; }

        void SetSprite(ItemType itemType);
        void Explode();
    }
}