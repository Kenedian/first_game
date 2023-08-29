using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressApocalypse
{
    public class TableItemToggle : MonoBehaviour
    {
        public string ItemName;


        public Toggle toggle;

        public TMP_Text toggleText;
        public TMP_Text firstValue;
        public TMP_Text secondValue;

        private void Start()
        {
            this.toggleText.text = ItemName;
        }
    }
}
