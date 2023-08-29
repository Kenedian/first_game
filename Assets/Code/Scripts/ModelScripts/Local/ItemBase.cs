using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressApocalypse
{
    [Serializable]
    public class ItemBase
    {
        public string name;
        public PaEnums.MarketCategories category;
        public BigDouble expense;
        public float effect;
        public string description;

        public bool unlocked;
        public bool active;

        public List<float> expenseMultiplier = new List<float>();

        public float GetEffect()
        {
            return effect;
        }

        public string GetEffectDescription()
        {
            return "x" + GetEffect() + " " + description;
        }

        protected float GetExpenseMultiplier()
        {
            float finalMultiplier = 1;

            if (expenseMultiplier.Count != 0)
            {
                foreach (var item in expenseMultiplier)
                {
                    finalMultiplier *= item;
                }
            }

            return finalMultiplier;
        }

        public BigDouble GetExpense()
        {
            return expense * GetExpenseMultiplier();
        }

        public string GetExpenseFormatted()
        {
            return PaFunctions.FormatResources(expense);
        }
    }
}
