using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class AI_UI_status_Bar : UI_status_Bar
    {
        
        [SerializeField] AI_characterManager AI_manager;

      

        protected override void Awake()
        {
            base.Awake();

            


        }
      


        public void EnableHp()
        {
            
            AI_manager.aI_CharacterNetworkManager.currentHp.OnValueChanged += OnAIHpChanged;
            SetMaxStaus(AI_manager.aI_CharacterNetworkManager.MaxHp.Value);
            Setstatus(AI_manager.aI_CharacterNetworkManager.currentHp.Value);
         
        }
        private void OnDestroy()
        {
            AI_manager.aI_CharacterNetworkManager.currentHp.OnValueChanged -= OnAIHpChanged;
        }

        public void OnAIHpChanged(int oldvalue, int newvalue)
        {
            Setstatus(newvalue);


        }


        
     
       
    }
}
