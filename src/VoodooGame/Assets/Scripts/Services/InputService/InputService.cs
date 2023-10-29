
using UnityEngine;

namespace Services.InputService
{
    public class InputService : IInputService
    {
        public bool GetClickOnScreen()
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
#else
            if (Input.touchCount > 0)
            {
                var firstTouch = Input.GetTouch(0);
                return firstTouch.phase == TouchPhase.Began;
            }
#endif
            return false;
        }
        
        public void Dispose()
        {
        }
    }
}