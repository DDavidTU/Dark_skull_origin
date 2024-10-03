using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class MeeleWeaponDamageCollider : DamageCollider
    {

        [Header("Attecking Character")]
        public charactermanger characterCausingDamage;



        [Header("Weapon Attack Modifier")]
        public float Light_attack_modifier;
        public float Light_attack2_modifier;
        public float Light_attack3_modifier;

        public float Heavy_attack_modifier;
        public float Heavy_attack2_modifier;

        public float Charge_attack_modifier;
        public float Charge_attack2_modifier;

        protected override void Awake()
        {
            base.Awake();

            if(damagecollider==null)
            {
                damagecollider=GetComponent<Collider>();
            }

            damagecollider.enabled = false; //武器碰撞器，不應該一開始就打開，故開始先不開。
        }
        protected override void OnTriggerEnter(Collider other)
        {
            charactermanger damageTarget = other.GetComponentInParent<charactermanger>();


            if (damageTarget != null)
            {
                if (damageTarget == characterCausingDamage)
                    return;
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);


                targetdamage(damageTarget);
            }

        }
        
        protected override void targetdamage(charactermanger targetdamage)
        {
            if (characterdamage.Contains(targetdamage))
                return;
            characterdamage.Add(targetdamage);

            takedamageEffect damageeffect = Instantiate(worldCharacterEffectManager.instance.takedamageeffect);
            damageeffect.physicalDamage = physicalDamage;
            damageeffect.MagicDamage = MagicDamage;
            damageeffect.fireDamage = fireDamage;
            damageeffect.holyDamage = holyDamage;
            damageeffect.lightningDamage = lightningDamage;
            damageeffect.contactPoint = contactPoint;
            damageeffect.angleHitFrom = Vector3.SignedAngle(characterCausingDamage.transform.forward, targetdamage.transform.forward, Vector3.up);

            damageeffect.poiseisbroken = Poiseisbroken;


            switch (characterCausingDamage.charactercombatmanager.Currentattacktype)
            {
                case Attacktype.light_attack_1:
                    ApplyAttackModifier(Light_attack_modifier, damageeffect);
                    break;
                case Attacktype.light_attack_2:
                    ApplyAttackModifier(Light_attack2_modifier, damageeffect);
                    break;
                case Attacktype.light_attack_3:
                    ApplyAttackModifier(Light_attack3_modifier, damageeffect);
                    break;

                case Attacktype.Hold_Heavy_attack_1:
                    ApplyAttackModifier(Heavy_attack_modifier, damageeffect);
                    break;
               

                case Attacktype.Charge_attack_1:
                    ApplyAttackModifier(Charge_attack_modifier, damageeffect);
                    break;
               
                default:
                    break;

            }
            if(characterCausingDamage.IsOwner)
            {
                targetdamage.characternetcodeManager.NotifyTheServeofofCharacterDamageServerRpc(targetdamage.NetworkObjectId, characterCausingDamage.NetworkObjectId, 
                damageeffect.physicalDamage,
                damageeffect.MagicDamage,
                damageeffect.fireDamage,
                damageeffect.holyDamage,
                damageeffect.lightningDamage,
                damageeffect.poiseDamage,
                damageeffect.angleHitFrom,
                damageeffect.contactPoint.x,
                damageeffect.contactPoint.y,
                damageeffect.contactPoint.z,
                damageeffect.poiseisbroken

               );
            }
            
          
        }

        private void ApplyAttackModifier(float Modifier,takedamageEffect damage)
        {
            damage.physicalDamage *= Modifier;
            damage.MagicDamage *= Modifier;
            damage.fireDamage *= Modifier;
            damage.holyDamage *= Modifier;
            damage.lightningDamage *= Modifier;
            damage.poiseDamage *= Modifier;
        }
    }
}
