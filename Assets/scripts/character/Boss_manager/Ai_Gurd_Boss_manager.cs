using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace sg
{
    public class Ai_Gurd_Boss_manager : AI_boss_manager
    {
       [SerializeField] public Boss_Gurd_soundFXsource boss_Gurd_SoundFX;
        protected override void Awake()
        {
            base.Awake();
            boss_Gurd_SoundFX = GetComponent<Boss_Gurd_soundFXsource>();

            characternetcodeManager.Damagepoise.Value = 40;

            this.GetComponent<NetworkObject>().Spawn();
        }
        protected override void Start()
        {
            base.Start();
            aI_CharacterAnimatorForManager.PlayTargetactionAnimation("Stand", false);
        }
    }
}
