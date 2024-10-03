using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace sg
{
    public class AI_HUB_manager : MonoBehaviour
    {
        


        [Header("Status_bar")]
        [SerializeField] public AI_UI_status_Bar HPbar;

        [Header("slider Status")]

        [SerializeField] public GameObject open_or_close_bar;
        [SerializeField] public float Barappeartime = 3;




        [HideInInspector] public bool openBar = false;

        [HideInInspector] public float timer = 0;


        private void Update()
        {
            if (openBar)
            {

                timer += Time.deltaTime;

                if (timer >= Barappeartime)
                {
                    timer = 0;
                    openBar = false;
                    open_or_close_bar.SetActive(false);
                }

            }
        }

       
        



       
    }
}
