using sg;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BigZombie_Manager : AI_characterManager
{
    [SerializeField] AI_UI_status_Bar Bar;
    protected override void Awake()
    {
        base.Awake();
        characternetcodeManager.Damagepoise.Value = 25;
        this.GetComponent<NetworkObject>().Spawn();
        Bar.enabled = true;
        Bar.EnableHp();
    }
}
