using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class Menu : MonoBehaviour
    {
        public GameObject HomePanel;
        public GameObject TutorialPanel;
        public GameObject ScorePanel;
        public GameObject PowerUpPanel;
        public AudioSource BackgroundMusic;
        public static Menu instance;

        void Start()
        {
            if (instance == null) instance = this;
 
        }

        public void SwitchPage(Enums.Pages page)
        {
            HideAllPages();
            switch (page)
            {
                case Enums.Pages.Home:
                    HomePanel.SetActive(true);
                    break;
                case Enums.Pages.Score:
                    HomePanel.SetActive(true);
                    ScorePanel.SetActive(true);
                    break;
                case Enums.Pages.Shop:
                    HomePanel.SetActive(true);
                    PowerUpPanel.SetActive(true);
                    break;
                case Enums.Pages.Tutorial:
                    TutorialPanel.SetActive(true);
                    break;
            }
        }

        void HideAllPages()
        {
            HomePanel.SetActive(false);
            TutorialPanel.SetActive(false);
            ScorePanel.SetActive(false);
            PowerUpPanel.SetActive(false);
        }


    }
}
