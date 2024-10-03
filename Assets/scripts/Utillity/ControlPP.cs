using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;


namespace sg
{
    public class ControlPP : MonoBehaviour
    {
        public static ControlPP instance;
        private Volume PP;
        public Vignette vg;
        private void Awake()
        {
            if (instance == null)
                instance = this;

            else
                Destroy(gameObject);


            PP = GetComponent<Volume>();
            PP.profile.TryGet(out vg);

            
            
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetVgValue(float oldValue, float newValue)
        {
            vg.intensity.value = newValue;
        }



    }
}
