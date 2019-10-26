using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class UISlideAnimation : MonoBehaviour
    {
        public float LerpTime = .2f;

        private bool isSliding;
        private bool isPopping;
        RectTransform rect;
        public bool IsOpen;


        public Vector3 SlideRepulsion;
        public Vector3 SlideOverslide;
        public Vector3 SlideFinalPosition;
        public Vector3 SlideInitialPosition;

        //Open UI
        public IEnumerator OpenUI()
        {
            if (!isSliding)
            {

                isSliding = true;
                float currentTime = 0;
                float normalizeTime = 0;

                Vector3 normalPosition = SlideInitialPosition;
                Vector3 minPosition;
                Vector3 maxPosition;
                Vector3 endPosition;

       
                minPosition = SlideRepulsion;
                maxPosition = SlideOverslide;
                endPosition = SlideFinalPosition;
                

                //take the object to the downPosition
                while (currentTime < LerpTime)
                {
                    currentTime += Time.deltaTime;
                    normalizeTime = currentTime / LerpTime;
                    rect.anchoredPosition = Vector3.Lerp(normalPosition, minPosition, normalizeTime);
                    yield return null;
                }
                //Take the object to the up position
                while (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                    normalizeTime = currentTime / LerpTime;
                    rect.anchoredPosition = Vector3.Lerp(maxPosition, minPosition, normalizeTime);
                    yield return null;
                }

                //Take the object to the normal position
                while (currentTime < LerpTime)
                {
                    currentTime += Time.deltaTime;
                    normalizeTime = currentTime / LerpTime;
                    rect.anchoredPosition = Vector3.Lerp(maxPosition, endPosition, normalizeTime);
                    yield return null;
                }
                IsOpen = true;
                isSliding = false;
            }

        }

    }
}
