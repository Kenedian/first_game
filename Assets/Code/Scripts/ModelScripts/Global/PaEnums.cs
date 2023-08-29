using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressApocalypse
{
    public static class PaEnums
    {
        public enum RequirementTypes
        {
            Task,
            Age,
            Technology,
            Resource
        }

        public enum JobCategories
        {
            BasicJob,
            CombatJob,
            EngineeringJob
        }

        public enum SkillCategories
        {
            BasicSkill,
            CombatSkill,
            EngineeringSkill,
            BiologicalEngineering
        }

        public enum MarketCategories
        {
            Settlement,
            Rent
        }
    }
}
