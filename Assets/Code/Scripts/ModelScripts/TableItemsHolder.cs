using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ProgressApocalypse.TableStructs;

namespace ProgressApocalypse
{
    public class TableItemsHolder : MonoBehaviour
    {
        [Header("Jobs")]
        public JobsHeaders[] JobsHeader;
        public List<Job> JobsClone;
        [SerializeField] private List<Job> Jobs;

        [Header("Skills")]
        public SkillHeaders[] SkillsHeader;
        public List<Skill> SkillsClone;
        [SerializeField] private List<Skill> Skills;

        [Header("Items")]
        public ItemHeaders[] ItemHeaders;
        public List<Item> ItemsClone;
        [SerializeField] private List<Item> Items;

        private void Start()
        {
            foreach (var job in Jobs)
            {
                JobsClone.Add(Instantiate(job));
            }

            foreach (var skill in Skills)
            {
                SkillsClone.Add(Instantiate(skill));
            }

            foreach (var item in Items)
            {
                ItemsClone.Add(Instantiate(item));
            }
        }
    }
}
