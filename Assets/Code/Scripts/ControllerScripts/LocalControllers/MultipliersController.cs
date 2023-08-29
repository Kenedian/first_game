using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProgressApocalypse
{
    public class MultipliersController : MonoBehaviour
    {
        [SerializeField] private TableItemsHolder tableItemHolder;
        [SerializeField] private GameDataHolder gameDataHolder;

        public void UpdateMultipliers()
        {
            foreach (Job task in tableItemHolder.JobsClone)
            {
                task.jobTask.xpMultiplers.Clear();
                task.jobTask.incomeMultipliers.Clear();

                task.jobTask.xpMultiplers.Add(task.jobTask.GetMaxLevelMultiplier());
                task.jobTask.xpMultiplers.Add(GetMorale());
                task.jobTask.xpMultiplers.Add(GetEffect("Tools"));
                task.jobTask.xpMultiplers.Add(GetEffect("Bio-Weapons"));

                task.jobTask.incomeMultipliers.Add(task.jobTask.GetIncomeLevelMultiplier());
                task.jobTask.incomeMultipliers.Add(GetEffect("Cyborgification"));
                task.jobTask.incomeMultipliers.Add(GetEffect("Productivity"));
                task.jobTask.incomeMultipliers.Add(GetItemEffect("Bodyguard"));

                if(task.jobTask.category == PaEnums.JobCategories.CombatJob)
                {
                    task.jobTask.incomeMultipliers.Add(GetEffect("Training"));
                    task.jobTask.xpMultiplers.Add(GetEffect("Agility"));
                    task.jobTask.xpMultiplers.Add(GetItemEffect("Rifle"));
                }

                if (task.jobTask.category == PaEnums.JobCategories.EngineeringJob)
                {
                    task.jobTask.xpMultiplers.Add(GetEffect("Researching"));
                }
            }

            foreach (Skill task in tableItemHolder.SkillsClone)
            {
                task.skillTask.xpMultiplers.Clear();

                task.skillTask.xpMultiplers.Add(task.skillTask.GetMaxLevelMultiplier());
                task.skillTask.xpMultiplers.Add(GetMorale());
                task.skillTask.xpMultiplers.Add(GetEffect("Tools"));
                task.skillTask.xpMultiplers.Add(GetEffect("Bio-Weapons"));

                task.skillTask.xpMultiplers.Add(GetEffect("Meditation"));
                task.skillTask.xpMultiplers.Add(GetItemEffect("Notes"));
                task.skillTask.xpMultiplers.Add(GetItemEffect("Master's Notes"));
                task.skillTask.xpMultiplers.Add(GetItemEffect("Research Table"));

                if(task.skillTask.name == "Training")
                {
                    task.skillTask.xpMultiplers.Add(GetEffect("Toughness"));
                    task.skillTask.xpMultiplers.Add(GetItemEffect("Trainer"));
                }

                if(task.skillTask.category == PaEnums.SkillCategories.EngineeringSkill)
                {
                    task.skillTask.xpMultiplers.Add(GetItemEffect("Tools"));
                }

                if(task.skillTask.category == PaEnums.SkillCategories.BiologicalEngineering)
                {
                    task.skillTask.xpMultiplers.Add(gameDataHolder.technology);
                }
            }

            foreach (Item item in tableItemHolder.ItemsClone)
            {
                item.item.expenseMultiplier.Clear();

                item.item.expenseMultiplier.Add(GetEffect("Bargaining"));
                item.item.expenseMultiplier.Add(GetEffect("Amplification"));
            }
        }

        public float GetMorale()
        {
            float moraleBoost = 1.0f;
            if(gameDataHolder.moraleBoostRemainingTime > 0)
            {
                moraleBoost = 2.0f;
            }

            return GetEffect("Resting") * GetItemEffect("Maid") * GetItemEffect(gameDataHolder.currentSettlement) * moraleBoost;
        }

        private float GetEffect(string name)
        {
            SkillTask item = tableItemHolder.SkillsClone.Single(x => x.skillTask.name == name).skillTask;
            if (!item.unlocked)
            {
                return 1.0f;
            }

            return item.GetEffectAmmount();
        }

        private float GetItemEffect(string name)
        {
            ItemBase item = tableItemHolder.ItemsClone.Single(x => x.item.name == name).item;
            if(!item.unlocked || !item.active)
            {
                return 1.0f;
            }

            return item.GetEffect();
        }
    }
}
