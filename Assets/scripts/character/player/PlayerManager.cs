using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System.Security.Cryptography;
using Unity.VisualScripting;

namespace sg
{
    public class PlayerManager : charactermanger
    {


        [Header("itemCheck")]
        [SerializeField] public Transform itemsearchposition;
        [SerializeField] public weaponmodelinitialSlot consumable_itemslot;
        
        

        [HideInInspector] public PlayerLocomotionManeger playerLocomotionManeger;
        [HideInInspector] public playerAnimatorManager playerAnimatorManager;
        [HideInInspector] public playNetWorkManager playerNetcodeManger;
        [HideInInspector] public playerstatusmanager playerstatusmanager;
        [HideInInspector] public playerInventoryManager playerInventoryManager;
        [HideInInspector] public playerEquipmentmanager playerEquipmentmanager;
        [HideInInspector] public playercombatmanager playercombatManager;
        [HideInInspector] public PlayeUIpopupManager playeUIpopupManager;
        [HideInInspector] public playerUIManger  playerUIManger;
        [HideInInspector] public playerEffectManager playerEffectManager;

        




        protected override void Awake()
        {
            base.Awake();


            playerLocomotionManeger = GetComponent<PlayerLocomotionManeger>();
            playerAnimatorManager = GetComponent<playerAnimatorManager>();
            playerNetcodeManger = GetComponent<playNetWorkManager>();
            playerstatusmanager = GetComponent<playerstatusmanager>();
            playerInventoryManager = GetComponent<playerInventoryManager>();
            playerEquipmentmanager = GetComponent<playerEquipmentmanager>();
            playercombatManager = GetComponent<playercombatmanager>();
            playeUIpopupManager = FindObjectOfType<PlayeUIpopupManager>();
            playerUIManger= FindObjectOfType<playerUIManger>();
            playerEffectManager = GetComponent<playerEffectManager>();
        }
        protected override void Start()
        {


           


        }

        protected override void Update()
        {
            base.Update();

            if (!IsOwner)
                return;

            playerLocomotionManeger.HandleAllMovement();
            playerstatusmanager.ReGenerateStamina();

            CheckForInteractertableObject();

            playerUIManger.playeruihubmanager.TopAmount.text = playerUIManger.instance.playeruihubmanager.TopAmount.text = playerInventoryManager.currentconsumable.currentItemAmount.ToString();

           

            


        }

        protected override void LateUpdate()
        {

            if (!IsOwner) return;
            base.LateUpdate();



            Playercamera.instance.HandleAllCameraActions();



        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            NetworkManager.Singleton.OnClientConnectedCallback += onclientconnectCallback;

            if (IsOwner)
            {
                Playercamera.instance.player = this;
                Playerinputmnager.instance.player = this;
                worldsavegamemanger.instance.player = this;
                playerUIManger.instance.playerInventorymanager = playerInventoryManager;

                playerNetcodeManger.Hpva.OnValueChanged += playerNetcodeManger.SetnewMaxHpva;
                playerNetcodeManger.endurance.OnValueChanged += playerNetcodeManger.SetnewMaxStamina;

                playerNetcodeManger.currentHp.OnValueChanged += playerUIManger.instance.playeruihubmanager.SetnewHPValue;



                playerNetcodeManger.currentstamina.OnValueChanged += playerUIManger.instance.playeruihubmanager.SetnewstaminaValue;
                playerNetcodeManger.currentstamina.OnValueChanged += playerstatusmanager.ResetStaminaRegenTimer;

                playerNetcodeManger.currentHp.OnValueChanged += playerNetcodeManger.SetMaxReductCurrentHP;
                playerNetcodeManger.MaxReductCurrentHp.OnValueChanged += ControlPP.instance.SetVgValue;


            }
            //狀態
            playerNetcodeManger.currentHp.OnValueChanged += playerNetcodeManger.CheckHp;

            //鎖定
            playerNetcodeManger.isLocking.OnValueChanged += playerNetcodeManger.onislockedonchange;
            playerNetcodeManger.CurrentObjectTargetID.OnValueChanged += playerNetcodeManger.onLockChangeTargetID;
            //裝備
            playerNetcodeManger.currentRightHandWeaponID.OnValueChanged += playerNetcodeManger.OncurrentRightHandChangID;
            playerNetcodeManger.currentLeftHandWeaponID.OnValueChanged += playerNetcodeManger.OncurrentLeftHandChangID;
            playerNetcodeManger.currentweaponBeingused.OnValueChanged += playerNetcodeManger.OncurrentbeingusedIDchange;
            //標示(正在做的動作)
            playerNetcodeManger.isChargeAttacking.OnValueChanged += playerNetcodeManger.onisChargeAttackChanged;


            //如果是角色擁有者，但不是伺服器主機，客戶端將重新將遊戲角色載入進去。
            if (IsOwner && !IsServer)
            {
                LoadGamedataTFromcurrentCharacter(ref worldsavegamemanger.instance.currentdata);
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            NetworkManager.Singleton.OnClientConnectedCallback -= onclientconnectCallback;

            if (IsOwner)
            {


                playerNetcodeManger.Hpva.OnValueChanged -= playerNetcodeManger.SetnewMaxHpva;
                playerNetcodeManger.endurance.OnValueChanged -= playerNetcodeManger.SetnewMaxStamina;

                playerNetcodeManger.currentHp.OnValueChanged -= playerUIManger.instance.playeruihubmanager.SetnewHPValue;


                playerNetcodeManger.currentstamina.OnValueChanged -= playerUIManger.instance.playeruihubmanager.SetnewstaminaValue;
                playerNetcodeManger.currentstamina.OnValueChanged -= playerstatusmanager.ResetStaminaRegenTimer;

                playerNetcodeManger.currentHp.OnValueChanged -= playerNetcodeManger.SetMaxReductCurrentHP;
                playerNetcodeManger.MaxReductCurrentHp.OnValueChanged -= ControlPP.instance.SetVgValue;

            }
            //狀態
            playerNetcodeManger.currentHp.OnValueChanged -= playerNetcodeManger.CheckHp;

            //鎖定
            playerNetcodeManger.isLocking.OnValueChanged -= playerNetcodeManger.onislockedonchange;
            playerNetcodeManger.CurrentObjectTargetID.OnValueChanged -= playerNetcodeManger.onLockChangeTargetID;
            //裝備
            playerNetcodeManger.currentRightHandWeaponID.OnValueChanged -= playerNetcodeManger.OncurrentRightHandChangID;
            playerNetcodeManger.currentLeftHandWeaponID.OnValueChanged -= playerNetcodeManger.OncurrentLeftHandChangID;
            playerNetcodeManger.currentweaponBeingused.OnValueChanged -= playerNetcodeManger.OncurrentbeingusedIDchange;
            //標示(正在做的動作)
            playerNetcodeManger.isChargeAttacking.OnValueChanged -= playerNetcodeManger.onisChargeAttackChanged;


            


        }



        public override IEnumerator ProcessDeathEvent(bool manualSelectDeadAnimator = false)
        {



            if (IsOwner)
            {
                playerUIManger.instance.playeUIpopupManager.sendYouDiedPopUp();
            }


            return base.ProcessDeathEvent(manualSelectDeadAnimator);
        }

        public override void revivecharacter()
        {
            base.revivecharacter();

            if (IsOwner)
            {
                IsDead.Value = false;
                playerNetcodeManger.currentHp.Value = characternetcodeManager.MaxHp.Value;
                playerNetcodeManger.currentstamina.Value = characternetcodeManager.Maxstamina.Value;

                playerAnimatorManager.PlayTargetactionAnimation("New State", false);
            }
        }

        public void SaveGamedataTocurrentCharacter(ref CharacterSave currentsavedata)
        {
            if (playerNetcodeManger == null)
                return;
            
            currentsavedata.sceneindex = SceneManager.GetActiveScene().buildIndex;
            currentsavedata.character_name = playerNetcodeManger.characterName.Value.ToString();
            currentsavedata.position_x = transform.position.x;
            currentsavedata.position_y = transform.position.y;
            currentsavedata.position_z = transform.position.z;

            currentsavedata.currentHp = playerNetcodeManger.currentHp.Value;
            currentsavedata.currentstamina = playerNetcodeManger.currentstamina.Value;

            currentsavedata.Hp = playerNetcodeManger.Hpva.Value;
            currentsavedata.stamina = playerNetcodeManger.endurance.Value;

            currentsavedata.weaponsInventory = playerInventoryManager.weaponsInventory;

            currentsavedata.weaponinRightSlots = playerInventoryManager.weaponinRightSlots;
            currentsavedata.weaponinLefttSlots = playerInventoryManager.weaponinLeftSlots;

            currentsavedata.currentrightweapon= playerInventoryManager.currentrightweapon;
            currentsavedata.currentleftweapon = playerInventoryManager.currentrightweapon;


            currentsavedata.HPRecoveryamount = playerInventoryManager.currentconsumable.currentItemAmount;
        }
        public void LoadGamedataTFromcurrentCharacter(ref CharacterSave currentsavedata)
        {
            playerNetcodeManger.characterName.Value = currentsavedata.character_name;
            Vector3 myposition = new Vector3(currentsavedata.position_x, currentsavedata.position_y, currentsavedata.position_z);
            transform.position = myposition;

            playerNetcodeManger.Hpva.Value = currentsavedata.Hp;
            playerNetcodeManger.endurance.Value = currentsavedata.stamina;


            playerNetcodeManger.MaxHp.Value = playerstatusmanager.caculateHpBaseofHpValevel(playerNetcodeManger.Hpva.Value);
            playerNetcodeManger.Maxstamina.Value = playerstatusmanager.caculatestaminaBaseonEndurancelevel(playerNetcodeManger.endurance.Value);
            playerNetcodeManger.currentHp.Value = currentsavedata.currentHp;
            playerNetcodeManger.currentstamina.Value = currentsavedata.currentstamina;

            playerUIManger.instance.playeruihubmanager.SetmaxstaminaValue(playerNetcodeManger.Maxstamina.Value);

            playerInventoryManager.weaponsInventory = currentsavedata.weaponsInventory;

            playerInventoryManager.weaponinRightSlots = currentsavedata.weaponinRightSlots;
            playerInventoryManager.weaponinLeftSlots = currentsavedata.weaponinLefttSlots;

            playerInventoryManager.currentrightweapon=currentsavedata.currentrightweapon;
            playerInventoryManager.currentleftweapon = currentsavedata.currentleftweapon;

            playerInventoryManager.currentconsumable.currentItemAmount = currentsavedata.HPRecoveryamount;
        }
        private void onclientconnectCallback(ulong ClientID)
        {
            WorldGameSessionManager.instance.AddLayertoActivepayerList(this);

            //如果本身是開房者，不需要去跟人家同步資訊
            if (!IsServer && IsOwner)
            {
                foreach (var player in WorldGameSessionManager.instance.players)
                {
                    if (player != this)
                    {
                        LoadotherPlayerCharacterWhenJointingServer();
                    }
                }
            }
        }
        public void LoadotherPlayerCharacterWhenJointingServer()
        {
            playerNetcodeManger.OncurrentRightHandChangID(0, playerNetcodeManger.currentRightHandWeaponID.Value);
            playerNetcodeManger.OncurrentLeftHandChangID(0, playerNetcodeManger.currentLeftHandWeaponID.Value);


            if (playerNetcodeManger.isLocking.Value)
            {
                playerNetcodeManger.onLockChangeTargetID(0, playerNetcodeManger.CurrentObjectTargetID.Value);
            }

        }


        //撿取物品
        public void CheckForInteractertableObject()
        {
            RaycastHit Hit;

            if (Physics.SphereCast(itemsearchposition.position, 0.5f, itemsearchposition.forward, out Hit, 0.2f, WorldUtillityManager.Instance.GetitemLayerMask()))
            {


                interactable cainteractable = Hit.collider.GetComponent<interactable>();

                if (cainteractable != null)
                {
                    //彈出互動視窗，提醒玩家已拾取
                    string interactableText = cainteractable.interctableTexts;

                    playeUIpopupManager.interactionInformation_text.text = interactableText;
                    playeUIpopupManager.interactionInformation_gameObject.SetActive(true);

                    
                {


                        if (Playerinputmnager.instance.interaction_input)
                        {
                            if (Hit.collider.tag == "item")
                                Hit.collider.GetComponent<Pickupitem>().Caninteractable(this);

                            else if (Hit.collider.tag == "save")
                                Hit.collider.GetComponent<SavePoint>().Caninteractable(this);

                            else if (Hit.collider.tag == "Door")
                                Hit.collider.GetComponent<Player_Opendoor>().Caninteractable(this);

                            else if (Hit.collider.tag == "BigDoor")
                                Hit.collider.GetComponent<player_opendoorBigDoor>().Caninteractable(this);

                        }
                    }
                }

              

                  
                




            }
            else
            {

                if (playeUIpopupManager.interactionInformation_gameObject != null)
                {
                    playeUIpopupManager.interactionInformation_gameObject.SetActive(false);
                }

               
                if (playeUIpopupManager.ItemInformation_gameObject != null && Playerinputmnager.instance.interaction_input)
                {
                    playeUIpopupManager.ItemInformation_gameObject.SetActive(false);
                }
            }
        }

        
    }
}

