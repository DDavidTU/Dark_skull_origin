using sg;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
namespace sg
{
    [CreateAssetMenu(menuName = "A.I/Status/combat_status")]

    public class AI_combat_status : AI_status
    {

        [Header("attack")]
        public List<AI_attackAction> aicharacterAttacks;//����үవ���ʧ@
        public List<AI_attackAction> potentialAttacks;
        private AI_attackAction chooseAttack;
        private AI_attackAction previousAttack;
        protected bool hasAttack = false;


        [Header("Combo")]
        [SerializeField] protected bool canpreformcombo = false;
        [SerializeField] protected int chanceToperformcombo = 25;//����combo�����v �ʤ���
         protected bool hasRolledforCombochance=false;//�O�_�ݭn���s�վ��v

        [Header("Engagement distance")]
        [SerializeField] public float maximumEngaementDistance = 5; //�W�L�o�ӶZ���A����status

        public override AI_status Tick(AI_characterManager aicharacter)
        {
            if (aicharacter.isPerformingAction)
                return this;
            if (!aicharacter.navMeshAgent.enabled)
                aicharacter.navMeshAgent.enabled = true;

            //�pAI�줣�F�A�h���a��A�L�̷|�ݵۧA
            if (aicharacter.aI_CharacterCombatManager.enablePivot)
            {
                if(aicharacter.aI_CharacterCombatManager.viewableAngle>30 || aicharacter.aI_CharacterCombatManager.viewableAngle < -30)              
                    aicharacter.aI_CharacterCombatManager.pivotTowardsTarget(aicharacter);
                
            }

            aicharacter.aI_CharacterCombatManager.RotateTowardsAgent(aicharacter);

            if (aicharacter.charactercombatmanager.currenttarget == null)
                return SwitchState(aicharacter, aicharacter.idle);

            //����AI�٨S�������A����@�ӷs�������ʧ@
            if(!hasAttack)
            {
                GetNewAttack(aicharacter);
            }
            else
            {
                aicharacter.attack.currentAttack = chooseAttack;
                return SwitchState(aicharacter, aicharacter.attack);


            }

            if (aicharacter.aI_CharacterCombatManager.TargerDistance > maximumEngaementDistance)
                return SwitchState(aicharacter, aicharacter.followTarget);


            NavMeshPath Path = new NavMeshPath();

            aicharacter.navMeshAgent.CalculatePath(aicharacter.aI_CharacterCombatManager.currenttarget.transform.position, Path);
            aicharacter.navMeshAgent.SetPath(Path);


             return this;
        }

        public virtual void GetNewAttack(AI_characterManager aI_Character)
        {
            potentialAttacks = new List<AI_attackAction>();
          

            foreach (var potentialAttack in aicharacterAttacks)
            {
                //Ĳ�o�@��if�A�ˬd�U�@�Ӱʧ@

                //�W�X�̤p�����Z��
                if (aI_Character.aI_CharacterCombatManager.TargerDistance < potentialAttack.minAttackDistance)
                    continue;
                //�W�X�̤j�����Z��
                if (aI_Character.aI_CharacterCombatManager.TargerDistance > potentialAttack.maxAttackDistance)
                    continue;
                //�W�X�̤p��������
                if (aI_Character.aI_CharacterCombatManager.viewableAngle < potentialAttack.minmumAttackAngle)
                    continue;
                //�W�X�̤j��������
                if (aI_Character.aI_CharacterCombatManager.viewableAngle > potentialAttack.maxmumAttackAngle)
                    continue;

                potentialAttacks.Add(potentialAttack);


            }

            if (potentialAttacks.Count <= 0)//�קK�ѰO�[�J�ʧ@
                return;

            var totalWeight = 0;

            foreach(var attack in potentialAttacks)
            {
                totalWeight += attack.attackWeight;
            }

            var randomWeightValue = Random.Range(1, totalWeight+1);
            var processedWeight = 0;

            foreach(var attack in potentialAttacks)
            {
                processedWeight += attack.attackWeight;

                if(randomWeightValue<=processedWeight)
                {
                    chooseAttack = attack;
                    previousAttack = chooseAttack;
                    hasAttack = true;
                    return;
                }
            }

        }
        
        protected virtual bool RollforoutcomeChance(int chance)
        {
            bool chanceWillbeperformed=false;

            int randomPercent=Random.Range(0, 100);

            if(randomPercent<= chance)
                chanceWillbeperformed=true;

            return chanceWillbeperformed;
        }
        protected override void ResetstatusFlags(AI_characterManager aicharacter)
        {
            base.ResetstatusFlags(aicharacter);
            hasRolledforCombochance = false;
            hasAttack = false;

        }

    }
}
