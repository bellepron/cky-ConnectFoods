using ConnectFoods.Enums;
using System;

namespace ConnectFoods.Helpers
{
    public static class ItemScriptHelper
    {
        public const string ORDINARY_ITEM = "ConnectFoods.Items.OrdinaryItem";
        public const string ROCKET_ITEM = "ConnectFoods.Items.RocketItem";
        public const string BOMB_ITEM = "ConnectFoods.Items.BombItem";

        public static Type GetItemScript(ItemType itemType)
        {
            string scriptName;

            switch (itemType)
            {
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
                    scriptName = ORDINARY_ITEM;
                    break;
            }

            return Type.GetType(scriptName);
        }
    }
}