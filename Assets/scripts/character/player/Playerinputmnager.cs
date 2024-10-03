using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class Playerinputmnager : MonoBehaviour
    {
        
        public static  Playerinputmnager instance;
        Playercontrols playerControls;
        public PlayerManager player;

        [Header("Movement Input")]
        [SerializeField] UnityEngine.Vector2 movementInput;
        public float vertical_Input;
        public float horizontal_Input;
        public float moveamount;


        [Header("camera Input")]
        [SerializeField] UnityEngine.Vector2 cameramInput;
        public float cameravertical_Input;
        public float camerahorizontal_Input;

        [Header("Lock on input")]
        [SerializeField] bool Lockon_input = false;
        [SerializeField] bool Lockon_Right_input = false;
        [SerializeField] bool Lockon_Left_input = false;
        private Coroutine lockon_coroutine;

        

        [Header("player action input")]
        [SerializeField] bool dodge_Input = false;
        [SerializeField] bool sprint_Input = false;
        [SerializeField] bool jump_Input=false;
        [SerializeField] public bool interaction_input = false;

        [Header("player attack action input")]
        [SerializeField] bool R1_input = false;
        [SerializeField] bool R2_input = false;
        [SerializeField] bool Hold_R2_input = false;

        [SerializeField] public bool L1_input = false;
        [SerializeField] public bool L2_input = false;
       


        [Header("player attack action input")]
        [SerializeField] bool Right_Pad_input = false;
        [SerializeField] bool Left_Pad_input = false;
        [SerializeField] bool Up_Pad_input = false;

        [Header("call menu")]
        [SerializeField] bool TouchMiddle_input =false;
        [SerializeField] bool option_input = false;

        private void Awake()
        {
            if (instance == null)
            {

                instance = this;
            }
            else Destroy(gameObject);


        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;

            if (playerControls != null)
            {
                playerControls.Disable();
            }

        }

        private void OnSceneChange(Scene oldscene, Scene newScene)
        {
            if (newScene.buildIndex == worldsavegamemanger.instance.GetWorldsceneIndex())
            {
                instance.enabled = true;
                player.playerLocomotionManeger.enabled = true;

                if (playerControls != null)
                {
                    playerControls.Enable();
                }
            }
            else 
            { 
                instance.enabled = false;
                if (playerControls != null)
                {
                    playerControls.Disable();
                }
            }
        }
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new Playercontrols();

                //移動指令
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<UnityEngine.Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameramInput = i.ReadValue<UnityEngine.Vector2>();



                playerControls.Playeraction.Dogde.performed += i => dodge_Input = true;

                playerControls.Playeraction.Jump.performed += i => jump_Input = true;

                playerControls.Playeraction.interaction.performed += i => interaction_input = true;


               

                //sprint controller
                playerControls.Playeraction.sprint.performed += i => sprint_Input = true;
                playerControls.Playeraction.sprint.canceled += i => sprint_Input = false;

                //攻擊
                playerControls.Playeraction.R1.performed += i => R1_input = true;
                playerControls.Playeraction.R2.performed += i => R2_input = true;

                playerControls.Playeraction.HoldR2.performed += i => Hold_R2_input = true;
                playerControls.Playeraction.HoldR2.canceled += i => Hold_R2_input = false;
                //副手
                playerControls.Playeraction.L1.performed += i => L1_input = true;
                playerControls.Playeraction.L2.performed += i => L2_input = true;

                



                //鎖定
                playerControls.Playeraction.Lockon.performed += i => Lockon_input = true;
                playerControls.Playeraction.LockonRight.performed += i => Lockon_Right_input = true;
                playerControls.Playeraction.LockonLeft.performed += i => Lockon_Left_input = true;

                //換武器
                playerControls.Playeraction.RightSwitch.performed += i => Right_Pad_input = true;
                playerControls.Playeraction.LeftSwitch.performed += i => Left_Pad_input = true;

                //使用道具
                playerControls.Playeraction.TopUse.performed += i => Up_Pad_input = true;

                //呼叫UI



                playerControls.PlayerUi.option_call_menu.performed += i => TouchMiddle_input = true;

                playerControls.PlayerUi.option_call_systemMenu.performed += i => option_input = true;


            }  
            playerControls.Enable();
        }

       public void stopAllAction()
        {
            dodge_Input = false;
            jump_Input = false;
            interaction_input = false;
            R1_input = false;
            R2_input=false;
            L1_input = false;
            L2_input = false;
            Right_Pad_input = false; 
            Left_Pad_input=false;


        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();

                }
            }
        }

        private void Update()
        {
            HandleAllActions();
        }

        private void HandleAllActions()
        {
            

            if (!playerUIManger.instance.isopenUI)
            {
                HandleJumpInput();
                HandleMovementInput();

                HandMovementcameraInput();

                HandleDodgeInput();
                HandleSprInput();
                HandleJumpInput();

                HandleR1Attack_input();
                HandleR2Attack_input();
                HandleHoldR2ChargeInput();

                HandleL1Attack_input();
                HandleL2Attack_input();

                HandleLockonInput();
                HandleLockSwtich();

                HandleSwitchRight();
                HandleSwitchLeft();

                HandleUseItemTop();

                Handleinteraction();

                HandleCallSystemMenu();
            }

            HandleCallMenu();

           

        }
        private void HandleLockonInput()
        {
            if (player.playerNetcodeManger.isLocking.Value)
            {
                if (player.playercombatManager.currenttarget == null)
                    return;
                if (player.playercombatManager.currenttarget.IsDead.Value)
                {
                    player.playerNetcodeManger.isLocking.Value = false;
                    //當前鎖定角色死亡，會自行找下 一個目標，不會重複執行多次
                    if (lockon_coroutine != null)
                        StopCoroutine(lockon_coroutine);

                    lockon_coroutine = StartCoroutine(Playercamera.instance.WaitThenFindnewTarget());
                }     

               

                
            }
          
            if (Lockon_input && player.playerNetcodeManger.isLocking.Value)
            {
                Lockon_input = false;
                Playercamera.instance.clearlockONtarget();
                player.playerNetcodeManger.isLocking.Value = false;


                //有目標，解除原本鎖定的目標
                return;
            }
            if (Lockon_input && !player.playerNetcodeManger.isLocking.Value)
            {
                Lockon_input = false;
                //沒目標，鎖定
                Playercamera.instance.HandleLocateLockonTargets();
               if(Playercamera.instance.nearestTarget != null)
                {
                    player.playerNetcodeManger.isLocking.Value = true;
                    player.playercombatManager.setTarget(Playercamera.instance.nearestTarget);
                }
            }

        }
        private void HandleLockSwtich()
        {
            if(Lockon_Right_input)
            {
                Lockon_Right_input = false;

                if(player.playerNetcodeManger.isLocking.Value )
                {
                    Playercamera.instance.HandleLocateLockonTargets();
                    if( Playercamera.instance.Right_target != null )
                    {
                        player.playercombatManager.setTarget(Playercamera.instance.Right_target);
                    }
                    
                }
            }
            if(Lockon_Left_input)
            {
                Lockon_Left_input= false;
                if (player.playerNetcodeManger.isLocking.Value)
                {
                    Playercamera.instance.HandleLocateLockonTargets();
                    if (Playercamera.instance.Left_target != null)
                    {
                        player.playercombatManager.setTarget(Playercamera.instance.Left_target);
                    }

                }
            }

        }
        private void HandleMovementInput()
        {
            vertical_Input = movementInput.y;
            horizontal_Input = movementInput.x;
            moveamount = Mathf.Clamp01(Mathf.Abs(vertical_Input) + Mathf.Abs(horizontal_Input));

            if (moveamount <= 0.5f && moveamount > 0)
            {
                moveamount = 0.5f;
            }
            else if (moveamount > 0.5 && moveamount <= 1)
            {
                moveamount = 1;
            }

            if (player == null)
                return;

            if (moveamount != 0)
            {
                player.playerNetcodeManger.ismoving.Value = true;
            }
            else
            {
                player.playerNetcodeManger.ismoving.Value = false;
            }


                if (!player.playerNetcodeManger.isLocking.Value || player.playerNetcodeManger.isSprint.Value)
                {
                    player.playerAnimatorManager.UpdateAnimatorMavementParameter(0, moveamount, player.playerNetcodeManger.isSprint.Value);
                }
                else
                {
                    player.playerAnimatorManager.UpdateAnimatorMavementParameter(horizontal_Input, vertical_Input, player.playerNetcodeManger.isSprint.Value);
                }
            
           
           
        }

        private void HandMovementcameraInput()
        {
            cameravertical_Input = cameramInput.y;
            camerahorizontal_Input = cameramInput.x;

        }

        private void HandleDodgeInput()
        {
            if (dodge_Input)
            {
                dodge_Input = false;
                if (!player.canMove)
                    return;
                player.playerLocomotionManeger.AttemptToPerformDodge();
            }
        }
        private void HandleJumpInput()
        {
            
            if(jump_Input)
            {
                jump_Input = false;
                player.playerLocomotionManeger.AttemptToPerformJump();
            }
            

        }
        private void HandleSprInput()
        {
            if(sprint_Input)
            {
                if (!player.isPerformingAction)

                {
                    player.playerLocomotionManeger.HandleSprint();
                }
            }
            else
            {
                player.playerNetcodeManger.isSprint.Value=false;
            }

        }

        private void HandleR1Attack_input()
        {
            if(R1_input)
            {
                R1_input= false;

                //使用右手武器時，不能使用左手武器。
                player.playerNetcodeManger.SetcharacterActionHand(true);
                player.playercombatManager.currentweaponBeingused = player.playerInventoryManager.currentrightweapon;
                player.playercombatManager.preformWeaponBasedAction(player.playerInventoryManager.currentrightweapon.R1_action, player.playerInventoryManager.currentrightweapon);
            }
        }

        private void HandleR2Attack_input()
        {
            if (R2_input)
            {
                R2_input = false;

              
                //使用右手武器時，不能使用左手武器。
                player.playerNetcodeManger.SetcharacterActionHand(true);
                player.playercombatManager.currentweaponBeingused = player.playerInventoryManager.currentrightweapon;
                
                player.playercombatManager.preformWeaponBasedAction(player.playerInventoryManager.currentrightweapon.R2_action, player.playerInventoryManager.currentrightweapon);
            }

           
        }
        private void HandleHoldR2ChargeInput()
        {
            if (player.isPerformingAction)
            {
              if(player.playerNetcodeManger.isusingRightHand.Value)
                {
                    player.playercombatManager.currentweaponBeingused = player.playerInventoryManager.currentrightweapon;
                    player.playerNetcodeManger.isChargeAttacking.Value = Hold_R2_input;
                }               
            }
           


        }

        private void HandleL1Attack_input()
        {
            if (L1_input)
            {
                L1_input = false;

                
                //使用左手武器時，不能使用右手武器。
                player.playerNetcodeManger.SetcharacterActionHand(false);
                player.playercombatManager.currentweaponBeingused = player.playerInventoryManager.currentleftweapon;
               
                player.playercombatManager.preformWeaponBasedAction(player.playerInventoryManager.currentleftweapon.L1_action, player.playerInventoryManager.currentleftweapon);
            }
        }

        private void HandleL2Attack_input()
        {
            if (L2_input)
            {
                L2_input = false;

               

                //使用左手武器時，不能使用右手武器。
                player.playerNetcodeManger.SetcharacterActionHand(false);
                player.playercombatManager.currentweaponBeingused = player.playerInventoryManager.currentleftweapon;
                player.playercombatManager.preformWeaponBasedAction(player.playerInventoryManager.currentleftweapon.L2_action, player.playerInventoryManager.currentleftweapon);
            }


        }

        private void HandleSwitchRight()
        {
            if(Right_Pad_input)
            {
                Right_Pad_input = false;

                if (!player.IsOwner)
                    return;
                if (player.isPerformingAction)
                    return;
                if (!player.isGround)
                    return;
                player.playerAnimatorManager.PlayTargetactionAnimation("Swag_Right_weapon", true, false, true, true);
                
            }
        }
        private void HandleSwitchLeft()
        {

            if (Left_Pad_input)
            {
                Left_Pad_input = false;
                if (!player.IsOwner)
                    return;
                if (player.isPerformingAction)
                    return;
                if (!player.isGround)
                    return;
                player.playerAnimatorManager.PlayTargetactionAnimation("Swag_Left_weapon", true, false, true, true);

                
            }
        }

        private void Handleinteraction()
        {
            if(interaction_input)
            {

             
                interaction_input = false;
            }
        }

        private void HandleCallMenu()
        {
            
            if(TouchMiddle_input)
            {
                TouchMiddle_input = false;

              


                if (!player.playerUIManger.selectWindos.activeSelf  )
                {
                   
                    playerUIManger.instance.isopenUI=true;
                    playerUIManger.instance.UpdateUI();
                    playerUIManger.instance.equipManagerUI.LoadweaponOnEquipmentScreen(player.playerInventoryManager);
                    Debug.Log(player.playerInventoryManager);
                    player.playerAnimatorManager.PlayTargetactionAnimation("Kneeling Down", true);
                    player.animator.SetBool("stand", false);
                }
               
                else
                {
                    
                    

                    player.animator.SetBool("stand", true);
                }
                                         
            }
            
           

           

        }

        private void HandleUseItemTop()
        {
            if (Up_Pad_input)
            {
                Up_Pad_input = false;

                if (!player.IsOwner)
                    return;
                if (player.isPerformingAction)
                    return;
                if (player.playerInventoryManager.currentconsumable.currentItemAmount <= 0)
                    return;
                if (!player.isGround)
                    return;
                
                player.playerAnimatorManager.PlayTargetactionAnimation("Drink", player.playerInventoryManager.currentconsumable.isinteracting,false,true,true);



            }
        }

        private void  HandleCallSystemMenu()
        {
            if(option_input)
            {
                option_input = false;

                if (!player.playerUIManger.SystemMenu.activeSelf)
                {

                   

                   

                    Time.timeScale = 0;

                    

                    player.playerUIManger.ReTurnGameButton.Select();

                    player.playerUIManger.SystemMenu.SetActive(true);
                }
                else
                {
                    stopAllAction();

                    Time.timeScale = 1;

                    player.playerUIManger.SystemMenu.SetActive(false);
                }

            }

        }












    }
}
