using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace sg
{
    public class charactercombatmanager : NetworkBehaviour
    {
        protected charactermanger character;

        [Header("Last Attack Animation performed")]
        public string lastAttackAnimationperformed;
        [Header("Attack Type")]
        public weaponitem currentweaponBeingused;
        public Attacktype Currentattacktype;
        [Header("Lock on")]
        public charactermanger currenttarget;
        public Transform lockonTransform;
        [Header("Hurt")]
        public bool be_attacked = false;

        protected virtual void Awake()
        {
            character = GetComponent<charactermanger>();
          
        }
        protected virtual void Start()
        {

        }

        public virtual void setTarget(charactermanger newtarget)
        {
            if (character.IsOwner)
            {

                if (newtarget != null)
                {
                    currenttarget = newtarget;
                    character.characternetcodeManager.CurrentObjectTargetID.Value = newtarget.GetComponent<NetworkObject>().NetworkObjectId;

                }
                else
                {
                    currenttarget = null;
                }

            }

        }
    }
}
