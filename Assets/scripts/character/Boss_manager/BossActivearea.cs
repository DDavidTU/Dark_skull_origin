using sg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class BossActivearea : MonoBehaviour
    {
       [SerializeField] public Ai_Gurd_Boss_manager Gurd_Boss;
        [SerializeField] private GameObject finalLight;
        [SerializeField] List<RockFall> Rock;

    
        private void OnTriggerEnter(Collider other)
        {
            charactermanger passTarget = other.GetComponentInParent<PlayerManager>();
            Gurd_Boss = FindObjectOfType<Ai_Gurd_Boss_manager>();

            if (passTarget!=null)
            {
                this.enabled = false;
                finalLight.SetActive(true);
                Gurd_Boss.AwakenkedBoss();
                Gurd_Boss.aI_CharacterCombatManager.currenttarget = passTarget;
                RockFall();
            }

        }

        private void RockFall()
        {
          for(int i = 0; i < Rock.Count; i++)
            {
                Rock[i].Fall=true;
            }
        }
}
}
