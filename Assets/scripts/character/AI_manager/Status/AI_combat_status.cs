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
        public List<AI_attackAction> aicharacterAttacks;//角色所能做的動作
        public List<AI_attackAction> potentialAttacks;
        private AI_attackAction chooseAttack;
        private AI_attackAction previousAttack;
        protected bool hasAttack = false;


        [Header("Combo")]
        [SerializeField] protected bool canpreformcombo = false;
        [SerializeField] protected int chanceToperformcombo = 25;//執行combo的機率 百分比
         protected bool hasRolledforCombochance=false;//是否需要重新調機率

        [Header("Engagement distance")]
        [SerializeField] public float maximumEngaementDistance = 5; //超過這個距離，改變status

        public override AI_status Tick(AI_characterManager aicharacter)
        {
            if (aicharacter.isPerformingAction)
                return this;
            if (!aicharacter.navMeshAgent.enabled)
                aicharacter.navMeshAgent.enabled = true;

            //如AI到不了你去的地方，他們會看著你
            if (aicharacter.aI_CharacterCombatManager.enablePivot)
            {
                if(aicharacter.aI_CharacterCombatManager.viewableAngle>30 || aicharacter.aI_CharacterCombatManager.viewableAngle < -30)              
                    aicharacter.aI_CharacterCombatManager.pivotTowardsTarget(aicharacter);
                
            }

            aicharacter.aI_CharacterCombatManager.RotateTowardsAgent(aicharacter);

            if (aicharacter.charactercombatmanager.currenttarget == null)
                return SwitchState(aicharacter, aicharacter.idle);

            //假使AI還沒有攻擊，獲取一個新的攻擊動作
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
                //觸發一個if，檢查下一個動作

                //超出最小攻擊距離
                if (aI_Character.aI_CharacterCombatManager.TargerDistance < potentialAttack.minAttackDistance)
                    continue;
                //超出最大攻擊距離
                if (aI_Character.aI_CharacterCombatManager.TargerDistance > potentialAttack.maxAttackDistance)
                    continue;
                //超出最小攻擊角度
                if (aI_Character.aI_CharacterCombatManager.viewableAngle < potentialAttack.minmumAttackAngle)
                    continue;
                //超出最大攻擊角度
                if (aI_Character.aI_CharacterCombatManager.viewableAngle > potentialAttack.maxmumAttackAngle)
                    continue;

                potentialAttacks.Add(potentialAttack);


            }

            if (potentialAttacks.Count <= 0)//避免忘記加入動作
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
