using Constants;
using DG.Tweening;
using UnityEngine;

namespace Services.AnimationService
{
    public class AnimationService : IAnimationService
    {
        public Sequence SetupDisposeAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            var position = target.position;
            var parent = target.parent;
            
            sequence
                .Append(target.DOScale(Vector3.one * 0.1f, animationSpeed)
                .OnStart(() => 
                { 
                    target.position = position; 
                    target.SetParent(parent);
                })
                .OnComplete(setSpriteCallBack));

            return sequence;
        }
        
        public Sequence SetupChangeSpriteAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            sequence.Append(target.DOScale(Vector3.one * 0.1f, animationSpeed).OnComplete(setSpriteCallBack));
            sequence.Insert(0f, target.DORotate(Vector3.forward * 180, animationSpeed, RotateMode.FastBeyond360));
            sequence.Append(target.DOScale(Vector3.one, animationSpeed));
            sequence.Insert(animationSpeed, target.DORotate(Vector3.zero, 0.5f));

            return sequence;
        }
        
        public Sequence SetupEnterAnimation(Transform target, float animationPositionOffset, float animationSpeed)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            var endPosition = target.localPosition;
            endPosition.y += animationPositionOffset;
            target.localPosition = endPosition;

            sequence.Append(target.DOLocalMoveY(-animationPositionOffset, animationSpeed));

            return sequence;
        }

        public Sequence SetupFloatingAnimation(Transform target, float amplitude, float duration)
        {
            var sequence = DOTween.Sequence(target);
            var animationPositionOffset = target.position; 
            sequence.SetTarget(target);
            sequence.SetAutoKill();
            
            sequence.Append(target.DOMoveY(target.position.y - amplitude, duration));
            sequence.Append(target.DOMoveY(animationPositionOffset.y, duration));
            sequence.SetEase(Ease.InOutSine);

            sequence.SetLoops(-1);

            return sequence;
        }
        
        public Sequence SetupMoveAnimation(Transform target, CanvasGroup canvasGroup, float distance, float fade, TweenCallback callback)
        {
            var sequence = DOTween.Sequence(target);
            var startPosition = target.localPosition;
            
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            var endPosition = startPosition;
            endPosition.y += distance;
            sequence.Append(target.DOLocalMove(endPosition, 0));

            sequence.Append(target.DOLocalMoveY(startPosition.y, AnimationConstants.AnimationSpeed));
            sequence.Insert(0f, canvasGroup.DOFade(fade, AnimationConstants.AnimationSpeed));
            sequence.AppendCallback(callback);
                
            return sequence;
        }
        
        public Sequence SetupShakeSequence(Transform target, float animationSpeed, float strength, int vibrato)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            sequence.Append(target.DOShakePosition(animationSpeed, strength, vibrato));
            return sequence;
        }

        public void Dispose()
        {
            
        }
    }
}