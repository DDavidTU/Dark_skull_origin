using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class UI_status_Bar : MonoBehaviour
    {
        private Slider slider;
        private RectTransform rectTransform;

        [Header("slider Status")]
        [SerializeField] protected bool scaleBarLengthWithstatus = true;
        [SerializeField] protected float widthScaleMultiplier = 1;
        

        protected virtual void Awake()
        {

            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();

        }


        protected virtual void Start()
        {

        }
        public virtual void Setstatus(int newValue)
        {
            slider.value = newValue;
        }
        public virtual void SetMaxStaus(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;

            if (scaleBarLengthWithstatus)
            {
                rectTransform.sizeDelta= new Vector2(maxValue*widthScaleMultiplier,rectTransform.sizeDelta.y);

                playerUIManger.instance.playeruihubmanager.RefreshHUB();
              
            }
        }
    }

}
