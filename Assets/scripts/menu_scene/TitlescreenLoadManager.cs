using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class TitlescreenLoadManager : MonoBehaviour
    {
        Playercontrols playercontrols;
        [Header("Title Screen Inputs ")]
        [SerializeField] bool deleteCharacterslot = false;

        private void Update()
        {
            if(deleteCharacterslot)
            {
                deleteCharacterslot = false;
                titlescreenmanager.instance.attempttodeletecharacterSlot();
            }
        }

        private void OnEnable()
        {
            if (playercontrols == null)
            {
                playercontrols = new Playercontrols();
                playercontrols.PlayerUi.delete_save_option.performed += i => deleteCharacterslot = true;
            }
            playercontrols.Enable();
        }
        private void OnDisable() 
        { 
            playercontrols.Disable();
        }
    }

}
