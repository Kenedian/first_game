using BreakInfinity;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressApocalypse
{
    public class Task
    {
        public string name;
        public int level = 0;
        public int maxLevel;
        public BigDouble currentXP = 0;

        [Header("Leveling")]
        public BigDouble maxXP = 50;
        public BigDouble baseXpGain = 10;

        [Header("Unlocked Task")]
        public bool unlocked;

        public List<float> xpMultiplers = new();
        public bool active;

        // Curent XP in 999 / 1K / 1M / 1B format
        public string GetCurrentXPFormatted()
        {
            return PaFunctions.FormatCurrencyKMB(currentXP, "F0");
        }

        // RequiredXP in 999 / 1K / 1M / 1B format
        public string GetRequiredXPFormatted()
        {
            return PaFunctions.FormatCurrencyKMB(GetRequiredXP(), "F0");
        }

        // Multiplier gained after basic prestige
        public int GetMaxLevelMultiplier()
        {
            return 1 + (maxLevel / 10);
        }

        // Current multipliers summed up
        protected float GetXpMultiplier()
        {
            float finalMultiplier = 1;

            if (xpMultiplers.Count != 0)
            {
                foreach (var item in xpMultiplers)
                {
                    finalMultiplier *= item;
                }
            }

            return finalMultiplier;
        }

        // returns xp needed for next level
        public BigDouble GetRequiredXP()
        {
            return BigDouble.Round(maxXP * (level + 1) * BigDouble.Pow(1.01, this.level));
        }

        // increase xp and also gives new level when threshold is met
        public void GainXP()
        {
            currentXP += baseXpGain * GetXpMultiplier();
            if(currentXP >= GetRequiredXP())
            {
                BigDouble excess = currentXP - GetRequiredXP();
                currentXP = excess;
                level++;
            }
        }
    }
}
