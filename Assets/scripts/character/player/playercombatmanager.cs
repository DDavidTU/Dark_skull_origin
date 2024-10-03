using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class playercombatmanager : charactercombatmanager
    {
        PlayerManager player;

        [Header("Flags")]
        public bool canCombowithMainHandWeapon = false;
        public bool canCombowithoffHandWeapon = false;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
           

        }
        protected override void Start()
        {
            base.Start();
            
        }
        public void preformWeaponBasedAction(weaponitemAction weaponaction, weaponitem weaponpreformingaction)
        {

            if (player.IsOwner)
            {
                //°õ¦æ°Ê§@
                weaponaction.AttemptToPerforaction(player, weaponpreformingaction);
                Debug.Log(weaponpreformingaction);
                player.playerNetcodeManger.NotifyTheServeofWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId,weaponpreformingaction.itemID,weaponaction.actionID);
            }
        }
       public virtual void DrainstaminaBasedOnAttack()
        {
            if (!player.IsOwner)
                return;
            if (currentweaponBeingused == null)
                return;

            float staminaDeducted = 0;
            switch(Currentattacktype)
            {
                case Attacktype.light_attack_1:
                    staminaDeducted = currentweaponBeingused.baseStaminacost * currentweaponBeingused.LightAttackstaminacostModifer;
                    break;
                case Attacktype.light_attack_2:
                    staminaDeducted = currentweaponBeingused.baseStaminacost * currentweaponBeingused.LightAttack_comboing_staminacostModifer;
                    break;
                case Attacktype.light_attack_3:
                    staminaDeducted = currentweaponBeingused.baseStaminacost * currentweaponBeingused.LightAttack_comboing2_staminacostModifer;
                    break;
                case Attacktype.Hold_Heavy_attack_1:
                    staminaDeducted = currentweaponBeingused.baseStaminacost * currentweaponBeingused.HeavyAttackstaminacostModifer;
                    break;
                case Attacktype.Charge_attack_1:
                    staminaDeducted = currentweaponBeingused.baseStaminacost * currentweaponBeingused.ChargeAttackstaminacostModifer;
                    break;
                default:
                    break;
            }
            Debug.Log("Cost Stamina " + staminaDeducted );
            player.playerNetcodeManger.currentstamina.Value -=Mathf.RoundToInt( staminaDeducted);
        }
        public override void setTarget(charactermanger newtarget)
        {
            base.setTarget(newtarget);

            if (player.IsOwner)
            {
                Playercamera.instance.SetlockcameraHeight();


            }

        }

       

    }
}
