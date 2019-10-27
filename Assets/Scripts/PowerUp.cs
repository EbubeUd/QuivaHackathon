using Assets.Matic;
using Assets.Models;
using Assets.Scripts;
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
            Arrow1Btn.onClick.AddListener(delegate { PurchaseOrEquipArrow(1).GetAwaiter().GetResult(); });
            Arrow2Btn.onClick.AddListener(delegate { PurchaseOrEquipArrow(2).GetAwaiter().GetResult(); });
            CancelBtn.onClick.AddListener(delegate { PurchaseOrEquipArrow(3).GetAwaiter().GetResult(); });
        }

        private void ClosePowerUp()
        {
            gameObject.SetActive(false);
        }


        async Task PurchaseOrEquipArrow(int arrowIndex)
        {
            Arrow arrow = new Arrow();
            arrow.Price = new BigInteger(0.1);
            //Check if the player has enough cash
            BigInteger balance = await MaticClient.instance.GetERC20Balance();
            Debug.Log(balance);
            if (balance < arrow.Price) return;
            
        }


     
    }
}
