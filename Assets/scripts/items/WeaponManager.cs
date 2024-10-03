using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class WeaponManager : MonoBehaviour
    {
        public MeeleWeaponDamageCollider meeleDamageCollider;

        private void Awake()
        {
            meeleDamageCollider=GetComponentInChildren<MeeleWeaponDamageCollider>();
        }

        public void SetweaponDamage(charactermanger characterAttackValue, weaponitem weapon)
        {
            meeleDamageCollider.characterCausingDamage = characterAttackValue;
            meeleDamageCollider.physicalDamage = weapon.physicdamage;
            meeleDamageCollider.MagicDamage = weapon.magicdamage;
            meeleDamageCollider.fireDamage = weapon.firedamage;
            meeleDamageCollider.lightningDamage=weapon.lightningdamage;
            meeleDamageCollider.holyDamage=weapon.holydamage;
            meeleDamageCollider.Light_attack_modifier = weapon.light_Attack_Modifier;
            meeleDamageCollider.Light_attack2_modifier = weapon.light_Attack2_Modifier;
            meeleDamageCollider.Light_attack3_modifier = weapon.light_Attack3_Modifier;
            meeleDamageCollider.Heavy_attack_modifier = weapon.heavy_Attack_Modifier;
            meeleDamageCollider.Heavy_attack2_modifier = weapon.heavy_Attack2_Modifier;
            meeleDamageCollider.Charge_attack_modifier = weapon.charger_attack_modifier;
            meeleDamageCollider.Charge_attack2_modifier = weapon.charger_attack2_modifier;
            meeleDamageCollider.Poiseisbroken = weapon.poiseBroken;
        }
    }
}
