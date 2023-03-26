using UnityEngine;

namespace ConnectFoods.Items
{
    public abstract class ItemAbstract : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}