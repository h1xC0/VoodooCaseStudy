using Core.WindowSystem.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.PersistentWindow
{
    public class LevelObjectiveView : RawView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetObjectiveCount(int value)
        {
            _count.text = $"x{value}";
        }
    }
}