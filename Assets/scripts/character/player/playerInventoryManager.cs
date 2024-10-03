using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class playerInventoryManager : characterIventorymanager
    {
        private PlayerManager playerManager;

        [Header("current item")]
        public consumable_item currentconsumable;
        public weaponitem currentrightweapon;
        public weaponitem currentleftweapon;

       


        [Header("quick switch")]
        public weaponitem[] weaponinRightSlots=new weaponitem[3];
        public int rightHandweaponindex = 0;
        public weaponitem[] weaponinLeftSlots=new weaponitem[3];
        public int leftHandweaponindex = 0;
        [Header("Weapon item")]
        public List<weaponitem> weaponsInventory;

        [Header("pick up item")]
        public Pickupitem pickupitem;
        private RaycastHit Hit;
        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }
        private void Start()
        {
            playerUIManger.instance.playeruihubmanager.RightWeapon_icon.sprite = playerUIManger.instance.playerInventorymanager.currentrightweapon.itemsicon;
            playerUIManger.instance.playeruihubmanager.RightWeapon_icon.enabled = true;

            playerUIManger.instance.playeruihubmanager.LeftWeapon_icon.sprite = playerUIManger.instance.playerInventorymanager.currentleftweapon.itemsicon;
            playerUIManger.instance.playeruihubmanager.LeftWeapon_icon.enabled = true;
        }
        protected override void Update()
        {
            if (Physics.SphereCast(playerManager.itemsearchposition.position, 0.5f, playerManager.itemsearchposition.forward, out Hit, 0.2f, WorldUtillityManager.Instance.GetitemLayerMask()))
            {

                if (Hit.collider.tag == "item")
                {
                    pickupitem = Hit.collider.GetComponent<Pickupitem>();

                }
            }

            
        }
        

    }
}
