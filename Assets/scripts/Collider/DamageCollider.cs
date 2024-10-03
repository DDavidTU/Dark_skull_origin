using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        [SerializeField] protected Collider damagecollider;

      

        [Header("Damage")]
        public float physicalDamage = 0;
        public float MagicDamage = 0;
        public float fireDamage = 0;
        public float holyDamage = 0;
        public float lightningDamage = 0;
        public bool Poiseisbroken=true;

        [Header("Contact point")]
        public Vector3 contactPoint;

        [Header("character Damaged")]
        protected List<charactermanger> characterdamage=new List<charactermanger>();

       

        protected virtual void Awake()
        {

        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            charactermanger damageTarget = other.GetComponentInParent<charactermanger>();


            if (damageTarget != null)
            {

                if (damageTarget.characternetcodeManager.isInvulnerable.Value)
                    return;

                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);


                targetdamage(damageTarget);
            }
        }

        protected virtual void targetdamage(charactermanger targetdamage)
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
            damageeffect.poiseisbroken = Poiseisbroken;


            targetdamage.charactereffectManager.ProcessInstantEffect(damageeffect);


        }

        public virtual void EnabledamageCollider()
        {
            damagecollider.enabled = true;
        }
        public virtual void DisabledamageCollider()
        {
            damagecollider.enabled = false;
            characterdamage.Clear();// reset 以便可以再次造成傷害
        }
    }

}