using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class PlayerLocomotionManeger : characterlocomotionmanger
    {

        PlayerManager player;

        [Header("Movement settings")]
        public float moveAmount;
        public float verticalmovement;
        public float horizontalmovement;
        [SerializeField] float wakingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float sperintspeed = 10;
        [SerializeField] float rotationtime = 15;     
        [SerializeField] float sprintStaminaCost=2;

        

        [Header("Dodge")]
        private Vector3 dodgedirection;
        [SerializeField] float DodgeStaminaRollCost = 20;
        [SerializeField] float DodgeStaminaCost = 10;

        [Header("Jump")]
        [SerializeField] float JumpStaminaCost = 10;
        [SerializeField] float JumpHeight = 4;
        [SerializeField] float JumpingForwardForce = 5;
        [SerializeField] float FreeFallspeed = 2;
        private Vector3 JumpDirection;


        private Vector3 movedirection;
        private Vector3 targetRotationDirection;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
            this.enabled = false;
        }

        protected override void Update()
        {
            base.Update();
            if (player.IsOwner)
            {
                player.characternetcodeManager.VerticalParameter.Value = verticalmovement;
                player.characternetcodeManager.HorizontalParameter.Value = horizontalmovement;
                player.characternetcodeManager.MoveAmount.Value = moveAmount;

            }
            else
            {
                verticalmovement = player.characternetcodeManager.VerticalParameter.Value;
                horizontalmovement = player.characternetcodeManager.HorizontalParameter.Value;
                moveAmount = player.characternetcodeManager.MoveAmount.Value;

                if (!player.playerNetcodeManger.isLocking.Value || player.playerNetcodeManger.isSprint.Value)
                {
                    player.playerAnimatorManager.UpdateAnimatorMavementParameter(0, moveAmount, player.playerNetcodeManger.isSprint.Value);
                }
                else
                {
                    player.playerAnimatorManager.UpdateAnimatorMavementParameter(horizontalmovement, verticalmovement, player.playerNetcodeManger.isSprint.Value);
                }


            }
        }
        public void HandleAllMovement()
        {
            if (player.IsDead.Value)
                return;
                
            
            HandGroundMovement();
            HandleRotation();         
            HandleJumpMove();
            HandleFreeFall();
        }

        private void getMovementValues()
        {
            verticalmovement = Playerinputmnager.instance.vertical_Input;
            horizontalmovement = Playerinputmnager.instance.horizontal_Input;
            moveAmount = Playerinputmnager.instance.moveamount;
        }

        public void HandleSprint()
        {

            
            if (player.isPerformingAction)
            {
                player.playerNetcodeManger.isSprint.Value = false;
            }
            if(player.playerNetcodeManger.currentstamina.Value<=0)
            {
                player.playerNetcodeManger.isSprint.Value = false;
                return;
            }
            if (moveAmount >= 0.5)
            {
                player.playerNetcodeManger.isSprint.Value = true;
            }
            else
            {
                player.playerNetcodeManger.isSprint.Value = false;
            }
            if(player.playerNetcodeManger.isSprint.Value)
            {
                player.playerNetcodeManger.currentstamina.Value -= sprintStaminaCost * Time.deltaTime;
            }
        } 
        private void HandGroundMovement()
        {
            if (!player.canMove)
                return;

            getMovementValues();
            movedirection = Playercamera.instance.transform.forward * verticalmovement;
            movedirection = movedirection + Playercamera.instance.transform.right * horizontalmovement;
            movedirection.Normalize();
            movedirection.y = 0;

            if(player.playerNetcodeManger.isSprint.Value)
            {
                player.Characactercontroller.Move(movedirection*sperintspeed*Time.deltaTime);
            }
            else
            {
                if (Playerinputmnager.instance.moveamount > 0.5f)
                {
                    player.Characactercontroller.Move(movedirection * runningSpeed * Time.deltaTime);
                }
                else if (Playerinputmnager.instance.moveamount <= 0.5f)
                {
                    player.Characactercontroller.Move(movedirection * wakingSpeed * Time.deltaTime);
                }
            }

            
        }

        private void HandleRotation()
        {

            if (player.IsDead.Value)
                return;
                
            
            if (!player.canRotation)
                return;

            if(player.playerNetcodeManger.isLocking.Value)
            {
                //朝鎖定者跑
                if(player.playerNetcodeManger.isSprint.Value || player.playerLocomotionManeger.isRolling)
                {
                    Vector3 targetDirection = Vector3.zero;
                    targetDirection=Playercamera.instance.cameraObjection.transform.forward *verticalmovement;
                    targetDirection += Playercamera.instance.cameraObjection.transform.right * horizontalmovement;
                    targetDirection.Normalize();
                    targetDirection.y = 0;

                    if(targetDirection==Vector3.zero)
                    {
                        targetDirection = transform.forward;

                    }
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationtime * Time.deltaTime);
                }
                else//沒跑步時，也要面朝鎖定者
                {
                    if (player.playercombatManager.currenttarget == null)
                        return;
                    Vector3 targetDirection;
                    targetDirection = player.playercombatManager.currenttarget.transform.position - transform.position;
                    targetDirection.y = 0;
                    targetDirection.Normalize();
                    Quaternion Targetrotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation,Targetrotation,rotationtime*Time.deltaTime);
                    


                    
                }
            }
            else
            {
                targetRotationDirection = Vector3.zero;
                targetRotationDirection = Playercamera.instance.transform.forward * verticalmovement;
                targetRotationDirection = targetRotationDirection + Playercamera.instance.transform.right * horizontalmovement;
                targetRotationDirection.Normalize();
                targetRotationDirection.y = 0;


                if (targetRotationDirection == Vector3.zero)
                {
                    targetRotationDirection = transform.forward;
                }


                Quaternion newrotation = Quaternion.LookRotation(targetRotationDirection);




                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newrotation, rotationtime * Time.deltaTime);
                transform.rotation = targetRotation;
            }

            
        }

        private void HandleJumpMove()
        {
            if (player.playerNetcodeManger.isjump.Value)
            {
                player.Characactercontroller.Move(JumpDirection * JumpingForwardForce * Time.deltaTime);
            }
       
        }

        private void HandleFreeFall()
        {
            if (!player.isGround)
            {
                Vector3 FreeFallVecloty;
                FreeFallVecloty = (Playercamera.instance.transform.forward * verticalmovement) + (Playercamera.instance.transform.right * horizontalmovement);
                FreeFallVecloty.y = 0;

                player.Characactercontroller.Move(FreeFallVecloty* FreeFallspeed * Time.deltaTime);
            }

        }

        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction)
                return;
            if (player.playerNetcodeManger.currentstamina.Value <= 0)
                return;
            


            if (Playerinputmnager.instance.moveamount > 0)
            {
               
                dodgedirection = Playercamera.instance.transform.forward * Playerinputmnager.instance.vertical_Input * Time.deltaTime;
                dodgedirection += Playercamera.instance.transform.right * Playerinputmnager.instance.horizontal_Input * Time.deltaTime;
                dodgedirection.y = 0;
                dodgedirection.Normalize();
                player.transform.rotation = Quaternion.LookRotation(dodgedirection);


                player.playerAnimatorManager.PlayTargetactionAnimation("Roll_Forward_01", true, true);
                player.playerLocomotionManeger.isRolling = true;
                player.playerNetcodeManger.currentstamina.Value -= DodgeStaminaRollCost ;

            }
            else
            {
             
                player.playerAnimatorManager.PlayTargetactionAnimation("Dodge_step_01", true, true);
               
                player.playerNetcodeManger.currentstamina.Value -= DodgeStaminaCost;
            }

        }

        public void AttemptToPerformJump()
        {
            if (player.isPerformingAction)
                return;
            if (player.playerNetcodeManger.currentstamina.Value <= 0)
                return;
            if (player.playerNetcodeManger.isjump.Value)
                return;
            if (!player.isGround)
                return;
            

            player.playerAnimatorManager.PlayTargetactionAnimation("Jump", false);

            player.playerNetcodeManger.isjump.Value = true;

            player.playerNetcodeManger.currentstamina.Value -= JumpStaminaCost;

            JumpDirection = (Playercamera.instance.cameraObjection.transform.forward * verticalmovement) +( Playercamera.instance.cameraObjection.transform.right * horizontalmovement);
            JumpDirection.y = 0;
            if (JumpDirection!=Vector3.zero)
            {
                if (player.characternetcodeManager.isSprint.Value)
                {
                    JumpDirection *= 1;
                }
                else if (Playerinputmnager.instance.moveamount > 0.5)
                {
                    JumpDirection *= 0.5f;
                }
                else if (Playerinputmnager.instance.moveamount <= 0.5)
                {
                    JumpDirection *= 0.25f;
                }
            }
            
            
        }

        public void JumpingVelocity()
        {
            Yvelocity.y = Mathf.Sqrt(-2* JumpHeight * gravityForce);
        }
    }
}
