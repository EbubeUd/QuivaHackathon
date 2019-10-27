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
        public GameObject LoginPanel;
        public InputField AddressInput;
        public InputField PrivateKeyInput;
        public Button LoginCancelBtn;
        public Button LoginBtn;

        private void Start()
        {
            CancelBtn.onClick.AddListener(delegate { ClosePowerUp(); });
            Arrow1Btn.onClick.AddListener(delegate { EquipArrow(1); });
            Arrow2Btn.onClick.AddListener(PurchaseOrEquipArrowAsyn2);
            Arrow3Btn.onClick.AddListener(PurchaseOrEquipArrowAsyn3);
            LoginCancelBtn.onClick.AddListener(delegate { LoginPanel.SetActive(false); });
            LoginBtn.onClick.AddListener(delegate { Login(); });
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
            if (String.IsNullOrEmpty(playerdetails))
            {
                //Show login
                LoginPanel.SetActive(true);
                return;
            };
            PlayerDetails playerDetails = JsonConvert.DeserializeObject<PlayerDetails>(playerdetails);
            if (playerDetails.Arrows == null) playerDetails.Arrows = new List<int>();
            playerDetails.Arrows.Remove(arrowIndex);
            if (playerDetails.Arrows.Contains(arrowIndex))
            {
                EquipArrow(arrowIndex);
            }
            else
            {
                
                Arrow arrow = GetArrow(arrowIndex);
                
                //Get the Balance of the player
                var matic = Settings.GetMatic();
                var from = playerDetails.Address;
                Settings.PRIVATE_KEY = playerDetails.PrivateKey;
                var token = Settings.ROPSTEN_TEST_TOKEN;

                var to = Settings.TO_ADDRESS;
                var amount = arrow.Price;

                string tHash = await matic.TransferTokens(from, token, to, amount);
                Debug.Log(tHash);
                Debug.Log($"TransferTokens finished");

                GetERC20Balance.instance.UpdateBalance();

                EquipArrow(arrowIndex);
            }
            
        
        }


        async void PurchaseOrEquipArrowAsyn3()
        {
            int arrowIndex = 3;
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

            gameObject.SetActive(false);
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


        public void Login()
        {
            string Address = AddressInput.text;
            string PrivateKey = PrivateKeyInput.text;

            if (String.IsNullOrEmpty(Address) || String.IsNullOrEmpty(PrivateKey)) return;

            //Get the User's Details
            string PlayerDetails = PlayerPrefs.GetString("playerDetails");
            PlayerDetails = JsonConvert.SerializeObject(new PlayerDetails() { Arrows = new List<int>(), Address = Address, PrivateKey = PrivateKey } );

            //Save the datails back
            PlayerPrefs.SetString("playerDetails", PlayerDetails);
            LoginPanel.SetActive(false);
               
        }

    }
}
