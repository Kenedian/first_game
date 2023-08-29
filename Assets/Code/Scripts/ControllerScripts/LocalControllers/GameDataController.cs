using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProgressApocalypse
{
    public class GameDataController : MonoBehaviour
    {
        public GameDataHolder gameDataHolder;
        [SerializeField] private TableItemsHolder tableItemHolder;

        [SerializeField] private TableItemController tableItemController;
        
        public string GetAgeText()
        {
            int age = (int)Math.Truncate((decimal)gameDataHolder.days / 365);
            int daysRemainder = (int)Math.Truncate((decimal)gameDataHolder.days % 365);

            return $"Age {age} Day {daysRemainder}";
        }

        public string GetLifespanText()
        {
            int age = (int)Math.Truncate((decimal)gameDataHolder.lifeSpan / 365);
            int daysRemainder = (int)Math.Truncate((decimal)gameDataHolder.lifeSpan % 365);

            return $"Lifespan: {age} years" + (gameDataHolder.lifeSpan % 365 > 0 ? $" days: {daysRemainder}" : "");
        }

        public bool IsYearsOverLifespan()
        {
            return gameDataHolder.days >= gameDataHolder.lifeSpan;
        }

        public void SetMenuLevel()
        {
            gameDataHolder.menuLevel++;
            if(gameDataHolder.menuLevel > 3)
            {
                gameDataHolder.menuLevel = 1;
            }
        }

        public float GetTechnologyGain()
        {
            return GetEffect("Robotics") * GetEffect("Experimenting");
        }

        public float GetGameSpeed()
        {
            float boost = GetEffect("Time Travel") * gameDataHolder.speedBoostRemainingTime > 0 ? 2 : 1;
            float speed = gameDataHolder.gameBoost * (gameDataHolder.timeBoostEnabled ? boost : 1) * Convert.ToInt32(!gameDataHolder.paused);
            return speed;
        }

        public void AddResources(BigDouble resources)
        {
            gameDataHolder.resources += resources;
            if (gameDataHolder.resources < 0)
            {
                gameDataHolder.resources = 0;
                gameDataHolder.currentSettlement = "Cave";
                gameDataHolder.currentRent = new();
                tableItemController.DisableAllItems();
                tableItemController.EnableItem("Cave");
            }
        }
        
        public BigDouble GetCurrentNet()
        {
            return GetCurrentIncome() - GetCurrentOutcome();
        }

        public BigDouble GetCurrentIncome()
        {
            JobTask item = GetJob(gameDataHolder.currentJob);
            BigDouble income = item.GetIncome();
            return income;
        }

        public BigDouble GetCurrentOutcome()
        {
            var rents = gameDataHolder.currentRent;
            BigDouble expenses = 0;

            foreach (var item in rents)
            {
                var found = GetItem(item);

                if(found != null && found.active)
                {
                    expenses += found.GetExpense();
                }
            }

            return expenses + GetItem(gameDataHolder.currentSettlement).GetExpense();
        }

        public float GetEffect(string name)
        {
            SkillTask item = tableItemHolder.SkillsClone.Single(x => x.skillTask.name == name).skillTask;

            return item != null ? item.GetEffectAmmount() : 1.0f;
        }

        public ItemBase GetItem(string name)
        {
            ItemBase item = tableItemHolder.ItemsClone.Single(x => x.item.name == name).item;

            return item;
        }

        public JobTask GetJob(string name)
        {
            JobTask item = tableItemHolder.JobsClone.Single(x => x.jobTask.name == name).jobTask;

            return item;
        }

        public SkillTask GetSkill(string name)
        {
            SkillTask item = tableItemHolder.SkillsClone.Single(x => x.skillTask.name == name).skillTask;

            return item;
        }
    }
}
