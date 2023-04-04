using cky.Reuseables.Singleton;
using ConnectFoods.Enums;
using UnityEngine;

namespace ConnectFoods.Managers
{
    public class SpriteManager : SingletonPersistent<SpriteManager>
    {
        #region Sprites

        public Sprite Leaf1Sprite;
        public Sprite Leaf2Sprite;
        public Sprite PumpkinSprite;
        public Sprite BananaSprite;
        public Sprite AppleSprite;
        public Sprite WaterDropSprite;
        public Sprite BlueberrySprite;
        public Sprite DragonFruitSprite;

        public Sprite VerticalRocketSprite;
        public Sprite HorizontalRocketSprite;
        public Sprite BombSprite;

        #endregion



        public Sprite GetSpritesForItemType(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Leaf1:
                    return Leaf1Sprite;
                case ItemType.Leaf2:
                    return Leaf2Sprite;
                case ItemType.Pumpkin:
                    return PumpkinSprite;
                case ItemType.Banana:
                    return BananaSprite;
                case ItemType.Apple:
                    return AppleSprite;
                case ItemType.WaterDrop:
                    return WaterDropSprite;
                case ItemType.Blueberry:
                    return BlueberrySprite;
                case ItemType.DragonFruit:
                    return DragonFruitSprite;

                case ItemType.VerticalRocket:
                    return VerticalRocketSprite;
                case ItemType.HorizontalRocket:
                    return HorizontalRocketSprite;
                case ItemType.Bomb:
                    return BombSprite;
            }

            return null;
        }
    }
}