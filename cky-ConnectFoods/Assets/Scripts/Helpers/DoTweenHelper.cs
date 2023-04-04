using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ConnectFoods.Helpers
{
    public static class DoTweenHelper
    {
        public static Tween ResetScale(this Transform itemTr, float initscale, float animationTime)
        {
            var seq = DOTween.Sequence();
            seq.Append(itemTr.DOScale(initscale, animationTime)).SetEase(Ease.InSine);

            return seq;
        }



        public static Tween VibrateScale(this Transform itemTr, float initscale, float maxScale, float animationTime)
        {
            var seq = DOTween.Sequence();
            seq.Append(itemTr.DOScale(maxScale, animationTime * 0.4f)).SetEase(Ease.OutSine);
            seq.Append(itemTr.DOScale(initscale, animationTime * 0.6f)).SetEase(Ease.InSine);

            return seq;
        }



        public static Tween VibrateScaleContinuously(this Transform itemTr, float initscale, float maxScale, float animationTime)
        {
            var seq = DOTween.Sequence();
            seq.Append(itemTr.DOScale(maxScale, animationTime * 0.4f)).SetEase(Ease.OutSine);
            seq.Append(itemTr.DOScale(initscale, animationTime * 0.6f)).SetEase(Ease.InSine);
            seq.SetLoops(-1);

            return seq;
        }



        public static Tween Fall(GameObject item, float targetPosY, float arrivingTime, Ease easeType)
        {
            var seq = DOTween.Sequence();
            seq.Append(item.transform.DOMoveY(targetPosY, arrivingTime));
            seq.SetEase(easeType);

            return seq;
        }



        public static Tween RemoveAnimation(GameObject actor, Transform clickedItemTr, float maxScale, float scaleUpTime, float scaleDownTime, float removeDistancingFactor)
        {
            var actorTr = actor.transform;
            var diffVector = actorTr.position - clickedItemTr.position;
            var addedPos = actorTr.position + diffVector * removeDistancingFactor;

            Sequence seq = DOTween.Sequence();

            seq.Append(actorTr.DOMove(addedPos, scaleUpTime).SetEase(Ease.Flash))
               .Join(actorTr.DOScale(maxScale, scaleUpTime));

            seq.Append(actorTr.DOMove(clickedItemTr.position, scaleDownTime))
               .Join(actorTr.DOScale(1f, scaleDownTime));

            return seq;
        }



        public static Tween BoingImage(Transform actorTr, TextMeshProUGUI tmp, float maxScale, float completedScale, float imageScaleUpTime, float imageScaleDownTime)
        {
            var seq = DOTween.Sequence();
            seq.Append(actorTr.DOScale(maxScale, imageScaleUpTime));
            seq.Append(actorTr.DOScale(completedScale, imageScaleDownTime));

            return seq;
        }
    }
}