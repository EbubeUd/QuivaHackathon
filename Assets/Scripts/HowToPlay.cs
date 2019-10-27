
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class HowToPlay : MonoBehaviour
    {
        public Button CancelButton;

        private void Start()
        {
            CancelButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        }
    }
}
