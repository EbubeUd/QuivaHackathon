using Assets.Matic;
using Assets.Models;
using Assets.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
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
            Arrow1Btn.onClick.AddListener(delegate { PurchaseOrEquipArrowAsync(1).GetAwaiter().GetResult(); });
            Arrow2Btn.onClick.AddListener(delegate { PurchaseOrEquipArrowAsync(2).GetAwaiter().GetResult(); });
            CancelBtn.onClick.AddListener(delegate { PurchaseOrEquipArrowAsync(3).GetAwaiter().GetResult(); });
        }

        private void ClosePowerUp()
        {
            gameObject.SetActive(false);
        }


         async Task PurchaseOrEquipArrowAsync(int arrowIndex)
        {
            if (arrowIndex == 1)
            {
                EquipArrow(1);
            }
            Arrow arrow = GetArrow(arrowIndex);
            var balance = await BalanceOfERC20();
            int division = (int)BigInteger.Divide(balance, new BigInteger(1000000000000000000));
            if(division < arrow.Price)
            {
                Debug.Log("Insufficient Funds!");
                return;
            }
            //Get the Balance of the player
            arrow.Price = new BigInteger(arrowIndex);
        
        }

        private void EquipArrow(int index)
        {
            //Get the PlayerDetails
            string playerdetailsString = PlayerPrefs.GetString("playerDetails");
            if (String.IsNullOrEmpty(playerdetailsString)) playerdetailsString = JsonConvert.SerializeObject(new PlayerDetails() { arrow = 1, Address = "", PrivateKey ="" });
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
                    arrow.Price = 20;
                    break;
                case 3:
                    arrow.Price = 25;
                    break;
            }
            return arrow;
        }

        public static async Task<BigInteger> BalanceOfERC20()
        {
            var matic = Settings.GetMatic();
            return await matic.BalanceOfERC20(Settings.FROM_ADDRESS, Settings.ROPSTEN_TEST_TOKEN, true);
        }
    }
}
