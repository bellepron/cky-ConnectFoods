using ConnectFoods.Enums;
using System;

namespace ConnectFoods.Helpers
{
    public static class ItemScriptHelper
    {
        public const string ORDINARY_ITEM = "ConnectFoods.Item.OrdinaryItem";
        public const string ROCKET_ITEM = "ConnectFoods.Item.RocketItem";
        public const string BOMB_ITEM = "ConnectFoods.Item.BombItem";

        public static Type GetItemScript(ItemType itemType)
        {
            string scriptName;

            switch (itemType)
            {
                case ItemType.Leaf1:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.Leaf2:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.Pumpkin:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.Banana:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.Apple:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.Blueberry:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.DragonFruit:
                    scriptName = ORDINARY_ITEM;
                    break;
                case ItemType.WaterDrop:
                    scriptName = ORDINARY_ITEM;
                    break;

                case ItemType.VerticalRocket:
                    scriptName = ROCKET_ITEM;
                    break;
                case ItemType.HorizontalRocket:
                    scriptName = ROCKET_ITEM;
                    break;
                case ItemType.Bomb:
                    scriptName = BOMB_ITEM;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Item script directory is wrong!", itemType, null);
            }

            return Type.GetType(scriptName);
        }



        public static MatchType GetMatchType(this ItemType itemType)
        {
            var matchType = MatchType.None;

            switch (itemType)
            {
                case ItemType.Leaf1:
                    matchType = MatchType.Leaf1;
                    break;
                case ItemType.Leaf2:
                    matchType = MatchType.Leaf2;

                    break;
                case ItemType.Pumpkin:
                    matchType = MatchType.Pumpkin;
                    break;
                case ItemType.Banana:
                    matchType = MatchType.Banana;
                    break;
                case ItemType.Apple:
                    matchType = MatchType.Apple;
                    break;
                case ItemType.Blueberry:
                    matchType = MatchType.Blueberry;
                    break;
                case ItemType.DragonFruit:
                    matchType = MatchType.DragonFruit;
                    break;
                case ItemType.WaterDrop:
                    matchType = MatchType.WaterDrop;
                    break;


                case ItemType.VerticalRocket:
                    matchType = MatchType.Special;
                    break;
                case ItemType.HorizontalRocket:
                    matchType = MatchType.Special;
                    break;
                case ItemType.Bomb:
                    matchType = MatchType.Special;
                    break;

                default:
                    matchType = MatchType.None;
                    break;
            }

            return matchType;
        }



        public static bool IsFallable(this ItemType itemType)
        {
            bool nIsFallable;

            switch (itemType)
            {


                default:
                    nIsFallable = true;
                    break;
            }

            return nIsFallable;
        }
    }
}