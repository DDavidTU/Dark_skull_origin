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
            //假設AI在動作，不改變
            if (aicharacter.isPerformingAction)
                return this;
            if(aicharacter.aI_CharacterCombatManager.currenttarget==null)           
                return SwitchState(aicharacter, aicharacter.idle);           

            if(!aicharacter.navMeshAgent.enabled)
                aicharacter.navMeshAgent.enabled = true;

            //目標超出視野外，角色身體轉過去動畫
            if (aicharacter.aI_CharacterCombatManager.enablePivot)
            {
                if (aicharacter.aI_CharacterCombatManager.maxViewangle < aicharacter.aI_CharacterCombatManager.viewableAngle || aicharacter.aI_CharacterCombatManager.minViewangle > aicharacter.aI_CharacterCombatManager.viewableAngle)
                    aicharacter.aI_CharacterCombatManager.pivotTowardsTarget(aicharacter);
            }


            

            //接近目標小於，指定距離，轉換到戰鬥姿態            
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
