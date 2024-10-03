using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor;

namespace sg
{
    public class titlescreenmanager : MonoBehaviour
    {
        public static titlescreenmanager instance;
        
        [Header("Menu")]
        [SerializeField] GameObject TittleScreenMainMenu;
        [SerializeField] GameObject TittleScreenLoadMenu;
        [Header("buttom")]
        [SerializeField] Button LoadMenuReturnButtom;
        [SerializeField] Button TitleLoadGameButtom;
        [SerializeField] Button MainmenuNewGameButton;
        [SerializeField] Button deleteCharacterSlotconfirmButton;
        [Header("Pop up")]
        [SerializeField] GameObject nocharacterSlotsPopup;
        [SerializeField] Button noCharacterSlotsOkayButton;
        [SerializeField] GameObject deleteCharacterSlotPopup;
        [Header("character slot")]
        public  characterslot currentcharactersaveslot=characterslot.Noslot ;
      
     



        private void Awake()
        {
            if(instance == null) 
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void startnetworkashost()
        {
            NetworkManager.Singleton.StartHost();

        }

        public void StartNewGame()
        {
            worldsavegamemanger.instance.AttemptToCreativeNewGame();
            ControlPP.instance.vg.intensity.value = 0;

        }

        public void openLoadGame()
        {
            TittleScreenMainMenu.SetActive(false);
            TittleScreenLoadMenu.SetActive(true);
            LoadMenuReturnButtom.Select();
            ControlPP.instance.vg.intensity.value = 0;
        }
        public void returnTitleMenu()
        {
            TittleScreenLoadMenu.SetActive(false);
            TittleScreenMainMenu.SetActive(true);
            TitleLoadGameButtom.Select();
        }

        public void DisplayNoFreecharacterSlotsPopup()
        {
            nocharacterSlotsPopup.SetActive(true);
            noCharacterSlotsOkayButton.Select();
        }
        public void CloseNoFreecharacterSlotsPopup()
        {
            nocharacterSlotsPopup.SetActive(false);
            MainmenuNewGameButton.Select();
        }
        public void selectcharacterSlot(characterslot charactersaveslot)
        {
            currentcharactersaveslot=charactersaveslot;
        }

        public void selectNoslot()
        {
            currentcharactersaveslot = characterslot.Noslot;
        }

        public void attempttodeletecharacterSlot()
        {
            if(currentcharactersaveslot!=characterslot.Noslot)
            
            {
                deleteCharacterSlotPopup.SetActive(true);
                deleteCharacterSlotconfirmButton.Select();
            }
        }
        public void Deletecharacterslot()
        {
            deleteCharacterSlotPopup.SetActive(false);
            worldsavegamemanger.instance.DeleteGame(currentcharactersaveslot);
            TittleScreenLoadMenu.SetActive(false );
            TittleScreenLoadMenu.SetActive(true);
            LoadMenuReturnButtom.Select();
         
        }

        public void closeDeletePopUp()
        {
            deleteCharacterSlotPopup.SetActive(false);
            LoadMenuReturnButtom.Select();
        }

        public void ExitGame()
        {
            Application.Quit();
            
            
        }
    }

}
