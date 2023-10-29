using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Core.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Configurations/Ingredient")]
    public class FoodIngredient : ScriptableObject
    {
        public IngredientType Type;
        public Sprite Sprite;
        public int Points;
    }
}