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
        public List<Job> Jobs;

        [Header("Skills")]
        public SkillHeaders[] SkillsHeader;
        public List<Skill> Skills;

        [Header("Items")]
        public ItemHeaders[] ItemHeaders;
        public List<Item> Items;
    }
}
