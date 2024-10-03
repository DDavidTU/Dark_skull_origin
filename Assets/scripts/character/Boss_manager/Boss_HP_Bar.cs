using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace sg
{
    public class Boss_HP_Bar : UI_status_Bar
    {

        
        [SerializeField] AI_boss_manager boss_manager;
        [SerializeField] float RemoveHpBarTime = 2;
        [SerializeField] PlayerManager player;
         [SerializeField] public GameObject HpGameobject;
        protected override void  Awake()
        {
            base.Awake();
            player=FindObjectOfType<PlayerManager>();
          
        }
        public void EnableBossHp(AI_boss_manager boss)
        {
            boss_manager = boss;
            boss_manager.aI_CharacterNetworkManager.currentHp.OnValueChanged += OnbossHpChanged;
            SetMaxStaus(boss_manager.aI_CharacterNetworkManager.MaxHp.Value);
            Setstatus(boss_manager.aI_CharacterNetworkManager.currentHp.Value);
            GetComponentInChildren<TextMeshProUGUI>().text = boss_manager.BossName;
        }

        private void OnDestroy()
        {
            boss_manager.aI_CharacterNetworkManager.currentHp.OnValueChanged -= OnbossHpChanged;
        }
        protected void Update()
        {
            if (player.IsDead.Value)
            {
                Destroy(HpGameobject);
            }
        }
        public void OnbossHpChanged(int oldvalue,int newvalue)
        {
            Setstatus(newvalue);
            if(newvalue<=0)
            {
                RemoveHpBar(); 
            }
           
        }
        public void RemoveHpBar()
        {
            Destroy(gameObject, RemoveHpBarTime);
        }
        

    }
}
