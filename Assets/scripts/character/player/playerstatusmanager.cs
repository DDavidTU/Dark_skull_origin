using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace sg
{
    public class playerstatusmanager : characterstatusmanager
    {
        PlayerManager player;
        protected override void  Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            caculateHpBaseofHpValevel(player.playerNetcodeManger.Hpva.Value);
            caculatestaminaBaseonEndurancelevel(player.playerNetcodeManger.endurance.Value);
        }
    }
}
