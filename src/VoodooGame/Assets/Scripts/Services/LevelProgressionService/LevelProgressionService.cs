using System.Collections.Generic;
using Core.Gameplay.Levels;
using Enums;
using Payloads;
using Services.LevelConfigurationService;
using Services.PlayerProgression;
using Signals;
using Systems.CommandSystem;
using UniRx;

namespace Services.LevelProgressionService
{
    public class LevelProgressionService : ILevelProgressionService
    {
        public bool LevelEnd => _levelEnded;
        private bool _levelEnded;

        // private bool _levelState;
        // public bool LevelState => _levelState;
        
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly CompositeDisposable _compositeDisposable;

        private List<FoodRecipe.FoodParameters> _foodParametersList;

        private float _currentTimerValue = 0f;
        private LevelConfiguration _levelConfiguration;
        
        public LevelProgressionService(
            ICommandDispatcher commandDispatcher,
            ILevelConfigurationService levelConfigurationService,
            IPlayerProgressionService playerProgressionService)
        {
            _commandDispatcher = commandDispatcher;
            _levelConfigurationService = levelConfigurationService;
            _playerProgressionService = playerProgressionService;
            _compositeDisposable = new CompositeDisposable();
            
            _levelConfiguration = _levelConfigurationService.GetLevelConfiguration(_playerProgressionService.CurrentLevel.Value);
            _foodParametersList = new List<FoodRecipe.FoodParameters>();

            foreach (var foodParameter in _levelConfiguration.LevelRecipe.FoodIngredients)
            {
                _foodParametersList.Add(new FoodRecipe.FoodParameters(foodParameter.FoodIngredient, 0));
            }
        }

        public void SetLevelEnded(bool flag)
        {
            _levelEnded = flag;
        }

        public void CompleteLevel(bool levelComplete)
        {
            _commandDispatcher.Dispatch<LevelEndSignal>(new LevelEndStatePayload(levelComplete, true));
        }

        public bool CheckAllCollectedIngredients()
        {
            for (var i = 0; i < _foodParametersList.Count; i++)
            {
                var foodParameter = _foodParametersList[i];
                
                if (foodParameter.Count >= _levelConfiguration.LevelRecipe.FoodIngredients[i].Count)
                {
                    return true;
                }
            }
            return false;
        }

        public void ScoreIngredient(IngredientType ingredientType)
        {
            foreach (var foodParameter in _foodParametersList)
            {
                if (foodParameter.FoodIngredient.Type == ingredientType)
                {
                    foodParameter.Count++;
                }
            }
        }

        public void Dispose()
        {
            _levelEnded = false;
            _compositeDisposable.Dispose();
        }
    }
}