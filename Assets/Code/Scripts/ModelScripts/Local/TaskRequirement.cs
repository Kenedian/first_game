using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressApocalypse
{
    [Serializable]
    public class TaskRequirement
    {
        public string nameOfUnlock;
        public List<Requirement> requirements = new List<Requirement>();

        [Serializable]
        public struct Requirement
        {
            public string nameOfTask;
            public int levelRequired;
        }
    }
}
