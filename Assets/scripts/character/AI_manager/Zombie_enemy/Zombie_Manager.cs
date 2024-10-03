using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace sg
{
    public class Zombie_Manager : AI_characterManager
    {
        [Header("Start Action")]
        [SerializeField] string Zombieeating;
        [SerializeField] AI_UI_status_Bar Bar;
        [SerializeField] float startAnimationtime;
        protected override void Awake()
        {
            base.Awake();
            characternetcodeManager.Damagepoise.Value = 10;
            this.GetComponent<NetworkObject>().Spawn();
            Bar.enabled = true;
            Bar.EnableHp();

        }
        protected override void Start()
        {
            base.Start();
            StartCoroutine(ZombieEatAnimation(startAnimationtime));
        }

         private  IEnumerator ZombieEatAnimation(float time)
        {
            yield return new WaitForSeconds(time);
            aI_CharacterAnimatorForManager.PlayTargetactionAnimation(Zombieeating, false);
        }
           

            




    }
}
