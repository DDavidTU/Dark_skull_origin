using UnityEngine;

namespace sg
{
    public class playerEquipmentmanager : characterEquipmentManager
    {

        PlayerManager player;
        public weaponmodelinitialSlot weaponRightHandSlot;
        public weaponmodelinitialSlot weaponLeftHandSlot;

        public GameObject weaponRightHandModel;
        public GameObject weaponLeftHandModel;

        [SerializeField] WeaponManager RightWeaponManager;
        [SerializeField] WeaponManager LeftWeaponManager;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
            InitializeWeaponsSlots();

        }
        protected override void Start()
        {
            base.Start();

            LoadBothHandWeapon();
        }
        private void InitializeWeaponsSlots()
        {
            weaponmodelinitialSlot[] weaponslots = GetComponentsInChildren<weaponmodelinitialSlot>();

            foreach (var weaponslot in weaponslots)
            {
                if (weaponslot.weaponsolt == weaponModelsolt.RightHand)
                {
                    weaponRightHandSlot = weaponslot;
                }
                else if (weaponslot.weaponsolt == weaponModelsolt.LeftHand)
                {
                    weaponLeftHandSlot = weaponslot;
                }
            }
        }

        public void LoadBothHandWeapon()
        {
            LoadLeftHandWeapon();
            LoadRightHandWeapon();
        }

        public void SwitchRightweapon()
        {



            weaponitem selelectedWeapon = null;

            player.playerInventoryManager.rightHandweaponindex += 1;

            if (player.playerInventoryManager.rightHandweaponindex < 0 || player.playerInventoryManager.rightHandweaponindex > 2)
            {
                player.playerInventoryManager.rightHandweaponindex = 0;


                float weaponcont = 0;
                weaponitem firstweapon = null;
                int firstweaponposition = 0;
                for (int i = 0; i < player.playerInventoryManager.weaponinRightSlots.Length; i++)
                {

                    if (player.playerInventoryManager.weaponinRightSlots[i].itemID != worldItemDatabase.Instance.unarmedWeapon.itemID)
                    {

                        weaponcont += 1;
                        if (firstweapon == null)
                        {
                            firstweapon = player.playerInventoryManager.weaponinRightSlots[i];
                            firstweaponposition = i;
                        }
                    }
                }

                if (weaponcont <= 1)
                {
                    player.playerInventoryManager.rightHandweaponindex = -1;
                    selelectedWeapon = worldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetcodeManger.currentRightHandWeaponID.Value = selelectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.rightHandweaponindex = firstweaponposition;
                    player.playerNetcodeManger.currentRightHandWeaponID.Value = firstweapon.itemID;
                }
                return;
            }



            foreach (weaponitem weapon in player.playerInventoryManager.weaponinRightSlots)
            {
                if (player.playerInventoryManager.weaponinRightSlots[player.playerInventoryManager.rightHandweaponindex].itemID != worldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selelectedWeapon = player.playerInventoryManager.weaponinRightSlots[player.playerInventoryManager.rightHandweaponindex];
                    player.playerNetcodeManger.currentRightHandWeaponID.Value = player.playerInventoryManager.weaponinRightSlots[player.playerInventoryManager.rightHandweaponindex].itemID;
                    return;
                }
            }

            if (selelectedWeapon == null && player.playerInventoryManager.rightHandweaponindex <= 2)//三樣武器，如有一樣沒裝東西，會挑過。
            {
                SwitchRightweapon();
            }



        }
        public void pullRightweapontiming()
        {
            SwitchRightweapon();
        }
        public void LoadRightHandWeapon()
        {
            if (player.playerInventoryManager.currentrightweapon != null)
              weaponRightHandSlot.unLoadWeapon();//移除武器


             
                weaponRightHandModel = Instantiate(player.playerInventoryManager.currentrightweapon.weapon);

                weaponRightHandSlot.LoadWeapon(weaponRightHandModel);

                RightWeaponManager = weaponRightHandModel.GetComponent<WeaponManager>();

                RightWeaponManager.SetweaponDamage(player, player.playerInventoryManager.currentrightweapon);
            
        }


        public void SwitchLeftweapon()
        {



            weaponitem selelectedWeapon = null;

            player.playerInventoryManager.leftHandweaponindex += 1;

            if (player.playerInventoryManager.leftHandweaponindex < 0 || player.playerInventoryManager.leftHandweaponindex > 2)
            {
                player.playerInventoryManager.leftHandweaponindex = 0;


                float weaponcont = 0;
                weaponitem firstweapon = null;
                int firstweaponposition = 0;
                for (int i = 0; i < player.playerInventoryManager.weaponinLeftSlots.Length; i++)
                {

                    if (player.playerInventoryManager.weaponinLeftSlots[i].itemID != worldItemDatabase.Instance.unarmedWeapon.itemID)
                    {

                        weaponcont += 1;
                        if (firstweapon == null)
                        {
                            firstweapon = player.playerInventoryManager.weaponinLeftSlots[i];
                            firstweaponposition = i;
                        }
                    }
                }

                if (weaponcont <= 1)
                {
                    player.playerInventoryManager.leftHandweaponindex = -1;
                    selelectedWeapon = worldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetcodeManger.currentLeftHandWeaponID.Value = selelectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.leftHandweaponindex = firstweaponposition;
                    player.playerNetcodeManger.currentLeftHandWeaponID.Value = firstweapon.itemID;
                }
                return;
            }



            foreach (weaponitem weapon in player.playerInventoryManager.weaponinLeftSlots)
            {
                if (player.playerInventoryManager.weaponinLeftSlots[player.playerInventoryManager.leftHandweaponindex].itemID != worldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selelectedWeapon = player.playerInventoryManager.weaponinLeftSlots[player.playerInventoryManager.leftHandweaponindex];
                    player.playerNetcodeManger.currentLeftHandWeaponID.Value = player.playerInventoryManager.weaponinLeftSlots[player.playerInventoryManager.leftHandweaponindex].itemID;
                    return;
                }
            }

            if (selelectedWeapon == null && player.playerInventoryManager.leftHandweaponindex <= 2)//三樣武器，如有一樣沒裝東西，會挑過。
            {
                SwitchLeftweapon();
            }


        }
        public void pullLefttweapontiming()
        {
            SwitchLeftweapon();
        }
        public void LoadLeftHandWeapon()
        {
            if (player.playerInventoryManager.currentleftweapon != null)
                weaponLeftHandSlot.unLoadWeapon();//移除武器


                         
                weaponLeftHandModel = Instantiate(player.playerInventoryManager.currentleftweapon.weapon);
                weaponLeftHandSlot.LoadWeapon(weaponLeftHandModel);
                LeftWeaponManager = weaponLeftHandModel.GetComponent<WeaponManager>();

                LeftWeaponManager.SetweaponDamage(player, player.playerInventoryManager.currentleftweapon);
            
        }

        public void openDamageCollider()
        {
            if (player.playerNetcodeManger.isusingRightHand.Value)
            {
                RightWeaponManager.meeleDamageCollider.EnabledamageCollider();
                

            }
            else if (player.playerNetcodeManger.isusingLeftHand.Value)
            {
                LeftWeaponManager.meeleDamageCollider.EnabledamageCollider();

            }
        }
        public void closedDamageCollider()
        {
            if (player.playerNetcodeManger.isusingRightHand.Value)
            {
                RightWeaponManager.meeleDamageCollider.DisabledamageCollider();
           
                player.charactersoundFXsource.playSoundFX(worldItemDatabase.Instance.ChoseRandonSound(player.playerInventoryManager.currentrightweapon.Attacksound));
            }
            else if (player.playerNetcodeManger.isusingLeftHand.Value)
            {
                LeftWeaponManager.meeleDamageCollider.DisabledamageCollider();
                
                    player.charactersoundFXsource.playSoundFX(worldItemDatabase.Instance.ChoseRandonSound(player.playerInventoryManager.currentleftweapon.Attacksound));
            }
        }
    }
}
