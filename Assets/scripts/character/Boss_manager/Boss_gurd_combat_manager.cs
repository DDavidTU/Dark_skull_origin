using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class Boss_gurd_combat_manager : AI_characterCombatManager
    {
        Ai_Gurd_Boss_manager aI_Boss_gurd_Manager;

        [Header("Damgagecolliders")]
        [SerializeField] public boss_Gurd_sword_DamageCollider Boss_SwordDamageCollider;
        [SerializeField]public boss_Gurd_swordEnergy_DamageCollider Boss_SwordEnergyCollider;

        [Header("damage")]
         int baseDamage = 50;
        float attack1_Modifier = 1.1f;
         float attack2_Modifier = 1.2f;
         float attack3_Modifier = 1.4f;
        float RunAttack_Modifier = 2f;
        [Header("Swordenergy")]
        [SerializeField] Transform SwordenergyTransform;
        [SerializeField] GameObject Swordenergy;

        protected override void Awake()
        {
            base.Awake();

            aI_Boss_gurd_Manager=GetComponent<Ai_Gurd_Boss_manager>();

        }

        public void setGurdAttack1Damage()
        {
            Boss_SwordDamageCollider.physicalDamage = baseDamage * attack1_Modifier;
           
      
        }
        public void setGurdAttack2Damage()
        {
            Boss_SwordDamageCollider.physicalDamage = baseDamage * attack2_Modifier;
      
        }
        public void setGurdAttack3Damage()
        {
            Boss_SwordDamageCollider.physicalDamage = baseDamage * attack3_Modifier;

        }
        public void setGurdRunAttackDamage()
        {
            Boss_SwordDamageCollider.physicalDamage = baseDamage * RunAttack_Modifier;

        }


        public void OpenBoss_SwordDamageColliderDamage()
        {
            Boss_SwordDamageCollider.EnabledamageCollider();
            
        }
       
        public void CloseBoss_SwordDamageColliderDamage()
        {
            Boss_SwordDamageCollider.DisabledamageCollider();
            aI_Boss_gurd_Manager.charactersoundFXsource.playSoundFX(worldItemDatabase.Instance.ChoseRandonSound(aI_Boss_gurd_Manager.boss_Gurd_SoundFX.SwordVoice));
           
        }
       
       
        

        public void Gurd_attackmove()
        {
            aI_Character.attack.followattack = true;

        }
        public void Gurd_Disattackmove()
        {
            aI_Character.attack.followattack = false;

        }
        public void SwordenergyAttack()
        {
            GameObject SwordEnergyClone = Instantiate(Swordenergy, SwordenergyTransform.position, SwordenergyTransform.rotation);

            boss_Gurd_swordEnergy_DamageCollider shoot_attack_DamageCollider = SwordEnergyClone.GetComponent<boss_Gurd_swordEnergy_DamageCollider>();
            shoot_attack_DamageCollider.ai_Boss_Gurd_character = aI_Boss_gurd_Manager;




        }
       public override void pivotTowardsTarget(AI_characterManager ai_character)
        {
            if (ai_character.isPerformingAction)
                return;

            
             if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("right_90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                ai_character.characterAnimatorforManager.PlayTargetactionAnimation("left_90", true);
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

        

    }
}

