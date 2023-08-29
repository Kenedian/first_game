using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProgressApocalypse
{
    public class RequirementsController : MonoBehaviour
    {
        [SerializeField] private TableItemsHolder tableItemHolder;
        [SerializeField] private RequirementsHolder requirementsHolder;
        [SerializeField] private GameDataHolder gameDataHolder;

        public bool IsRequirementFulfilled(PaEnums.RequirementTypes type, string name)
        {
            if(type == PaEnums.RequirementTypes.Task)
            {
                var requirement = requirementsHolder.TaskRequirements.Single(x => x.nameOfUnlock == name);
                for (int i = 0; i < requirement.requirements.Count(); i++)
                {
                    if (!IsTaskFulfilled(requirement.requirements[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
            else if(type == PaEnums.RequirementTypes.Age) 
            {
                var requirement = requirementsHolder.AgeRequirements.Single(x => x.nameOfUnlock == name);
                return gameDataHolder.days >= requirement.ageRequired * 365;
            }
            else if(type == PaEnums.RequirementTypes.Technology)
            {
                var requirement = requirementsHolder.TechnologyRequirements.Single(x => x.nameOfUnlock == name);
                return gameDataHolder.technology >= requirement.technologyRequired;
            }
            else if(type == PaEnums.RequirementTypes.Resource)
            {
                var requirement = requirementsHolder.ResourceRequirements.Single(x => x.nameOfUnlock == name);
                return gameDataHolder.resources >= requirement.resourcesRequired ;
            }

            return false;
        }

        public string getRequirementString(PaEnums.RequirementTypes type, string name)
        {
            string text = "Required: ";

            if (type == PaEnums.RequirementTypes.Task)
            {
                var requirement = requirementsHolder.TaskRequirements.Single(x => x.nameOfUnlock == name);
                for (int i = 0; i < requirement.requirements.Count(); i++)
                {
                    Task required = tableItemHolder.Jobs.Single(x => x.jobTask.name == requirement.requirements[i].nameOfTask).jobTask as Task ??
                            tableItemHolder.Skills.Single(x => x.skillTask.name == requirement.requirements[i].nameOfTask).skillTask;

                    if (!IsTaskFulfilled(requirement.requirements[i]))
                    {
                        text += $"{requirement.requirements[i].nameOfTask} level {required.level}/{requirement.requirements[i].levelRequired}" + (i >= requirement.requirements.Count ? "" : ",");
                    }
                }

            }
            else if (type == PaEnums.RequirementTypes.Age)
            {
                var requirement = requirementsHolder.AgeRequirements.Single(x => x.nameOfUnlock == name);
                text += requirement.ageRequired + " years";
            }
            else if (type == PaEnums.RequirementTypes.Technology)
            {
                var requirement = requirementsHolder.TechnologyRequirements.Single(x => x.nameOfUnlock == name);
                text += requirement.technologyRequired + " technology";
            }
            else if (type == PaEnums.RequirementTypes.Resource)
            {
                var requirement = requirementsHolder.ResourceRequirements.Single(x => x.nameOfUnlock == name);
                text += PaFunctions.FormatResources(requirement.resourcesRequired);
            }

            return text;
        }

        private bool IsTaskFulfilled(TaskRequirement.Requirement requirement)
        {
            bool fulfilled = false;

            Task required = tableItemHolder.Jobs.Single(x => x.jobTask.name == requirement.nameOfTask).jobTask as Task ??
                            tableItemHolder.Skills.Single(x => x.skillTask.name == requirement.nameOfTask).skillTask;

            if(required != null)
            {
                fulfilled = required.level >= requirement.levelRequired;
            }

            return fulfilled;
        }
    }
}
