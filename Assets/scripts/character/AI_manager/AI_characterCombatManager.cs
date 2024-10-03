using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace sg
{
    public class AI_characterCombatManager : charactercombatmanager
    {
        protected AI_characterManager aI_Character;

        [Header("Action Set Value")]
        public float ActionRecoveryTimer = 0;
        public float AttackRotationSpeed = 20;

        [Header("Pivot")]
        [SerializeField]public bool enablePivot = true;

        [Header("Target information")]
        public float TargerDistance;
        public float viewableAngle;
        public Vector3 TargetDirection;
      


        [Header("AI Target")]
        [SerializeField] public float Sight_Rauis = 15;
        [SerializeField] public float minViewangle = -35;
        [SerializeField] public float maxViewangle = 35;

        [Header("Grap check")]
        public bool isGrip = false;
      

        protected  override void Awake()
        {
                base.Awake();
            lockonTransform= GetComponentInChildren<LockOntransfrom>().transform;
            aI_Character=GetComponent<AI_characterManager>();
        }
        public void FindTargetViaLineofSight(AI_characterManager aicharacter)
        {
            if (currenttarget != null)
                return;

            Collider[] colliders=Physics.OverlapSphere(aicharacter.transform.position,Sight_Rauis,WorldUtillityManager.Instance.GetCharacterLayerMask());

           for(int i = 0; i < colliders.Length; i++)
            {
                charactermanger TargetCharacter = colliders[i].transform.GetComponent<charactermanger>();

                if (TargetCharacter == null)
                    continue;
                if (TargetCharacter == aicharacter)
                    continue;
                if (TargetCharacter.IsDead.Value)
                    continue;

                if(WorldUtillityManager.Instance.is_canAttackTeamGrounp(aicharacter.charactergroup,TargetCharacter.charactergroup))
                {
                    if (be_attacked)
                    {


                        TargetDirection = TargetCharacter.transform.position - transform.position;
                        viewableAngle = WorldUtillityManager.Instance.calculateviewAngle(transform, TargetDirection);
                        aicharacter.charactercombatmanager.setTarget(TargetCharacter);
                        pivotTowardsTarget(aicharacter);

                    }
                    else
                    {
                        Vector3 targetDirection = TargetCharacter.transform.position - aicharacter.transform.position;

                        float TargetAngle = Vector3.Angle(targetDirection, aicharacter.transform.forward);
                        if (TargetAngle < maxViewangle && TargetAngle > minViewangle)
                        {
                            if (Physics.Linecast(aicharacter.charactercombatmanager.lockonTransform.position, TargetCharacter.charactercombatmanager.lockonTransform.position, WorldUtillityManager.Instance.GetEnviroLayerMask()))
                            {

                                Debug.DrawLine(aicharacter.charactercombatmanager.lockonTransform.position, TargetCharacter.charactercombatmanager.lockonTransform.position);


                            }


                            else
                            {
                                TargetDirection = TargetCharacter.transform.position - transform.position;
                                viewableAngle = WorldUtillityManager.Instance.calculateviewAngle(transform, TargetDirection);
                                aicharacter.charactercombatmanager.setTarget(TargetCharacter);

                                if(enablePivot)
                                pivotTowardsTarget(aicharacter);

                            }
                        }
                    }
                }
            }

           
            
        }


        
        //動畫轉動，朝向玩家
        public virtual void pivotTowardsTarget(AI_characterManager ai_character)
        {
            if (ai_character.isPerformingAction)
                return;

            if(viewableAngle>=20 &&viewableAngle<=60)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("right_45", true);
            }
            else if (viewableAngle <= -20 && viewableAngle >= -60)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("left_45", true);
            }
            else if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("right_90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("left_90", true);
            }
            if (viewableAngle >= 110 && viewableAngle <= 145)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("right_135", true);

            }
            else if (viewableAngle <= -110 && viewableAngle >= -145)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("left_135", true);
            }
            else if (viewableAngle >= 146 && viewableAngle <= 180)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("right_180", true);
            }
            else if (viewableAngle <= -146 && viewableAngle >= -180)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("left_180", true);
            }




        }
        
        public void RotateTowardsAgent(AI_characterManager aicharacter)
        {
            if(aicharacter.characternetcodeManager.ismoving.Value)
            {
                aicharacter.aI_LocomationManager.Ai_RotateTowardsAgent(aicharacter);
            }
        }

        //追蹤攻擊(攻擊邊旋轉)
        public void RotateTowardsTargetsWhilstAttacking(AI_characterManager aicharacter)
        {
            if (currenttarget == null)
                return;
            if (!aicharacter.canRotation)
                return;

            //沒動作時，不用追蹤
            if (!aicharacter.isPerformingAction)
                return;

            Vector3 Targetdirection =currenttarget.transform.position-aicharacter.transform.position;
            Targetdirection.y = 0;
            Targetdirection.Normalize();

            //假設AI和人重疊，AI面向前
            if(Targetdirection==Vector3.zero)
                Targetdirection = aicharacter.transform.forward;

          Quaternion Rotation =Quaternion.LookRotation(Targetdirection);

            aicharacter.transform.rotation = Quaternion.Slerp(aicharacter.transform.rotation, Rotation, AttackRotationSpeed*Time.deltaTime);


        }


        public void HandleActionRecovery(AI_characterManager aI_Character)
        {
            if(ActionRecoveryTimer > 0)
            {
                if(!aI_Character.isPerformingAction)
                {
                    ActionRecoveryTimer -= Time.deltaTime;

                }
            }
        }
    }
}
