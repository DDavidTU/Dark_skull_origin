    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace sg
{
    public class CharacterAnimatorforManager : MonoBehaviour
    {

        charactermanger character;

        [Header("Damage Animation")]
        public string Hit_Forward_Medium_01 = "Hit_Forward_Medium_01";
        public string Hit_Back_Medium_01 = "Hit_Back_Medium_01";
        public string Hit_Left_Medium_01 = "Hit_Left_Medium_01";
        public string Hit_Right_Medium_01 = "Hit_Right_Medium_01";

        protected virtual void Awake()
        {
            character = GetComponent<charactermanger>();
        }
        
        protected virtual void Update()
        {

        }
        public virtual void EnablecanCombo()
        {
           
        }
        public virtual void DisablecanCombo()
        {

           

        }


        public void UpdateAnimatorMavementParameter(float horizontalValue ,float verticalValue,bool IsSprint )
        {
            float snappedHorizontal;
          float snappedVerticl;
            //設定走路動畫的值，只有0 0.5 -0.5 1 -1;
            if (horizontalValue > 0 && horizontalValue<=0.5f)
            {
                snappedHorizontal = 0.5f;
            }
            else if (horizontalValue < 0 && horizontalValue >= -0.5f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalValue > 0.5f && horizontalValue <= 1)
            {
                snappedHorizontal = 1;
            }
            else if (horizontalValue <-0.5f  && horizontalValue >= -1)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }

            if (verticalValue > 0 && verticalValue <= 0.5f)
            {
                snappedVerticl = 0.5f;
            }
            else if (verticalValue < 0 && verticalValue >= -0.5f)
            {
                snappedVerticl = -0.5f;
            }
            else if (verticalValue > 0.5f && verticalValue <= 1)
            {
                snappedVerticl = 1;
            }
            else if (verticalValue < -0.5f && verticalValue >= -1)
            {
                snappedVerticl = -1;
            }
            else
            {
                snappedVerticl = 0;
            }
            if(IsSprint)
            {
                snappedVerticl = 2;
            }


            character.animator.SetFloat("Horizontal", snappedHorizontal, 0.15f, Time.deltaTime);
                character.animator.SetFloat("Veritacal", snappedVerticl, 0.15f, Time.deltaTime);
            
        }

        public virtual void PlayTargetactionAnimation(string targetAnimation,bool isPerformingAction,bool applyRootMotion=true, bool canMove=false,bool canRotation=false) 
        { 
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);



            character.isPerformingAction = isPerformingAction;   
            character.canMove = canMove;
            character.canRotation = canRotation;

            character.characternetcodeManager.NotifyserveofactionanimationServerRpc(NetworkManager.Singleton.LocalClientId,targetAnimation,applyRootMotion );



        }
        public virtual void PlayTargetAttackactionAnimation(Attacktype Attacktype,
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canMove = false, 
            bool canRotation = false)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);


            character.charactercombatmanager.Currentattacktype = Attacktype;
            character.charactercombatmanager.lastAttackAnimationperformed = targetAnimation;
            character.isPerformingAction = isPerformingAction;
            character.canMove = canMove;
            character.canRotation = canRotation;

            character.characternetcodeManager.NotifyTheServeofofAttackActionServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);



        }
    }
}
