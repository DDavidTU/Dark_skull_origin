using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace sg { 
public class worldsavegamemanger : MonoBehaviour
{
        
        public static worldsavegamemanger instance;
        public PlayerManager player;
        [Header("Sava/Load")]
        [SerializeField] public bool saveGame;
        [SerializeField] public bool loadGame;

        [SerializeField] public List<GameObject> Aicharacter;

        [SerializeField] private Transform BossHPTrasform;
        
        [Header("Scene index")]
        [SerializeField] int worldsceneindex = 1;

        [Header("Savedata writer ")]
        private SaveFileDataWriter saveFileDataWriter;


        [Header("Character current")]
        public characterslot currentusingslot;
        public CharacterSave currentdata;
        private string FileName;

        [Header("Character slot")]
        public CharacterSave charactersave01;

       public CharacterSave charactersave02;
        public CharacterSave charactersave03;
        public CharacterSave charactersave04;
        public CharacterSave charactersave05;
        public CharacterSave charactersave06;
        public CharacterSave charactersave07;
        public CharacterSave charactersave08;
        public CharacterSave charactersave09;
        public CharacterSave charactersave10;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadAllCharacterProfile();
        }

        private void Update()
        {

         

            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }
            if (loadGame) 
            { 
            loadGame = false;

                for(int i=0; i< Aicharacter.Count; i++)
                {
                    Destroy(Aicharacter[i]);
                    
                }


                



                LoadGame();
            }
           


        }
        private void LateUpdate()
        {
            ClearList();
        }
        public string DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot characterslot)
        {
            string filename = " ";
            switch (characterslot) 
            {
                case characterslot.characterslot01:
                    filename = "character_01";
                    break;
                case characterslot.characterslot02:
                    filename = "character_02";
                    break; 
                case characterslot.characterslot03:
                    filename = "character_03";
                    break;
                case characterslot.characterslot04:
                    filename = "character_04";
                    break;
                case characterslot.characterslot05:
                    filename = "character_05";
                    break;
                case characterslot.characterslot06:
                    filename = "character_06";
                    break;
                case characterslot.characterslot07:
                    filename = "character_07";
                    break;
                case characterslot.characterslot08:
                    filename = "character_08";
                    break;
                case characterslot.characterslot09:
                    filename = "character_09";
                    break;
                case characterslot.characterslot10:
                    filename = "character_10";
                    break;

                default:
                    break;
            }
            return filename;
        }

        public void AttemptToCreativeNewGame()
        {

            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot01);

            if(!saveFileDataWriter.CheckToSeeFileExists()) 
            {
                currentusingslot=characterslot.characterslot01;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot02);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot02;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot03);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot03;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot04);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot04;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot05);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot05;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot06);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot06;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot07);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot07;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot08);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot08;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot09);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot09;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot10);

            if (!saveFileDataWriter.CheckToSeeFileExists())
            {
                currentusingslot = characterslot.characterslot10;

                currentdata = new CharacterSave();
                NewGame();
                return;
            }
            titlescreenmanager.instance.DisplayNoFreecharacterSlotsPopup();
            

        }

        private void NewGame()
        {
            player.playerNetcodeManger.characterName.Value = "character";
            player.playerNetcodeManger.Hpva.Value = 15;
            player.playerNetcodeManger.endurance.Value = 10;
            player.playerNetcodeManger.MaxHp.Value = player. playerstatusmanager.caculateHpBaseofHpValevel(player.playerNetcodeManger.Hpva.Value);
            player.playerNetcodeManger.currentHp.Value = player.playerNetcodeManger.MaxHp.Value;
            player.playerInventoryManager.currentconsumable.currentItemAmount = 5;
            player.playerAnimatorManager.PlayTargetactionAnimation("revive", true);
            playerUIManger.instance.playeruihubmanager.gameObject.SetActive(true);

            player.transform.position =new Vector3(0, 1, 0);
            player.IsDead.Value = false;
            SaveGame();
            StartCoroutine(LoadWorldScnece());
        }
        public void LoadGame()
        {
            player.playerAnimatorManager.PlayTargetactionAnimation("revive", true);
            player.IsDead.Value=false;
            playerUIManger.instance.playeruihubmanager.gameObject.SetActive(true);
            ControlPP.instance.vg.color.value = Color.red;

            FileName= DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(currentusingslot);

            saveFileDataWriter = new SaveFileDataWriter();

            saveFileDataWriter.saveDataDirectoryPath=Application.persistentDataPath;
            saveFileDataWriter.saveFilename = FileName;
            currentdata = saveFileDataWriter.Loadsave();

            StartCoroutine(LoadWorldScnece());

           
        }
        public void SaveGame()
        {
            FileName= DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(currentusingslot);

            saveFileDataWriter=new SaveFileDataWriter();

            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFilename = FileName;

            player.SaveGamedataTocurrentCharacter(ref currentdata);

            saveFileDataWriter.CreateNewCharacterSaveFile(currentdata);

        }

        public void DeleteGame(characterslot charactersaveslot)
        {
           

            saveFileDataWriter = new SaveFileDataWriter();

            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
           saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(charactersaveslot);
         
            saveFileDataWriter.DeleteSaveFile();
        }
        private void LoadAllCharacterProfile()
        {
            saveFileDataWriter= new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath= Application.persistentDataPath;
            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot01);
            charactersave01= saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot02);
            charactersave02 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot03);
            charactersave03 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot04);
            charactersave04 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot05);
            charactersave05 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot06);
            charactersave06 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot07);
            charactersave07 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot08);
            charactersave08 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot09);
            charactersave09 = saveFileDataWriter.Loadsave();

            saveFileDataWriter.saveFilename = DecieCharacterFilenameBaseOnCharacterSlotBeingSlotBeusing(characterslot.characterslot10);
            charactersave10 = saveFileDataWriter.Loadsave();
        }

        public IEnumerator LoadWorldScnece() 
        {
            
            AsyncOperation loadoperation = SceneManager.LoadSceneAsync(worldsceneindex);

           player.LoadGamedataTFromcurrentCharacter(ref currentdata );

            yield return null;
        }

        public int GetWorldsceneIndex()
        {
            return worldsceneindex;
        }
        private void ClearList()
        {
            for (int i = 0; i < Aicharacter.Count; i++)
            {
                if (Aicharacter[i] == null)
                {
                    Aicharacter.Remove(Aicharacter[i]);
                }


            }
           


            }

        public void DeadLoad()
        {
            loadGame = true;
        }
        
        public void ReturnMenu()
        {
            SceneManager.LoadScene(0);
            Destroy(player.gameObject);
            NetworkManager.Singleton.Shutdown();


        }

        public void StopOff()
        {
            Time.timeScale = 1;
        }
        public void StopOn()
        {
            Time.timeScale = 0;
        }
    }
    }

