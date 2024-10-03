using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class zombie_CombatManager : AI_characterCombatManager
    {
       

        [Header("Damgagecolliders")]
        [SerializeField] public ZombieBiteDamageCollider BiteDamageCollider;
        [SerializeField]public ZombieHandDamageCollider RightdamageCollider;
        [SerializeField] public ZombieHandDamageCollider LeftdamageCollider;
        


        [Header("damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack1_Modifier = 1.1f;
        [SerializeField] float attack2_Modifier = 1.1f;
        [SerializeField] float Bite_Modifier = 2;

        [Header("Shoot Attack")]
        [SerializeField] Transform ShootTransform;
        [SerializeField] GameObject ShootBall;
        protected override void Awake()
        {
            base.Awake();
            
           


        }
      
        


        public void setAttack1Damage()
        {
            RightdamageCollider.physicalDamage =baseDamage*attack1_Modifier;
           LeftdamageCollider.physicalDamage = baseDamage * attack1_Modifier;
            aI_Character.charactersoundFXsource.playZombieAttackSound();
        }
        public void setAttack2Damage()
        {
            RightdamageCollider.physicalDamage = baseDamage * attack2_Modifier;
            LeftdamageCollider.physicalDamage = baseDamage * attack2_Modifier;
            aI_Character.charactersoundFXsource.playZombieAttackSound();
        }
        public void setAttackGripDamage()
        {
            RightdamageCollider.physicalDamage = 0;
            LeftdamageCollider.physicalDamage = 0;
            aI_Character.charactersoundFXsource.playZombieAttackSound();
        }
        public void setAttackBiteDamage()
        {
            BiteDamageCollider.physicalDamage = baseDamage* Bite_Modifier;
            aI_Character.charactersoundFXsource.playZombieBiteSound();
        }
     

        public void OpenRightColliderDamage()
        {
            RightdamageCollider.EnabledamageCollider();
           
        }
        public void OpenLeftColliderDamage()
        {
            LeftdamageCollider.EnabledamageCollider();
            

        }
        public void CloseRightColliderDamage()
        {
            RightdamageCollider.DisabledamageCollider();

        }    
        public void CloseLeftColliderDamage()
        {
            LeftdamageCollider.DisabledamageCollider();
        }
        public void OpenBiteColliderDamage()
        {
            BiteDamageCollider.EnabledamageCollider();

        }
        public void CloseBiteColliderDamage()
        {
            BiteDamageCollider.DisabledamageCollider();

        }

        public void attackmove()
        {
            aI_Character.attack.followattack = true;
          
        }
        public void Disattackmove()
        {
            aI_Character.attack.followattack = false;
            
        }
        public void Opengripbody()
        {

            isGrip = true;
            
        }
        public void closegripbody()
        {
            isGrip = false;
            aI_Character.animator.SetBool("isGrip", false);
        }

      
        
        public void ZombieShoot()
        {
           
            GameObject ZombieShootBallClone = Instantiate(ShootBall, ShootTransform.position, ShootTransform.rotation);
            
            Zombie_shoot_attack_DamageCollider shoot_attack_DamageCollider = ZombieShootBallClone.GetComponent<Zombie_shoot_attack_DamageCollider>();
            shoot_attack_DamageCollider.ai_Zombie_character = aI_Character;

            aI_Character.charactersoundFXsource.playZombieShootSound();
        }

    }
}
