using UnityEngine;

namespace ProgressApocalypse
{
    [CreateAssetMenu(fileName = "New Job", menuName = "Tasks/Job")]
    public class Job : ScriptableObject
    {
        public JobTask jobTask;
    }
}
