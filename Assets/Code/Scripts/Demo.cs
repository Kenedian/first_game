using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProgressApocalypse
{
    public class Demo : MonoBehaviour
    {
        public PaEnums.JobCategories jobCategory;
        public string jobName;

        public PaEnums.SkillCategories skillCategory;
        public string skillName;

        public PaEnums.MarketCategories itemCategory;
        public string itemName;

        [SerializeField] private TableItemController tableItemController;

        public void OnDemoJob()
        {
            tableItemController.InstantiateJobTableItem(jobCategory, jobName);
        }

        public void OnDemoSkill()
        {
            tableItemController.InstantiateSkillTableItem(skillCategory, skillName);
        }

        public void OnDemoItem()
        {
            tableItemController.InstantiateItemTableItem(itemCategory, itemName);
        }
    }
}
