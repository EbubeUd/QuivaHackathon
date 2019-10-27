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
        private Vector2 scene;
        public Image SettingsCoverImage;

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
                    SettingsCoverImage.gameObject.SetActive(true);

                    HomePanel.SetActive(true);

                    break;
                case Enums.Pages.Score:
                    SettingsCoverImage.gameObject.SetActive(false);
                    HomePanel.SetActive(true);
                    ScorePanel.SetActive(true);
                    break;
                case Enums.Pages.Shop:
                    SettingsCoverImage.gameObject.SetActive(false);
                    HomePanel.SetActive(true);
                    PowerUpPanel.SetActive(true);
                    break;
                case Enums.Pages.Tutorial:
                    SettingsCoverImage.gameObject.SetActive(false);
                    HomePanel.SetActive(true);
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
