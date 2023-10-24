using System;
using DG.Tweening;
using UnityEngine;

namespace Services.AnimationService
{
    public interface IAnimationService : IDisposable
    {
        Sequence SetupDisposeAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed);
        Sequence SetupChangeSpriteAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed);
        Sequence SetupEnterAnimation(Transform target, float animationPositionOffset, float animationSpeed);
        Sequence SetupFloatingAnimation(Transform target, float amplitude, float duration);
        Sequence SetupMoveAnimation(Transform target, CanvasGroup canvasGroup, float distance, float fade,
            TweenCallback callback);
        Sequence SetupShakeSequence(Transform target, float animationSpeed, float strength, int vibrato);
    }
}