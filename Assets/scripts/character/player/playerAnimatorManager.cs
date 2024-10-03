using Cinemachine;
using sg;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PlayerLoop;

namespace sg
{
    public class playerAnimatorManager : CharacterAnimatorforManager
    {
        PlayerManager player;
       
       
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();

           

        }
        protected override void Update()
        {
           
        }
        
            
        

        private void OnAnimatorMove()
        {
            if (player.applyRootMotion)
            {
                Vector3 velocity = player.animator.deltaPosition;
                player.Characactercontroller.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }
        public override void EnablecanCombo()
        {
            if (player.playerNetcodeManger.isusingRightHand.Value)
            {
                player.playercombatManager.canCombowithMainHandWeapon = true;
            }
            else
            {
                player.playercombatManager.canCombowithoffHandWeapon= true;
            }
        }
        public override void DisablecanCombo()
        {

            player.playercombatManager.canCombowithMainHandWeapon  = false;
            player.playercombatManager.canCombowithoffHandWeapon= false;

        }
        public void pickupActionpoint()
        {
            
           

          
            PlayeUIpopupManager playeUIpopupManager = FindObjectOfType<PlayeUIpopupManager>();
            playerUIManger.instance.UpdateUI();


            player.playerInventoryManager.weaponsInventory.Add(player.playerInventoryManager.pickupitem.weapon);

            playeUIpopupManager.ItemInformation_text.text = player.playerInventoryManager.pickupitem.weapon.itemsname;
            playeUIpopupManager.Itemicon.texture = player.playerInventoryManager.pickupitem.weapon.itemsicon.texture;
            playeUIpopupManager.ItemInformation_gameObject.SetActive(true);

            Destroy(player.playerInventoryManager.pickupitem.gameObject);

        }

        public void sittingcheckUI()
        {

            player.isPerformingAction = true;
            player.playerUIManger.selectWindos.SetActive(true);

            UIWindows.instance.itemButton.Select();
        }
        public void standingcloseUI()
        {
            Playerinputmnager.instance.stopAllAction();
            playerUIManger.instance.isopenUI = false;
            player.playerUIManger.CloseAllUI();
        }
        public void Lightopen()
        {

            Lampcontrol.instance.Lightcontrol();

        }
        public void moveLampeposition()
        {
            Lampcontrol.instance.moveup();
        }
        public void UnmoveLampeposition()
        {
            Lampcontrol.instance.movedown();
        }
        public void openRollInvulnerable()
        {
            player.playerNetcodeManger.isInvulnerable.Value = true;
        }
        public void closeRollInvulnerable()
        {
            player.playerNetcodeManger.isInvulnerable.Value = false;
        }

        
        public void Drink()
        {
            
            player.playerInventoryManager.currentconsumable.currentItemAmount -= 1;
           
            player.playerInventoryManager.currentconsumable.AttemptoconsumeItem( player.consumable_itemslot, player.playerEffectManager);
        }
        public void SavePray()
        {
            worldsavegamemanger.instance.saveGame = true;
            player.playerInventoryManager.currentconsumable.currentItemAmount = player.playerInventoryManager.currentconsumable.maxItemAmount;
        }
    }
}
