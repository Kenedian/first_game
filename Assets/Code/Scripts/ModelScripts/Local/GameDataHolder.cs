using BreakInfinity;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressApocalypse
{
    public class GameDataHolder: MonoBehaviour
    {
        public BigDouble resources = 0;
        public int days = 365 * 14;
        public float technology = 0;

        public float gameSpeed = 1;
        public int lifeSpan = 365 * 70;

        public bool paused = false;
        public bool timeBoostEnabled = true;
        public bool autoPromote = false;
        public bool autoLearn = false;

        public bool loggedIn = false;

        public bool boughtDisableAds = false;
        public float speedBoostRemainingTime = 0;
        public float moraleBoostRemainingTime = 0;

        public int rebirthOneCount = 0;
        public int rebirthTwoCount = 0;

        public string currentJob = "Scavenger";
        public string currentSkill = "Motivation";
        public string currentSettlement = "Cave";
        public List<string> currentRent = new();

        public int menuLevel = 1;
        public bool gameOver = false;
    }
}
