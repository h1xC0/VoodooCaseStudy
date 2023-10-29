using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "FoodRecipe", menuName = "Configurations/FoodRecipe")]
    public class FoodRecipe : ScriptableObject
    {
        public string Title;
        public Sprite Icon;
        public List<FoodParameters> FoodIngredients;

        [Serializable]
        public class FoodParameters
        {
            public FoodIngredient FoodIngredient;
            public int Count;

            public FoodParameters(FoodIngredient foodIngredient, int count)
            {
                FoodIngredient = foodIngredient;
                Count = count;
            }
        }
    }
}