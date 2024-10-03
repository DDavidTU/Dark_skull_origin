using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace sg
{

    public class playerUIManger : MonoBehaviour
    {
        public static playerUIManger instance;
        [HideInInspector] public playerUIHUBManager playeruihubmanager;
        [HideInInspector] public PlayeUIpopupManager playeUIpopupManager;
        [SerializeField] public EquipManagerUI equipManagerUI;
        [SerializeField] public playerInventoryManager playerInventorymanager;
        [SerializeField] public GameObject SystemMenu;
        [SerializeField] public Button ReTurnGameButton;
        


        [Header("call UI")]
        [SerializeField] public GameObject selectWindos;
        

        [Header("Equip Window")]

        public HandEquipmentslotUI[] handEquipmentslot;

        [SerializeField] public weaponitem unarm;

      




        [Header("weapon inventory UI")]
        public GameObject weaponinventorySlotpref;
        [SerializeField] public Transform weaponinventoryslotTransform;
        [SerializeField] public weaponinventorySlot[] weaponinventoryslots;
       


        public bool isopenUI = false;

        [Header("Network Join")]
        [SerializeField] bool startgameClient;
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
            playeruihubmanager = GetComponentInChildren<playerUIHUBManager>();
            playeUIpopupManager = GetComponentInChildren<PlayeUIpopupManager>();

            handEquipmentslot = equipManagerUI.GetComponentsInChildren<HandEquipmentslotUI>();
           
        }
        protected virtual void Start()
        {
            DontDestroyOnLoad(gameObject);
            weaponinventoryslots = weaponinventoryslotTransform.GetComponentsInChildren<weaponinventorySlot>() ;
            


        }
        private void Update()
        {
            if (startgameClient)
            {
                startgameClient = false;
                NetworkManager.Singleton.Shutdown();

                NetworkManager.Singleton.StartClient();
            }

            

        }
        public void UpdateUI()
        {
            Debug.Log("1");
            
            for (int i = 0; i < weaponinventoryslots.Length; i++)
            {
                Debug.Log("2");
                //複製solt，包括上面的程式和格子
                if (i< playerInventorymanager.weaponsInventory.Count)
                {
                    Debug.Log("3");
                    if (weaponinventoryslots.Length< playerInventorymanager.weaponsInventory.Count)
                    {
                        Debug.Log("4");
                        Instantiate(weaponinventorySlotpref, weaponinventoryslotTransform);
                        weaponinventoryslots = weaponinventoryslotTransform.GetComponentsInChildren<weaponinventorySlot>();
                    }
                    weaponinventoryslots[i].AddItem(playerInventorymanager.weaponsInventory[i]);

                }
                else
                {
                    Debug.Log("NO");
                    weaponinventoryslots[i].RemoveItem();
                }
               
            }

           

        }

        public void CloseAllUI()
        {
            selectWindos.SetActive(false);

            for (int i = 0; i < weaponinventoryslots.Length; i++)
            {
                weaponinventoryslots[i].closeEquipoption();
                weaponinventoryslots[i].ItemUnuseButtonUse();
            }


        }
        public void select(weaponinventorySlot weaponslot)
        {


            
           for (int i = 0;i< playerInventorymanager.weaponinRightSlots.Length;i++)
            {
                if (weaponslot.weapon.RightHand)
                {
                    if (playerInventorymanager.weaponinRightSlots[i] == unarm && !weaponslot.isuse)
                    {


                        playerInventorymanager.weaponinRightSlots[i] = weaponslot.weapon;


                        weaponslot.isuse = true;
                    }
                }
                else
                {
                    if (playerInventorymanager.weaponinLeftSlots[i] == unarm && !weaponslot.isuse)
                    {


                        playerInventorymanager.weaponinLeftSlots[i] = weaponslot.weapon;


                        weaponslot.isuse = true;
                    }
                }
            }

            

            equipManagerUI.LoadweaponOnEquipmentScreen(playerInventorymanager);

             


        }
     
        public void diselect(weaponinventorySlot weaponslot)
        {
            for (int i = 0; i < playerInventorymanager.weaponinRightSlots.Length; i++ )
            {
                Debug.Log("正在解除");
                if (weaponslot.weapon.RightHand)
                {
                    if (playerInventorymanager.weaponinRightSlots[i] != unarm && weaponslot.isuse)
                    {
                        Debug.Log("解除");
                        playerInventorymanager.weaponinRightSlots[i] = unarm;
                        weaponslot.isuse = false;
                    }
                }
                else
                {
                    if (playerInventorymanager.weaponinLeftSlots[i] != unarm && weaponslot.isuse)
                    {
                        Debug.Log("解除");
                        playerInventorymanager.weaponinLeftSlots[i] = unarm;
                        weaponslot.isuse = false;
                    }
                }
            }


            equipManagerUI.LoadweaponOnEquipmentScreen(playerInventorymanager);
        }
       





    }
}
