using System;
using DG.Tweening;

namespace Services.AnimationService
{
    public class IWindowAnimationService : IDisposable
    {
        Sequence Show();
        Sequence Hide();
    }
}