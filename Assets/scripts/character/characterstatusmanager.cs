using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace sg
{

    public class characterstatusmanager : MonoBehaviour
    {
        charactermanger chacater;
        [Header("ReGenerateStamina")]
        private float RegneratestaminaTimer = 0;
        [SerializeField] float RegneratestaminaTimerDelay = 1;
        private float staminaticktimer = 0;
        [SerializeField] int staminaRegenerateValue = 1;

        protected virtual void Awake()
        {
            chacater=GetComponent<charactermanger>();
        }
        protected virtual void Start()
        {
            
        }

        public int caculateHpBaseofHpValevel(int Hpva)
        {
            float Health = 0;

            Health = Hpva * 15;


            return Mathf.RoundToInt(Health);
        }

        public int caculatestaminaBaseonEndurancelevel(int endurance)
        {
            float stamina = 0;

            stamina = endurance * 10;


            return Mathf.RoundToInt(stamina);
        }

        public void RecoveryHP(int HPamount)
        {
            chacater.characternetcodeManager.currentHp.Value += HPamount;
        }
        public void ReGenerateStamina()
        {
            if (!chacater.IsOwner)
                return;
            if (chacater.characternetcodeManager.isSprint.Value)
                return;
            if (chacater.isPerformingAction)
                return;
            

            RegneratestaminaTimer += Time.deltaTime;

            if (RegneratestaminaTimer >= RegneratestaminaTimerDelay)
            {
                if (chacater.characternetcodeManager.currentstamina.Value < chacater.characternetcodeManager.Maxstamina.Value)
                {
                    staminaticktimer += Time.deltaTime;
                    if (staminaticktimer >= 0.1)
                    {
                        staminaticktimer = 0;
                        chacater.characternetcodeManager.currentstamina.Value += staminaRegenerateValue;
                    }
                }
            }



        }
        public void ResetStaminaRegenTimer(float preStaminaValue,float StaminaValue)
        {
            if (StaminaValue < preStaminaValue)
            {
                RegneratestaminaTimer = 0;
            }
        }

       
    }

}