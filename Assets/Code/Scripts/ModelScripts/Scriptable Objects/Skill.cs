using UnityEngine;

namespace ProgressApocalypse
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Tasks/Skill")]
    public class Skill : ScriptableObject
    {
        public SkillTask skillTask;
    }
}
