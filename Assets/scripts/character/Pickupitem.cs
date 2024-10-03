using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace sg
{
    public class Pickupitem : interactable
    {
        [SerializeField]public weaponitem weapon;
        

        private void Awake()
        {
            foreach(var weaponitem in playerUIManger.instance.playerInventorymanager.weaponsInventory)
            {
                if (weapon== weaponitem)
                {
                    Destroy(gameObject);
                }
            }
          
        }

        public override void Caninteractable(PlayerManager player)
        {
            base.Caninteractable(player);
            pickupuitem(player);
        }

        
        public void pickupuitem(PlayerManager player)
        {

           

            player.playerAnimatorManager.PlayTargetactionAnimation("PickUp", false);

        }
       
    }
}
