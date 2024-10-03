using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    [CreateAssetMenu(menuName = "A.I/Actions/Attack")]
    public class AI_attackAction : ScriptableObject
    {
        [Header("AttackAnimation")]
        [SerializeField] private string AttackAnimations;

        [Header("Combo Axtion")]
        public AI_attackAction comboaction;

        [Header("Actions Value")]
        [SerializeField] Attacktype attacktype;
        public int attackWeight = 50;
        
        public float actionRecoveryTime = 1.5f;//攻擊對手後，冷卻的時間

        public float minmumAttackAngle = -35;
        public float maxmumAttackAngle = 35;
        public float minAttackDistance = 0;
        public float maxAttackDistance = 2;
        public void AttemptToperformAction(AI_characterManager aI_Character)
        {

            aI_Character.aI_CharacterAnimatorForManager.PlayTargetAttackactionAnimation(attacktype,AttackAnimations,true);
            
        }
    }
}
