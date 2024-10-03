using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class EquipManagerUI : MonoBehaviour
    {
        public bool rightHandSolts1Selected;
        public bool rightHandSolts2Selected;
        public bool rightHandSolts3Selected;
        public bool leftHandSolts1Selected;
        public bool leftHandSolts2Selected;
        public bool leftHandSolts3Selected;

        


        private  void Awake()
        {

           
        }
        
       

        public void LoadweaponOnEquipmentScreen(playerInventoryManager playerInventoryManager)
        {
           
            for(int i = 0; i < playerUIManger.instance.handEquipmentslot.Length; i++)
            {
               

                if (playerUIManger.instance.handEquipmentslot[i].righthandslot1)
                {
                    playerUIManger.instance.handEquipmentslot[i].AddItem(playerInventoryManager.weaponinRightSlots[0]);
                }
                else if (playerUIManger.instance.handEquipmentslot[i].righthandslot2)
                {
                    playerUIManger.instance.handEquipmentslot[i].AddItem(playerInventoryManager.weaponinRightSlots[1]);
                }
                else if (playerUIManger.instance.handEquipmentslot[i].righthandslot3)
                {
                    playerUIManger.instance.handEquipmentslot[i].AddItem(playerInventoryManager.weaponinRightSlots[2]);
                }               
                else if (playerUIManger.instance.handEquipmentslot[i].lefthandslot1)
                {
                    playerUIManger.instance.handEquipmentslot[i].AddItem(playerInventoryManager.weaponinLeftSlots[0]);
                }
                else if (playerUIManger.instance.handEquipmentslot[i].lefthandslot2)
                {
                    playerUIManger.instance.handEquipmentslot[i].AddItem(playerInventoryManager.weaponinLeftSlots[1]);
                }
                else if (playerUIManger.instance.handEquipmentslot[i].lefthandslot3)
                {
                    playerUIManger.instance.handEquipmentslot[i].AddItem(playerInventoryManager.weaponinLeftSlots[2]);
                }
            }
        }
            

        public void SelectedRightSolts1()
        {
            rightHandSolts1Selected=true;
        }
        public void SelectedRightSolts2()
        {
            rightHandSolts2Selected=true;
        }
        public void SelectedRightSolts3()
        {
            rightHandSolts3Selected=true;
        }
        public void SelectedLeftSolts1()
        {
            leftHandSolts1Selected=true;
        }
        public void SelectedLeftSolts2()
        {
            leftHandSolts2Selected = true;
        }
        public void SelectedLeftSolts3()
        {
            leftHandSolts3Selected = true;
        }

       
    }
}
