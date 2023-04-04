using ConnectFoods.Enums;
using ConnectFoods.Grid;
using ConnectFoods.Helpers;
using ConnectFoods.Managers;
using UnityEngine;
using EZ_Pooling;
using DG.Tweening;

namespace ConnectFoods.Item
{
    public abstract class ItemAbstract : MonoBehaviour, IItem
    {
        public ItemType ItemType { get; set; }
        public MatchType MatchType { get; set; }
        public bool IsFallableItem { get; set; }
        public bool IsMatchableItem { get; set; }

        public ICell TargetCell { get; private set; }
        public bool IsFalling { get; private set; }
        public float FallSpeed { get; private set; }
        public float FallTime { get; private set; }
        public bool IsAnimPlayed { get; private set; }

        protected SpriteRenderer spriteRenderer;
        Tween _tween;



        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }



        public void Initialize(ItemType itemType)
        {
            ItemType = itemType;
            IsFallableItem = itemType.IsFallable();
            MatchType = itemType.GetMatchType();
            IsMatchableItem = MatchType != MatchType.None ? true : false;

            SetSprite(ItemType);

            FallSpeed = 0.0f;
        }



        private void SetSprite(ItemType itemType)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetSpritesForItemType(itemType);
        }



        public void Explode()
        {
            _tween.Kill();
            transform.localScale = Vector3.one * 0.2f;
            EZ_PoolManager.Despawn(transform);
            Destroy(this);
        }



        public void FallTo(ICell targetCell, float maxFallSpeed, float acceleration, float deltaTime)
        {
            //if (IsFalling == true) return;
            //IsFalling = true;

            var thisTransform = transform;
            var targetCellPos = targetCell.GetPosition();

            if (thisTransform.position.y == targetCellPos.y)
            {
                if (!IsAnimPlayed)
                {
                    _tween.Kill();
                    IsAnimPlayed = true;
                    _tween = thisTransform.DOScaleY(0.175f, 0.1f).OnComplete(() => thisTransform.DOScaleY(0.2f, 0.08f));
                }

                return;
            }

            FallTime += deltaTime;
            FallSpeed += 0.5f * acceleration * FallTime;
            maxFallSpeed *= Time.deltaTime;

            FallSpeed = FallSpeed > maxFallSpeed ? maxFallSpeed : FallSpeed;

            if (thisTransform.position.y > targetCellPos.y)
            {

                thisTransform.position += Vector3.down * FallSpeed;

                _tween.Kill();
                transform.localScale = Vector3.one * 0.2f;
                IsAnimPlayed = false;
            }
            else
            {
                thisTransform.position = targetCellPos;

                IsFalling = false;
                FallTime = 0.0f;
                FallSpeed = 0.0f;
            }
        }
    }
}