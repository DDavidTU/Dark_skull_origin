using Unity.Netcode;
using UnityEngine;

namespace sg
{
    public class characterNetworkManager : NetworkBehaviour
    {

        public charactermanger character;

        [Header("is Active")]
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [Header("position")]
        public NetworkVariable<Vector3> networkposition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkrotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkpositionVelocity;
        public float networksmoothTime = 0.1f;
        public float networkrotationTime = 0.1f;
        [Header("Target")]
        public NetworkVariable<ulong> CurrentObjectTargetID= new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [Header("Animator")]
        public NetworkVariable<bool> ismoving = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> HorizontalParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> VerticalParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> MoveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [Header("flags")]
        public NetworkVariable<bool> isInvulnerable = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isSprint = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isjump = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool>isLocking=new NetworkVariable<bool>(false,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isChargeAttacking = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [Header("Status")]
        public NetworkVariable<int> Hpva = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> endurance = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);      
        [Header("resource")]
        public NetworkVariable<int> currentHp = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> MaxHp = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentstamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> Maxstamina = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Damage poise ")]
        public NetworkVariable<int> Damagepoise  = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);




        protected virtual void  Awake()
        {
            character = GetComponent<charactermanger>();
        }

        public void CheckHp(int oldvalue,int newvalue)
        {
            if(currentHp.Value<=0)
            {
                StartCoroutine(character.ProcessDeathEvent());
            }
            if(character.IsOwner)
            {
                if(currentHp.Value>MaxHp.Value)
                {
                    currentHp.Value = MaxHp.Value;
                }
            }
        }

        public void onLockChangeTargetID(ulong oldid,ulong newid)
        {
            if (!IsOwner)
            {
                character.charactercombatmanager.currenttarget = NetworkManager.Singleton.SpawnManager.SpawnedObjects[newid].gameObject.GetComponent<charactermanger>();
            }
        }
        public void onislockedonchange(bool old,bool islockedon)
        {
            if (!islockedon)
            {
                character.charactercombatmanager.currenttarget = null; 
            }
        }
        public void onisChargeAttackChanged(bool old, bool newStatus)
        {
            character.animator.SetBool("isCharge", isChargeAttacking.Value);
        }
        public void ismovingChanged(bool old, bool newStatus)
        {
            character.animator.SetBool("IsMoving", newStatus);
        }

        public virtual void isActivingChanged(bool old, bool newstatus)
        {
            gameObject.SetActive(newstatus);
        }

        //下方為動畫宣告
        [ServerRpc]
        public void NotifyserveofactionanimationServerRpc(ulong clientID, string animationID, bool applyrootmotion)
        {
            if (IsServer)
            {
                playactionanimationForAllcilentClientRpc(clientID, animationID, applyrootmotion);
            }
        }
        [ClientRpc]
        public void playactionanimationForAllcilentClientRpc(ulong clientID, string animationID, bool applyrootmotion)
        {
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                performactionanimationfromserve(animationID, applyrootmotion);
            }
        }
        public void performactionanimationfromserve(string animationID, bool applyrootmotion)
        {
            character.applyRootMotion = applyrootmotion;
            character.animator.CrossFade(animationID, 0.2f);
        }



        //下方為攻擊宣告
        [ServerRpc]
       public void NotifyTheServeofofAttackActionServerRpc(ulong clientID, string animationID, bool applyrootmotion)
        {
            if (IsServer)
            {
                NotifyTheServeofofAttackActionClientRpc(clientID, animationID, applyrootmotion);
            }
        }
        [ClientRpc]
        public void NotifyTheServeofofAttackActionClientRpc(ulong clientID, string animationID, bool applyrootmotion)
        {
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                performAttackactionanimationfromserve(animationID, applyrootmotion);
            }

        }
        public void performAttackactionanimationfromserve(string animationID, bool applyrootmotion)
        {
            character.applyRootMotion = applyrootmotion;
            character.animator.CrossFade(animationID, 0.2f);
        }





        [ServerRpc(RequireOwnership = false)]
        public void NotifyTheServeofofCharacterDamageServerRpc(ulong damagedCharacterID, ulong characterCausingDamageID,
            float physicalDamage,
            float MagicDamage,
            float fireDamage,
            float holyDamage,
            float lightningDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ,
            bool poiseBroken
            )
        {
            if(IsServer)
            {
                NotifyTheServeofofCharacterDamageClientRpc(damagedCharacterID, characterCausingDamageID,
             physicalDamage,
             MagicDamage,
             fireDamage,
             holyDamage,
             lightningDamage,
             poiseDamage,
             angleHitFrom,
             contactPointX,
             contactPointY,
             contactPointZ,
             poiseBroken
              );
            }
        }


        [ClientRpc]
        public void NotifyTheServeofofCharacterDamageClientRpc(ulong damagedCharacterID, ulong characterCausingDamageID,
            float physicalDamage,
            float MagicDamage,
            float fireDamage,
            float holyDamage,
            float lightningDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ,
            bool poiseBroken)
        {
            ProcessCharacterDamageFromServer(damagedCharacterID, characterCausingDamageID,
             physicalDamage,
             MagicDamage,
             fireDamage,
             holyDamage,
             lightningDamage,
             poiseDamage,
             angleHitFrom,
             contactPointX,
             contactPointY,
             contactPointZ,
             poiseBroken);
        }
        public void ProcessCharacterDamageFromServer(ulong damagedCharacterID, ulong characterCausingDamageID,
            float physicalDamage,
            float MagicDamage,
            float fireDamage,
            float holyDamage,
            float lightningDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ,
            bool poiseBroken)
        {
            charactermanger damagedcharacterter = NetworkManager.Singleton.SpawnManager.SpawnedObjects[damagedCharacterID].gameObject.GetComponent<charactermanger>();
            charactermanger characterCausingDamag = NetworkManager.Singleton.SpawnManager.SpawnedObjects[characterCausingDamageID].gameObject.GetComponent<charactermanger>();

            takedamageEffect damageeffect = Instantiate(worldCharacterEffectManager.instance.takedamageeffect);
            damageeffect.physicalDamage = physicalDamage;
            damageeffect.MagicDamage = MagicDamage;
            damageeffect.fireDamage = fireDamage;
            damageeffect.holyDamage = holyDamage;
            damageeffect.lightningDamage = lightningDamage;
            damageeffect.poiseDamage = poiseDamage ;
            damageeffect.poiseisbroken= poiseBroken;
            damageeffect.angleHitFrom = angleHitFrom;
            damageeffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);
            damageeffect.charactercausedamage= characterCausingDamag;
            


            damagedcharacterter.charactereffectManager.ProcessInstantEffect(damageeffect);

        }


    }
}
