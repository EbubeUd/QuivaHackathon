using Assets.Matic;
using Assets.Models;
using Assets.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class PowerUp : MonoBehaviour
    {
        public Button Arrow1Btn;
        public Button Arrow2Btn;
        public Button Arrow3Btn;
        public Button CancelBtn;

        private void Start()
        {
            CancelBtn.onClick.AddListener(delegate { ClosePowerUp(); });
           // Arrow1Btn.onClick.AddListener( );
            Arrow2Btn.onClick.AddListener(PurchaseOrEquipArrowAsyn2);
            //Arrow3Btn.onClick.AddListener();
        }

        private void ClosePowerUp()
        {
            gameObject.SetActive(false);
        }


        async void PurchaseOrEquipArrowAsyn2()
        {
            int arrowIndex = 2;
            //Get the players List of Arrows
            string playerdetails = PlayerPrefs.GetString("playerDetails");
            if (String.IsNullOrEmpty(playerdetails)) return;
            PlayerDetails playerDetails = JsonConvert.DeserializeObject<PlayerDetails>(playerdetails);
            if (playerDetails.Arrows == null) playerDetails.Arrows = new List<int>();
            if (playerDetails.Arrows.Contains(arrowIndex))
            {
                EquipArrow(arrowIndex);
            }
            else
            {
                Arrow arrow = GetArrow(arrowIndex);
                
                //Get the Balance of the player
                var matic = Settings.GetMatic();
                var from = Settings.FROM_ADDRESS;
                var token = Settings.MATIC_TEST_TOKEN;

                var to = Settings.TO_ADDRESS;
                var amount = arrow.Price;

                await matic.TransferTokens(from, token, to, amount);
                Debug.Log($"TransferTokens finished");

                GetERC20Balance.instance.UpdateBalance();

                EquipArrow(arrowIndex);
            }
            
        
        }

        private void EquipArrow(int index)
        {
            //Get the PlayerDetails
            PlayerDetails playerDetails = new PlayerDetails();
            string playerdetailsString = PlayerPrefs.GetString("playerDetails");
            if (String.IsNullOrEmpty(playerdetailsString))
            {
                playerDetails.ActiveArrow = index;
                if(!playerDetails.Arrows.Contains(index)) playerDetails.Arrows.Add(index);
            }
            else
            {
                playerDetails = JsonConvert.DeserializeObject<PlayerDetails>(playerdetailsString);
                playerDetails.ActiveArrow = index;
                if (playerDetails.Arrows == null) playerDetails.Arrows = new List<int>();
                if (!playerDetails.Arrows.Contains(index)) playerDetails.Arrows.Add(index);
            }

            playerdetailsString = JsonConvert.SerializeObject(playerDetails);
            PlayerPrefs.SetString("playerDetails", playerdetailsString);
        }



        Arrow GetArrow(int index)
        {
            Arrow arrow = new Arrow();
            arrow.Id = index;
            switch (index)
            {
                case 1:
                    arrow.Price = 0;
                    break;
                case 2:
                    arrow.Price = 100000000000000000;
                    break;
                case 3:
                    arrow.Price = 1000000000000000000;
                    break;
            }
            return arrow;
        }

    }
}
