using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class Home : MonoBehaviour
    {
        public Button TutorialButton;
        public Button PowerUpButton;
        public Button ScoreButton;
        public Button ExitButton;

        void Start()
        {
            TutorialButton.onClick.AddListener(delegate () { Tutorial(); });
            PowerUpButton.onClick.AddListener(delegate () { PowerUp(); });
            ScoreButton.onClick.AddListener(delegate () { Score(); });
            ExitButton.onClick.AddListener(delegate () { Exit(); });
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void Score()
        {
            Menu.instance.SwitchPage(Enums.Pages.Score);
        }

        private void PowerUp()
        {
            Menu.instance.SwitchPage(Enums.Pages.Shop);
        }

        private void Tutorial()
        {
            Menu.instance.SwitchPage(Enums.Pages.Tutorial);
        }
    }
}
