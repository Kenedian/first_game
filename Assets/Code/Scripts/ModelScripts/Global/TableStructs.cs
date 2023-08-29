using System;
using UnityEngine;

namespace ProgressApocalypse
{
    public static class TableStructs
    {
        [Serializable]
        public struct JobsHeaders
        {
            public PaEnums.JobCategories category;
            public GameObject JobHeader;
            public GameObject JobsRequirementText;
        }
        [Serializable]
        public struct SkillHeaders
        {
            public PaEnums.SkillCategories category;
            public GameObject SkillsHeader;
            public GameObject SkillRequirementText;
        }
        [Serializable]
        public struct ItemHeaders
        {
            public PaEnums.MarketCategories category;
            public GameObject ItemsHeader;
            public GameObject ItemRequirementText;
        }
    }
}
