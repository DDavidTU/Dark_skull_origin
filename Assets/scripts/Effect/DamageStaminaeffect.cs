using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    [CreateAssetMenu(menuName = "CharacterEffect/Instant Effect/stamina damage Effect")]

    public class DamageStaminaeffect : InstantEffectManager
    {
        public int StaminaDamage;

        public override void ProcessEffect(charactermanger character)
        {
            CalculateStaminaDamage(character);
        }
        private void CalculateStaminaDamage(charactermanger character)
        {  
            if(character.IsOwner)
            {
                Debug.Log("staminaDamaage= "+StaminaDamage);
                character.characternetcodeManager.currentstamina.Value -= StaminaDamage;
            }
        }

    }
}

