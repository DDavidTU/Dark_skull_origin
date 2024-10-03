using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class UIWindows : MonoBehaviour
    {
        public static UIWindows instance;

     
        [SerializeField] public Button itemButton;

    

        private void Awake()
        {
            if(instance == null )
            {
                instance = this;
            }
            else
            {
                Destroy( gameObject );
            }

            
        }

      
    }
}
