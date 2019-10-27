using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{
    public class ScorePanel : MonoBehaviour
    {
        public Button CloseButton;

        private void Start()
        {
            CloseButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        }
    }
}
