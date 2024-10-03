using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class weaponinventorySlot : MonoBehaviour
    {
       [SerializeField] public Image icon;
       [SerializeField] public weaponitem weapon;
        [SerializeField] public GameObject use;
        [SerializeField] public GameObject Equip;
        [SerializeField] public Button EquipButton;
        [SerializeField] public GameObject UnEquip;
        [SerializeField] public Button UnEquipButton;
        [SerializeField] public Button Returnnutton;
        [SerializeField ] public GameObject Equipoption;
      
        [SerializeField] public Button ItemButton;

        [SerializeField] public GameObject UseView;
        
        public bool isuse; 

        private void Awake()
        {
           
                use.SetActive(false);
            isuse = false;
            ItemButton.enabled = true;
        }

        private void Update()
        {


            if (playerUIManger.instance.playerInventorymanager.weaponinRightSlots.Contains(weapon) || playerUIManger.instance.playerInventorymanager.weaponinLeftSlots.Contains(weapon))
            {
                UnEquip.SetActive(true);
                Equip.SetActive(false);
                UseView.SetActive(true);
                isuse = true;

            }
            else
            {
                Equip.SetActive(true);
                UnEquip.SetActive(false);
                UseView.SetActive(false);
                isuse = false;
            }
            
        }


        public void AddItem(weaponitem newitem)
        {
            weapon = newitem;
            icon.sprite = newitem.itemsicon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void RemoveItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void selectEquipButton()
        {
            
            

            if (weapon.RightHand)
            {
                if (!isuse && playerUIManger.instance.playerInventorymanager.weaponinRightSlots.Contains(playerUIManger.instance.unarm))
                {
                    Equip.SetActive(true);
                    UnEquip.SetActive(false);
                    EquipButton.Select();
                }
                else if (isuse)
                {
                    UnEquip.SetActive(true);
                    Equip.SetActive(false);

                    UnEquipButton.Select();
                }
                else
                {
                    UnEquip.SetActive(false);
                    Equip.SetActive(false);
                    Returnnutton.Select();
                }
            }
            else
            {
                if (!isuse && playerUIManger.instance.playerInventorymanager.weaponinLeftSlots.Contains(playerUIManger.instance.unarm))
                {
                    Equip.SetActive(true);
                    UnEquip.SetActive(false);
                    EquipButton.Select();
                }
                else if (isuse)
                {
                    UnEquip.SetActive(true);
                    Equip.SetActive(false);

                    UnEquipButton.Select();
                }
                else
                {
                    UnEquip.SetActive(false);
                    Equip.SetActive(false);
                    Returnnutton.Select();
                }
            }
        }

        public void closeEquipoption()
        {
            Equipoption.SetActive(false);
        }
        public void ItemuseButtonUse()
        {

           for(int i = 0;i< playerUIManger.instance.weaponinventoryslots.Length;i++)
            {
                playerUIManger.instance.weaponinventoryslots[i].ItemButton.enabled = false;
            }


          
            

        }
        public void ItemUnuseButtonUse()
        {
            for (int i = 0; i < playerUIManger.instance.weaponinventoryslots.Length; i++)
            {
                playerUIManger.instance.weaponinventoryslots[i].ItemButton.enabled = true;
            }
        }

        
        
    }
}
