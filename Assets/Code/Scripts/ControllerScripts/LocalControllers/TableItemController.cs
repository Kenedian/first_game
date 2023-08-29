using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProgressApocalypse
{
    public class TableItemController : MonoBehaviour
    {
        [SerializeField] public TableItemsHolder tableItemHolder;
        [SerializeField] private GameDataController gameDataController;

        [SerializeField] private GameObject jobsContent;
        [SerializeField] private GameObject skillsContent;
        [SerializeField] private GameObject itemsContent;

        [SerializeField] private GameObject tableItemSliderPrefab;
        [SerializeField] private GameObject tableItemTogglePrefab;

        public List<GameObject> rowsJob = new();
        public List<GameObject> rowsSkill = new();
        public List<GameObject> rowsItems = new();

        public void Start()
        {
            foreach (var item in tableItemHolder.JobsHeader)
            {
                item.JobHeader.GetComponent<TableHeader>().changeMenuLevelButton.onClick.AddListener(() => {
                    gameDataController.SetMenuLevel();
                    SetHeaderValues();
                    SetTableForMenuLevel();
                });
            }

            foreach (var item in tableItemHolder.SkillsHeader)
            {
                item.SkillsHeader.GetComponent<TableHeader>().changeMenuLevelButton.onClick.AddListener(() => {
                    gameDataController.SetMenuLevel();
                    SetHeaderValues();
                    SetTableForMenuLevel();
                });
            }
        }

        public void SetHeaderValues()
        {
            // set value for headers
            // level 1 - Level / Income
            // level 2 - xpday / xpleft
            // level 3 - max level / Skip(only for skills) 
        }

        public void SetTableForMenuLevel()
        {
            //sets value based of header menu value
            // level 1 - Level / Income (enable text, disable skip toggle)
            // level 2 - xpday / xpleft (enable text, disable skip toggle)
            // level 3 - max level / Skip(only for skills - enable button and disable value 1) 
        }

        public void InstantiateJobTableItem(PaEnums.JobCategories category, string name)
        {
            // if we already added this don't add another one
            foreach (var row in rowsJob)
            {
                if(row.GetComponent<TableItemSlider>().JobSkillName == name)
                {
                    return;
                }
            }

            GameObject footer = null;
            foreach (var item in tableItemHolder.JobsHeader)
            {
                if(item.category == category)
                {
                    footer = item.JobsRequirementText; 
                    break;
                }
            }

            if(footer != null)
            {
                GameObject tableItem = null;
                JobTask jobTask = gameDataController.GetJob(name);

                if(jobTask != null && jobTask.category == category)
                {
                    tableItem = Instantiate(tableItemSliderPrefab, jobsContent.transform);
                    tableItem.transform.SetSiblingIndex(footer.transform.GetSiblingIndex());
                    tableItem.GetComponent<TableItemSlider>().JobSkillName = jobTask.name;

                    if (jobTask.active)
                    {
                        tableItem.GetComponent<TableItemSlider>().setActive(true);
                    }

                    EventTrigger trigger = tableItem.GetComponent<TableItemSlider>().slider.gameObject.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new();
                    entry.eventID = EventTriggerType.PointerClick;
                    entry.callback.AddListener( (eventData) =>
                    {
                        for (int i = 0; i < tableItemHolder.Jobs.Count(); i++)
                        {
                            if (tableItemHolder.Jobs[i].jobTask.name == name)
                            {
                                DisableAllJobs();
                                tableItemHolder.Jobs[i].jobTask.active = true;
                                tableItem.GetComponent<TableItemSlider>().setActive(true);
                                gameDataController.gameDataHolder.currentJob = name;
                                break;
                            }
                        }
                    });
                    trigger.triggers.Add(entry);

                    rowsJob.Add(tableItem);
                }
            }
        }

        public void InstantiateSkillTableItem(PaEnums.SkillCategories category, string name)
        {
            // if we already added this don't add another one
            foreach (var row in rowsSkill)
            {
                if (row.GetComponent<TableItemSlider>().JobSkillName == name)
                {
                    return;
                }
            }

            GameObject footer = null;
            foreach (var item in tableItemHolder.SkillsHeader)
            {
                if (item.category == category)
                {
                    footer = item.SkillRequirementText;
                    break;
                }
            }

            if (footer != null)
            {
                GameObject tableItem = null;
                SkillTask skillTask = gameDataController.GetSkill(name);

                if (skillTask != null && skillTask.category == category)
                {
                    tableItem = Instantiate(tableItemSliderPrefab, skillsContent.transform);
                    tableItem.transform.SetSiblingIndex(footer.transform.GetSiblingIndex());
                    tableItem.GetComponent<TableItemSlider>().JobSkillName = skillTask.name;

                    if(skillTask.active)
                    {
                        tableItem.GetComponent<TableItemSlider>().setActive(true);
                    }

                    tableItem.GetComponent<TableItemSlider>().secondValueToggleContainer
                        .GetComponent<Toggle>().onValueChanged.AddListener(toggle =>
                        {
                            for (int i = 0; i < tableItemHolder.Skills.Count(); i++)
                            {
                                if (tableItemHolder.Skills[i].skillTask.name == name)
                                {
                                    tableItemHolder.Skills[i].skillTask.autoLearn = toggle;
                                    break;
                                }
                            }
                        });

                    EventTrigger trigger = tableItem.GetComponent<TableItemSlider>().slider.gameObject.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new();
                    entry.eventID = EventTriggerType.PointerClick;
                    entry.callback.AddListener((eventData) =>
                    {
                        for (int i = 0; i < tableItemHolder.Skills.Count(); i++)
                        {
                            if (tableItemHolder.Skills[i].skillTask.name == name)
                            {
                                DisableAllSkills();
                                tableItemHolder.Skills[i].skillTask.active = true;
                                tableItem.GetComponent<TableItemSlider>().setActive(true);
                                gameDataController.gameDataHolder.currentSkill = name;
                                break;
                            }
                        }
                    });
                    trigger.triggers.Add(entry);

                    rowsSkill.Add(tableItem);
                }
            }
        }

        public void InstantiateItemTableItem(PaEnums.MarketCategories category, string name)
        {
            // if we already added this don't add another one
            foreach (var row in rowsItems)
            {
                if (row.GetComponent<TableItemToggle>().ItemName == name)
                {
                    return;
                }
            }

            GameObject footer = null;
            foreach (var item in tableItemHolder.ItemHeaders)
            {
                if (item.category == category)
                {
                    footer = item.ItemRequirementText;
                    break;
                }
            }

            if (footer != null)
            {
                GameObject tableItem = null;
                ItemBase itemBase = gameDataController.GetItem(name);

                if (itemBase != null && itemBase.category == category)
                {
                    tableItem = Instantiate(tableItemTogglePrefab, itemsContent.transform);
                    tableItem.transform.SetSiblingIndex(footer.transform.GetSiblingIndex());
                    tableItem.GetComponent<TableItemToggle>().ItemName = itemBase.name;

                    if (itemBase.active)
                    {
                        tableItem.GetComponent<TableItemToggle>().toggle.isOn = true;
                    }

                    tableItem.GetComponent<TableItemToggle>().toggle.GetComponent<Toggle>().onValueChanged.AddListener(toggle =>
                    {
                        for (int i = 0; i < tableItemHolder.Items.Count(); i++)
                        {
                            if (tableItemHolder.Items[i].item.name == name)
                            {
                                if (toggle)
                                {
                                    if (tableItemHolder.Items[i].item.category == PaEnums.MarketCategories.Settlement)
                                    {
                                        DisableAllButSettlements(name);
                                        tableItem.GetComponent<TableItemToggle>().toggle.GetComponent<Toggle>().isOn = true;
                                        gameDataController.gameDataHolder.currentSettlement = name;
                                        tableItemHolder.Items[i].item.active = true;
                                        break;
                                    }

                                    gameDataController.gameDataHolder.currentRent.Add(name);
                                }
                                else
                                {
                                    gameDataController.gameDataHolder.currentRent.Remove(name);
                                }
                                tableItemHolder.Items[i].item.active = toggle;
                                break;
                            }
                        }
                    });

                    rowsItems.Add(tableItem);
                }
            }
        }

        public void EnableJob(string Name)
        {
            foreach (var row in rowsJob)
            {
                if (row.GetComponent<TableItemSlider>().JobSkillName == Name)
                {
                    row.GetComponent<TableItemSlider>().setActive(true);
                    break;
                }
            }
        }

        public void EnableSkill(string Name)
        {
            foreach (var row in rowsSkill)
            {
                if (row.GetComponent<TableItemSlider>().JobSkillName == Name)
                {
                    row.GetComponent<TableItemSlider>().setActive(true);
                    break;
                }
            }
        }

        public void EnableItem(string Name)
        {
            foreach (var row in rowsItems)
            {
                if (row.GetComponent<TableItemToggle>().ItemName == Name)
                {
                    row.GetComponent<TableItemToggle>().toggle.isOn = true;
                    break;
                }
            }
        }

        public void DisableAllJobs()
        {
            for (int i = 0; i < tableItemHolder.Jobs.Count(); i++)
            {
                tableItemHolder.Jobs[i].jobTask.active = false;
            }

            for (int i = 0; i < rowsJob.Count; i++)
            {
                rowsJob[i].GetComponent<TableItemSlider>().setActive(false);
            }
        }

        public void DisableAllSkills()
        {
            for (int i = 0; i < tableItemHolder.Skills.Count(); i++)
            {
                tableItemHolder.Skills[i].skillTask.active = false;
            }

            for (int i = 0; i < rowsSkill.Count; i++)
            {
                rowsSkill[i].GetComponent<TableItemSlider>().setActive(false);
            }
        }

        public void DisableAllRents()
        {
            for (int i = 0; i < tableItemHolder.Items.Count(); i++)
            {
                if (tableItemHolder.Items[i].item.category == PaEnums.MarketCategories.Rent)
                {
                    tableItemHolder.Items[i].item.active = false;

                    for (int j = 0; j < rowsItems.Count; j++)
                    {
                        if (tableItemHolder.Items[i].item.name == rowsItems[j].GetComponent<TableItemToggle>().ItemName)
                        {
                            rowsItems[j].GetComponent<TableItemToggle>().toggle.isOn = false;
                        }
                    }
                }
            }
        }

        public void DisableAllSettlements()
        {
            for (int i = 0; i < tableItemHolder.Items.Count(); i++)
            {
                if (tableItemHolder.Items[i].item.category == PaEnums.MarketCategories.Settlement)
                {
                    tableItemHolder.Items[i].item.active = false;

                    for (int j = 0; j < rowsItems.Count; j++)
                    {
                        if (tableItemHolder.Items[i].item.name == rowsItems[j].GetComponent<TableItemToggle>().ItemName)
                        {
                            rowsItems[j].GetComponent<TableItemToggle>().toggle.isOn = false;
                        }
                    }
                }
            }
        }

        public void DisableAllButSettlements(string Name)
        {
            for (int i = 0; i < tableItemHolder.Items.Count(); i++)
            {
                if (tableItemHolder.Items[i].item.category == PaEnums.MarketCategories.Settlement &&
                    tableItemHolder.Items[i].item.name != Name)
                {
                    tableItemHolder.Items[i].item.active = false;

                    for (int j = 0; j < rowsItems.Count; j++)
                    {
                        if (tableItemHolder.Items[i].item.name == rowsItems[j].GetComponent<TableItemToggle>().ItemName 
                            && rowsItems[j].GetComponent<TableItemToggle>().ItemName != Name)
                        {
                            rowsItems[j].GetComponent<TableItemToggle>().toggle.isOn = false;
                        }
                    }
                }
            }
        }

        public void DisableAllItems()
        {
            DisableAllRents();
            DisableAllSettlements();

            for (int i = 0; i < rowsItems.Count; i++)
            {
                rowsItems[i].GetComponent<TableItemToggle>().toggle.isOn = false;
            }
        }

        public void SetJobFirstValue(string name, string value)
        {
            foreach(var row in rowsJob) 
            {
                if(row.GetComponent<TableItemSlider>().JobSkillName == name)
                {
                    row.GetComponent<TableItemSlider>().firstValue.text = value;
                }
            }
        }

        public void SetJobSecondValue(string name, string value)
        {
            foreach (var row in rowsJob)
            {
                if (row.GetComponent<TableItemSlider>().JobSkillName == name)
                {
                    row.GetComponent<TableItemSlider>().secondValue.text = value;
                }
            }
        }

    }
}
