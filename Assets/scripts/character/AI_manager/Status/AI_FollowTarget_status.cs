using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace sg
{
    [CreateAssetMenu(menuName = "A.I/Status/FollowTarget_status")]
    public class AI_FollowTarget_status : AI_status
    {
        public override AI_status Tick(AI_characterManager aicharacter)
        {
            //���]AI�b�ʧ@�A������
            if (aicharacter.isPerformingAction)
                return this;
            if(aicharacter.aI_CharacterCombatManager.currenttarget==null)           
                return SwitchState(aicharacter, aicharacter.idle);           

            if(!aicharacter.navMeshAgent.enabled)
                aicharacter.navMeshAgent.enabled = true;

            //�ؼжW�X�����~�A���⨭����L�h�ʵe
            if (aicharacter.aI_CharacterCombatManager.enablePivot)
            {
                if (aicharacter.aI_CharacterCombatManager.maxViewangle < aicharacter.aI_CharacterCombatManager.viewableAngle || aicharacter.aI_CharacterCombatManager.minViewangle > aicharacter.aI_CharacterCombatManager.viewableAngle)
                    aicharacter.aI_CharacterCombatManager.pivotTowardsTarget(aicharacter);
            }


            

            //����ؼФp��A���w�Z���A�ഫ��԰����A            
            if (aicharacter.aI_CharacterCombatManager.TargerDistance <= aicharacter.navMeshAgent.stoppingDistance)
               return SwitchState(aicharacter, aicharacter.combatstance);




            aicharacter.aI_LocomationManager.Ai_RotateTowardsAgent(aicharacter);

            NavMeshPath Path = new NavMeshPath();

            aicharacter.navMeshAgent.CalculatePath(aicharacter.aI_CharacterCombatManager.currenttarget.transform.position, Path);
            aicharacter.navMeshAgent.SetPath(Path);


            return this;
        }
    }
}
