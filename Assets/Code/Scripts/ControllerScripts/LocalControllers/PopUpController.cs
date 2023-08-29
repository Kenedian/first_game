using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProgressApocalypse
{
    public class PopUpController : MonoBehaviour
    {
        [SerializeField] private PopUpYesNo PopUpYesNo;

        public delegate void MethodAction(bool confirmed);

        public void OpenYesNoPopUp(string headerText, string descriptionText, MethodAction action)
        {
            PopUpYesNo.transform.parent.gameObject.SetActive(true);
            PopUpYesNo.gameObject.SetActive(true);

            PopUpYesNo.HeaderText.text = headerText;
            PopUpYesNo.DescriptionText.text = descriptionText;

            PopUpYesNo.ConfirmButton.onClick.AddListener(delegate
            {
                YesNoConfirmClicked(action);
            });

            PopUpYesNo.CancelButton.onClick.AddListener(delegate
            {
                YesNoCancelClicked(action);
            });

            PopUpYesNo.BackgroundButton.onClick.AddListener(delegate
            {
                YesNoCancelClicked(action);
            });
        }
        private void YesNoConfirmClicked(MethodAction action)
        {
            PopUpYesNo.transform.parent.gameObject.SetActive(false);
            PopUpYesNo.gameObject.SetActive(false);
            action(true);
            PopUpYesNo.ConfirmButton.onClick.RemoveAllListeners();
            PopUpYesNo.CancelButton.onClick.RemoveAllListeners();
            PopUpYesNo.BackgroundButton.onClick.RemoveAllListeners();
        }

        private void YesNoCancelClicked(MethodAction action)
        {
            PopUpYesNo.transform.parent.gameObject.SetActive(false);
            PopUpYesNo.gameObject.SetActive(false);
            action(false);
            PopUpYesNo.ConfirmButton.onClick.RemoveAllListeners();
            PopUpYesNo.CancelButton.onClick.RemoveAllListeners();
            PopUpYesNo.BackgroundButton.onClick.RemoveAllListeners();
        }

        // TODO: REMOVE (TESTING ONLY) - THIS IS HOW YOU USE IT
        public void OnOpenSampleYesNo()
        {
            //this is how you open pop up with yes or no
            OpenYesNoPopUp("Rebirth", "Are you sure you want to rebirth?", SimpleMethod);
        }

        public void SimpleMethod(bool confirmed)
        {
            // this is the method where you want to know if its confirmed or not
            Debug.Log("Got the result: " + confirmed);
        }
    }
}
