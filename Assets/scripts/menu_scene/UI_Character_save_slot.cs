using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

namespace sg
{
    public class UI_Character_save_slot : MonoBehaviour
    {
        SaveFileDataWriter savewriter;
        [Header("Game Slot")]
        public characterslot characterslot;
        [Header("Game Info")]
        public TextMeshProUGUI character_name;
        public TextMeshProUGUI character_playtime;

        private void OnEnable()
        {
            LoadSaveSlot();
        }
        private void LoadSaveSlot()
        {
            savewriter = new SaveFileDataWriter();
            savewriter.saveDataDirectoryPath=Application.persistentDataPath;

            switch (characterslot)
            {
                case characterslot.characterslot01:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave01.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                    
                case characterslot.characterslot02:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave02.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }

                    break;
                case characterslot.characterslot03:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave03.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot04:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave04.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot05:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave05.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot06:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave06.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot07:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave07.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot08:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave08.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot09:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave09.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case characterslot.characterslot10:
                    savewriter.saveFilename = worldsavegamemanger.instance.DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot);
                    if (savewriter.CheckToSeeFileExists())
                    {
                        character_name.text = worldsavegamemanger.instance.charactersave10.character_name;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                default:
                    break;

                    
            }

           
            
        }

        public void LoadGameFromCharacterSlot()
        {
            worldsavegamemanger.instance.currentusingslot= characterslot ;
            worldsavegamemanger.instance.LoadGame();

        }

        public void SelectCurrentSlot()
        {
            titlescreenmanager.instance.selectcharacterSlot(characterslot);
        }

    }
}
