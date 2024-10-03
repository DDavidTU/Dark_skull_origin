using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace sg
{
    public class weaponitem : item
    {
        [Header("weapon model")]
         public   GameObject weapon;
        public bool RightHand;
        [Header("weapon requirements")]
        public int strengthREQ = 0;
        public int dexREQ = 0;
        public int intREQ = 0;
        public int faithREQ = 0;
        [Header("weapon base damage")]
        public int physicdamage = 0;
        public int magicdamage = 0;
        public int firedamage = 0;
        public int lightningdamage = 0;
        public int holydamage = 0;
        [Header("weapon Base poise damage")]
        public int poisedamage = 10;
        public bool poiseBroken = true;
        [Header("Attack Modifiers")]
        public float light_Attack_Modifier = 1;
        public float light_Attack2_Modifier = 1.1f;
        public float light_Attack3_Modifier = 1.1f;

        public float heavy_Attack_Modifier = 1.5f;
        public float heavy_Attack2_Modifier = 1.7f;

        public float charger_attack_modifier = 2;
        public float charger_attack2_modifier = 2.2f;
        [Header("stamina cost Modifiers")]
        public int baseStaminacost = 20;

        public float  LightAttackstaminacostModifer = 1;
        public float LightAttack_comboing_staminacostModifer = 0.8f;
        public float LightAttack_comboing2_staminacostModifer = 0.7f;

        public float HeavyAttackstaminacostModifer = 1.5f;

        public float ChargeAttackstaminacostModifer = 2;


        [Header("Action")]
        //使用搖桿RB的動作
        public weaponitemAction R1_action;
        public weaponitemAction R2_action;
        public weaponitemAction L1_action;
        public weaponitemAction L2_action;
        [Header("SFX")]
        public AudioClip[] Attacksound;

        
        

    }
}
