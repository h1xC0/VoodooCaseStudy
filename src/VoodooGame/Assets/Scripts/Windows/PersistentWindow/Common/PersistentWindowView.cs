using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Constants;
using Core.Gameplay.Levels;
using Core.WindowSystem;
using DG.Tweening;
using Services.ResourceProvider;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.PersistentWindow.Common
{
    public class PersistentWindowView : WindowView, IPersistentWindowView, IResource
    {
        [SerializeField] private TMP_Text _moneyCount;
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private TMP_Text _LevelObjectiveTMP;

        private List<LevelObjectiveView> _levelObjectiveList;
        [SerializeField] private List<CanvasGroup> _objectsToHide;
        [SerializeField] private LevelObjectiveView _levelObjectiveView;
        [SerializeField] private Transform _recipeLayout;

        [SerializeField] private Button _hideButton;

        public IObservable<Unit> HideViewObservable { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            HideViewObservable = _hideButton.OnClickAsObservable();
            HideViewObservable.Subscribe(_ => HideLevelObjectives());
        }

        public void SetMoney(int value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value);
            sb.Append("$");
            _moneyCount.text = sb.ToString();
        }

        public void SetLevelNumber(int value)
        {
            StringBuilder sb = new StringBuilder("Level ");
            sb.Append(value);
            _levelNumber.text = sb.ToString();
        }

        public void SetLevelObjectives(FoodRecipe foodRecipe)
        {
            _LevelObjectiveTMP.text = foodRecipe.Title;
            _levelObjectiveList = new List<LevelObjectiveView>();
            for (int i = 0; i < foodRecipe.FoodIngredients.Count; i++)
            {
                var levelObjectiveView = Instantiate(_levelObjectiveView, _recipeLayout);
                _levelObjectiveList.Add(levelObjectiveView);
             
                var parameter = foodRecipe.FoodIngredients[i];
                _levelObjectiveList[i].SetIcon(parameter.FoodIngredient.Sprite);
                _levelObjectiveList[i].SetObjectiveCount(parameter.Count);
            } 
        }

        private void HideLevelObjectives()
        {
            foreach (var hideObject in _objectsToHide)
            {
                hideObject.DOFade(0, AnimationConstants.AnimationSpeed)
                    .OnComplete(() => hideObject.gameObject.SetActive(false));
            }
        }
    }
}