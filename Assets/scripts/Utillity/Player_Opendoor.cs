using Cinemachine;
using sg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class Player_Opendoor : interactable
    {
        [SerializeField] public GameObject Door;
        [SerializeField] public bool open = false;
        [SerializeField] public float Pushangle = 4;
        [SerializeField] private float PushSpeed=4;
        [SerializeField] private Collider checkpoint;
        
     
        public override void Caninteractable(PlayerManager player)
        {
            base.Caninteractable(player);
            OpenDoor( player);
        }
        private void OpenDoor(PlayerManager player)
        {
            player.playerAnimatorManager.PlayTargetactionAnimation("Push Door", false);
            open=true;
            checkpoint.enabled = false;

          
        }

        private void Update()
        {
            if (open) { Door.transform.rotation = Quaternion.Slerp(Door.transform.rotation, Quaternion.Euler(0, Pushangle, 0), Time.deltaTime * PushSpeed); }
        }
    }
}
