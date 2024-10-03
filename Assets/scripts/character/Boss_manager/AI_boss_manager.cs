using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace sg
{
    public class AI_boss_manager : AI_characterManager
    {

        public string BossName = "secret Gurd";
        public int BossID = 0;
        public NetworkVariable<bool> BossfightisActive = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [SerializeField] public bool fightisActive = false;
        [SerializeField] bool BossHasbeenDefeat = false;
        [SerializeField] bool BossHasbeenAweakened = false;
        [SerializeField] bool appearBar = false;

        [SerializeField] List<FogwallScritable> fogwalls;

       

      
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            BossfightisActive.OnValueChanged += OnbossfightisChanged;
            OnbossfightisChanged(false, BossfightisActive.Value);
            if (IsServer)
            {
                if(!worldsavegamemanger.instance.currentdata.bossesAwakened.ContainsKey(BossID))
                {
                    worldsavegamemanger.instance.currentdata.bossesAwakened.Add(BossID, false);
                    worldsavegamemanger.instance.currentdata.bossesDefeat.Add(BossID, false);
                }
                else
                {
                    BossHasbeenDefeat = worldsavegamemanger.instance.currentdata.bossesDefeat[BossID];
                    BossHasbeenAweakened = worldsavegamemanger.instance.currentdata.bossesAwakened[BossID];
                   
                }

                //霧牆與boss共享ID可以對應指定的霧牆
                StartCoroutine(GetFogwallFromWorldobjectManager());
               

                //boss甦醒，呼叫霧牆擋住路
                if(BossHasbeenAweakened)
                {
                    for(int i=0;i<fogwalls.Count;i++)
                    {
                        fogwalls[i].isActive.Value = true;
                    }
                }
                //boss已被擊敗，霧牆散退
                if (BossHasbeenDefeat)
                {
                    for (int i = 0; i < fogwalls.Count; i++)
                    {
                        fogwalls[i].isActive.Value = false;
                    }
                    aI_CharacterNetworkManager.isActive.Value = false;
                }
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            BossfightisActive.OnValueChanged -= OnbossfightisChanged;
        }
        //使用協議，使輸入更順暢
        public  IEnumerator GetFogwallFromWorldobjectManager()
        {
            while (WorldObjectmanager.instance.Fogwalls.Count == 0)
               yield return new WaitForEndOfFrame();

           
            fogwalls = new List<FogwallScritable>();
            foreach (var fogwall in WorldObjectmanager.instance.Fogwalls)
            {
                if (fogwall.FogWallID == BossID)
                    fogwalls.Add(fogwall);
            }

        }
        protected override void Update()
        {
            base.Update();
            if (BossHasbeenAweakened)
            {
                BossHasbeenAweakened = false;
                
            }
           
        }

        public override IEnumerator ProcessDeathEvent(bool manualSelectDeadAnimator = false)
        {
            if (IsOwner)
            {
                characternetcodeManager.currentHp.Value = 0;
                IsDead.Value = true;
                BossfightisActive.Value = false;
                fightisActive = false;
                if (!manualSelectDeadAnimator)
                {
                    
                    characterAnimatorforManager.PlayTargetactionAnimation("dead_1", true);
                }

                BossHasbeenDefeat = true;
                    if (!worldsavegamemanger.instance.currentdata.bossesAwakened.ContainsKey(BossID))
                    {
                        worldsavegamemanger.instance.currentdata.bossesAwakened.Add(BossID, true);
                        worldsavegamemanger.instance.currentdata.bossesDefeat.Add(BossID, true);
                    }
                    else
                    {
                        worldsavegamemanger.instance.currentdata.bossesAwakened.Remove(BossID);
                        worldsavegamemanger.instance.currentdata.bossesDefeat.Remove(BossID);
                        worldsavegamemanger.instance.currentdata.bossesAwakened.Add(BossID, true);
                        worldsavegamemanger.instance.currentdata.bossesDefeat.Add(BossID, true);
                    }
                    worldsavegamemanger.instance.SaveGame();
                
            }

            yield return new WaitForSeconds(5);

        }

        public void AwakenkedBoss()
        {

            if (IsOwner)
            {

                fightisActive = true;
                BossfightisActive.Value = true;
                BossHasbeenDefeat = true;
                if (!worldsavegamemanger.instance.currentdata.bossesAwakened.ContainsKey(BossID))
                {
                    worldsavegamemanger.instance.currentdata.bossesAwakened.Add(BossID, true);

                }
                else
                {
                    worldsavegamemanger.instance.currentdata.bossesAwakened.Remove(BossID);

                    worldsavegamemanger.instance.currentdata.bossesAwakened.Add(BossID, true);

                }

                for (int i = 0; i < fogwalls.Count; i++)
                {
                    fogwalls[i].isActive.Value = true;
                }
            }
        }

       private void OnbossfightisChanged(bool oldstatus, bool newstatus)
        {

            if(fightisActive)
            {
                if (!appearBar)
                {
                    GameObject bossHealthBar = Instantiate(playerUIManger.instance.playeruihubmanager.bossHealthBarObject, playerUIManger.instance.playeruihubmanager.bossHealthBarParent);
                    Boss_HP_Bar bossHPBar = bossHealthBar.GetComponentInChildren<Boss_HP_Bar>();

                    bossHPBar.EnableBossHp(this);
                    appearBar = true;
                }
            }
        }
        
           
    }
}
