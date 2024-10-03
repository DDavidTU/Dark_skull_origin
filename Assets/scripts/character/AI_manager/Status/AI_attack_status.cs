using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;


namespace sg
{
    [CreateAssetMenu(menuName = "A.I/Status/attack_status")]
    public class AI_attack_status : AI_status
    {
        [Header("currentAttack")]
        [HideInInspector]public AI_attackAction currentAttack;
        [HideInInspector]public bool willPerformCombo=true;
        

        [Header("State Flags")]
        protected bool hasperformedAttack = false;
        protected bool hasperformedCombo=false;

        [Header("pivot After Attack")]//AI攻擊後，轉向玩家的flag
        [SerializeField]protected bool pivotAfterAttack=false;

        [Header("follow attack")]
        public bool followattack = false;

        public override AI_status Tick(AI_characterManager aicharacter)
        {
            if (aicharacter.charactercombatmanager.currenttarget == null)
                return SwitchState(aicharacter,aicharacter.idle) ;

            if(aicharacter.charactercombatmanager.currenttarget.IsDead.Value)
                return SwitchState(aicharacter, aicharacter.idle);

            aicharacter.aI_CharacterCombatManager.RotateTowardsTargetsWhilstAttacking(aicharacter);

            //test
            if (followattack)
            {
                aicharacter.aI_LocomationManager.Ai_RotateTowardsAgent(aicharacter);

                aicharacter.characterAnimatorforManager.UpdateAnimatorMavementParameter(0, 1, false);

                NavMeshPath Path = new NavMeshPath();

                aicharacter.navMeshAgent.stoppingDistance = 1;

                aicharacter.navMeshAgent.CalculatePath(aicharacter.aI_CharacterCombatManager.currenttarget.transform.position, Path);
                aicharacter.navMeshAgent.SetPath(Path);
                //test
            }
            else
            {

                aicharacter.characterAnimatorforManager.UpdateAnimatorMavementParameter(0, 0, false);
            }

            //執行combo
            if (!hasperformedCombo)
            {
                
                if (currentAttack.comboaction != null)
                {

                    hasperformedCombo = true;
                    
                    currentAttack.comboaction.AttemptToperformAction(aicharacter);
                   
                }
            }

            if (aicharacter.isPerformingAction)
                return this;

            //攻擊冷卻時間
            if (!hasperformedAttack)
            {
                if (aicharacter.aI_CharacterCombatManager.ActionRecoveryTimer > 0)
                    return this;

                
                preformAttack(aicharacter);

                

                return this;//返回程式頂部，如有combo組合，即可接續下去
            }
           
            if(pivotAfterAttack)          
                aicharacter.aI_CharacterCombatManager.pivotTowardsTarget(aicharacter) ;


            return SwitchState(aicharacter, aicharacter.combatstance);

        }
        private void preformAttack(AI_characterManager aI_Character)
        {
            hasperformedAttack = true;
            currentAttack.AttemptToperformAction(aI_Character);
            aI_Character.aI_CharacterCombatManager.ActionRecoveryTimer = currentAttack.actionRecoveryTime;
        }

        protected override void ResetstatusFlags(AI_characterManager aicharacter)
        {
            base.ResetstatusFlags(aicharacter);
            hasperformedAttack = false;
            hasperformedCombo = false;
        }
    }
}
