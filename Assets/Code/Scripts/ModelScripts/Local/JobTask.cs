using BreakInfinity;
using System;
using System.Collections.Generic;
using UnityEngine;
using static ProgressApocalypse.PaEnums;

namespace ProgressApocalypse
{
    [Serializable]
    public class JobTask : Task
    {
        [Header("JobSpecific")]
        public JobCategories category;
        public BigDouble baseIncome;
        public List<float> incomeMultipliers = new();

        protected float GetIncomeMultiplier()
        {
            float finalMultiplier = 1;

            if (incomeMultipliers.Count != 0)
            {
                foreach (var item in incomeMultipliers)
                {
                    finalMultiplier *= item;
                }
            }

            return finalMultiplier;
        }

        public float GetIncomeLevelMultiplier()
        {
            return 1 + Mathf.Log10(level + 1);
        }

        public BigDouble GetIncome()
        {
            return baseIncome * GetIncomeMultiplier();
        }

        public string GetIncomeFormatted()
        {
            return PaFunctions.FormatResources(baseIncome);
        }
    }
}
