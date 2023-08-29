using DTT.UI.ProceduralUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressApocalypse
{
    public class TableItemSlider : MonoBehaviour
    {
        public string JobSkillName;

        public RoundedImage fillColor;
        public Slider slider;        

        public TMP_Text sliderText;
        public TMP_Text firstValue;
        public TMP_Text secondValue;
        public GameObject secondValueToggleContainer;

        private Color32 selectedColor = new Color32(113, 105, 145, 255);
        private Color32 disabledColor = new Color32(145, 145, 105, 255);

        private void Start()
        {
            this.sliderText.text = JobSkillName;
        }

        public void setActive(bool active)
        {
            fillColor.color = active ? selectedColor : disabledColor;
        }
    }
}
