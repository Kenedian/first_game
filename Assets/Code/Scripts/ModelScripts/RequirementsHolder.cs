using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressApocalypse
{
    public class RequirementsHolder : MonoBehaviour
    {
        [Header("Task Requirements")]
        public List<TaskRequirement> TaskRequirements;

        [Header("Age Requirements")]
        public List<AgeRequirement> AgeRequirements;

        [Header("Resource Requirements")]
        public List<ResourceRequirement> ResourceRequirements;

        [Header("Technology Requirements")]
        public List<TechnologyRequirement> TechnologyRequirements;
    }
}
