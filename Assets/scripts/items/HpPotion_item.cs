using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sg
{
    [CreateAssetMenu(menuName = "Items/consumable_item/HpPotion_item")]
    public class HpPotion_item : consumable_item
    {
        [Header("potion Type")]
        public bool HPpotion;
        [Header("Recovery Amount")]
        public int HPRecoveryamount;
        [Header("Recovery FX")]
        public GameObject recoveryFX;
        

        public override void AttemptoconsumeItem(weaponmodelinitialSlot playercurrentweaponposition,  playerEffectManager playerEffect)
        {
            base.AttemptoconsumeItem(  playercurrentweaponposition,  playerEffect);

            GameObject potion = Instantiate(itemModel, playercurrentweaponposition.transform);
            playerEffect.currentParticleFX = recoveryFX;
            playerEffect.Healamount = HPRecoveryamount;
            playerEffect.instantiatedFXModel = potion;
            playerEffect.weaponslot = playercurrentweaponposition;
            playercurrentweaponposition.unLoadWeapon();
           



            playerEffect.HealplayerFromEffect();
        }
    }
}
