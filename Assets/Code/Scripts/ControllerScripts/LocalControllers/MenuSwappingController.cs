using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressApocalypse
{
    public class MenuSwappingController : MonoBehaviour
    {
        public MenuItem[] menuItems;

        [Serializable]
        public struct MenuItem
        {
            public GameObject menuItem;
            public Button button;
        }

        public void Start()
        {
            var currentMenu = FindCurrentMenu();
            if(!currentMenu.IsUnityNull())
            {
                currentMenu.button.interactable = false;
                currentMenu.menuItem.SetActive(true);
            }

            foreach (var menuItem in menuItems) 
            {
                menuItem.button.onClick.AddListener(delegate 
                {
                    MenuSwap(menuItem.button);
                });
            }
        }

        private void MenuSwap(Button button)
        {
            foreach (var menuItem in menuItems)
            {
                if(menuItem.button == button)
                {
                    menuItem.button.interactable = false;
                    menuItem.menuItem.SetActive(true);
                }
                else
                {
                    menuItem.button.interactable = true;
                    menuItem.menuItem.SetActive(false);
                }
            }
        }

        private MenuItem FindCurrentMenu()
        {
            foreach(var menuItem in menuItems)
            {
                if(menuItem.menuItem.activeSelf == true)
                {
                    return menuItem;
                }
            }

            return new MenuItem();
        }
    }
}
