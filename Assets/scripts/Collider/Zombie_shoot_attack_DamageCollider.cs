using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class Zombie_shoot_attack_DamageCollider : DamageCollider
    {
        [SerializeField] public AI_characterManager ai_Zombie_character;

        protected override void Awake()
        {
            base.Awake();
            damagecollider = GetComponent<Collider>();
          
        }
        protected override void OnTriggerEnter(Collider other)
        {
            charactermanger damageTarget = other.GetComponentInParent<charactermanger>();


           


                if (damageTarget != null)
                {
                    if (damageTarget == ai_Zombie_character)
                        return;


                    if (damageTarget.characternetcodeManager.isInvulnerable.Value)
                        return;

                if (damageTarget.charactergroup == ai_Zombie_character.charactergroup)
                    return;

                Destroy(gameObject);

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
            damageeffect.angleHitFrom = Vector3.SignedAngle(ai_Zombie_character.transform.forward, targetdamage.transform.forward, Vector3.up);

            switch (ai_Zombie_character.charactercombatmanager.Currentattacktype)
            {

                default:
                    break;

            }
            if (targetdamage.IsOwner)
            {
                targetdamage.characternetcodeManager.NotifyTheServeofofCharacterDamageServerRpc(targetdamage.NetworkObjectId, ai_Zombie_character.NetworkObjectId,
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
    }
}
    
