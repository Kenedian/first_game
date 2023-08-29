using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProgressApocalypse
{
    public class GameController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private RequirementsController requirementsController;
        [SerializeField] private MultipliersController multipliersController;
        [SerializeField] private GameDataController gameDataController;
        [SerializeField] private TableItemController tableItemController;

        [Header("UI_Elements")]
        [SerializeField] private TMP_Text ageText;
        [SerializeField] private TMP_Text lifespanText;
        [SerializeField] private TMP_Text currentCurrencyText;
        [SerializeField] private TMP_Text timeBoostText;
        [SerializeField] private TMP_Text moraleText;
        [SerializeField] private TMP_Text technologyText;

        [Header("UI_Elements/NetResources")]
        [SerializeField] private TMP_Text resourcesNetText;
        [SerializeField] private TMP_Text resourcesIncomeText;
        [SerializeField] private TMP_Text resourcesOutcomeText;

        private readonly float dayTimerBase = 0.050f; //0.3

        private float dayTimer;
        private float currentTime = 0;


        private void Update()
        {
            dayTimer = dayTimerBase * gameDataController.gameDataHolder.gameBoost;
            if (gameDataController.IsYearsOverLifespan())
            {
                if (!gameDataController.gameDataHolder.gameOver)
                {
                    gameDataController.gameDataHolder.gameOver = true;
                    Debug.Log("game over");
                }
                return;
            }

            currentTime += Time.deltaTime;
            if (currentTime >= dayTimer)
            {
                currentTime = 0;
                gameDataController.gameDataHolder.days++;
                ageText.text = gameDataController.GetAgeText();
                lifespanText.text = gameDataController.GetLifespanText();

                //currency
                gameDataController.AddResources(gameDataController.GetCurrentNet());
                currentCurrencyText.text = PaFunctions.FormatResources(gameDataController.gameDataHolder.resources);

                //net
                resourcesNetText.text = $"<color=#7EB1EC>Net</color>/day: {PaFunctions.FormatResources(gameDataController.GetCurrentNet())}";
                resourcesIncomeText.text = $"<color=#699171>Income</color>/day: {PaFunctions.FormatResources(gameDataController.GetCurrentIncome())}";
                resourcesOutcomeText.text = $"<color=#916969>Outcome</color>/day: {PaFunctions.FormatResources(gameDataController.GetCurrentOutcome())}";

                //timewarp morale and technology text
                timeBoostText.text = $"<color=#C80B88>Time Boost:</color> x{gameDataController.gameDataHolder.gameBoost}";
                moraleText.text = $"<color=#7EB1EC>Morale:</color> {multipliersController.GetMorale()}";
                technologyText.text = $"<color=#806991>Technology:</color> {gameDataController.gameDataHolder.technology}";

                //Check Requirements
                AddAvaliableTasksAndItems();

            }
        }

        public void AddAvaliableTasksAndItems()
        {
            foreach (var item in tableItemController.tableItemHolder.JobsClone)
            {
                if(item.jobTask.unlocked)
                {
                    tableItemController.InstantiateJobTableItem(item.jobTask.category, item.jobTask.name);
                }
            }

            foreach (var item in tableItemController.tableItemHolder.SkillsClone)
            {
                if (item.skillTask.unlocked)
                {
                    tableItemController.InstantiateSkillTableItem(item.skillTask.category, item.skillTask.name);
                }
            }

            foreach (var item in tableItemController.tableItemHolder.ItemsClone)
            {
                if (item.item.unlocked)
                {
                    tableItemController.InstantiateItemTableItem(item.item.category, item.item.name);
                }
            }
        }
    }
}
