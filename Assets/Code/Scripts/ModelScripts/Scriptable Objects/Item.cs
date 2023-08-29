using BreakInfinity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace ProgressApocalypse
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class Item : ScriptableObject
    {       
        [Header("Item Specific")]
        public ItemBase item;
    }
}
