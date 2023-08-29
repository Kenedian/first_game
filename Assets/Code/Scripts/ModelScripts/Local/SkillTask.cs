using System;
using UnityEngine;
using static ProgressApocalypse.PaEnums;

namespace ProgressApocalypse
{
    [Serializable]
    public class SkillTask : Task
    {
        [Header("SkillSpecific")]
        public SkillCategories category;
        public float effectAmmountPerLevel;
        public string effectDescription;
        public bool autoLearn = false;

        public float GetEffectAmmount()
        {
            if(name == "Bargaining" || name == "Amplification")
            {
                float multiplier = 1 - Mathf.Log(7) / Mathf.Log(level + 1) / 10;
                if(multiplier < 0.1f)
                {
                    multiplier = 0.1f;
                }

                return multiplier;
            }
            else if(name == "Time Travel")
            {
                return 1 + Mathf.Log(13) / Mathf.Log(level + 1);
            }
            else if (name == "Containment")
            {
                return 1 + Mathf.Log(33) / Mathf.Log(level + 1);
            }

            return 1 + (effectAmmountPerLevel * level);
        }

        public string GetEffectDescription()
        {
            return "x" + GetEffectAmmount() + " " + effectDescription;
        }
    }
}
