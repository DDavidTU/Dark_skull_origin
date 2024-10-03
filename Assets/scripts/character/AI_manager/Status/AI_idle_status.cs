using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace sg
{
    [CreateAssetMenu(menuName ="A.I/Status/idle")]
    public class AI_idle_status : AI_status
    {
        public override AI_status Tick(AI_characterManager aicharacter)
        {
            
             
            if(aicharacter.charactercombatmanager.currenttarget!=null)
            {
              return  SwitchState(aicharacter, aicharacter.followTarget);
              
            }
            else
            {
                aicharacter.aI_CharacterCombatManager.FindTargetViaLineofSight(aicharacter);

                return this;
            }

           
        }
    }
}
