using CKY.Pooling;
using ConnectFoods.Enums;
using ConnectFoods.Managers;

namespace ConnectFoods.Items
{
    public class OrdinaryItem : ItemAbstract, IItem
    {
        public ItemType ItemType { get; set; }

        public void SetSprite(ItemType itemType)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetSpritesForItemType(itemType);
        }

        public void Explode()
        {
            PoolManager.Instance.Despawn(gameObject);
        }
    }
}