using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sg
{
    public class player_opendoorBigDoor : interactable
    {
        [SerializeField] public GameObject Door1;
        [SerializeField] public GameObject Door2;
        [SerializeField] public bool open = false;
        [SerializeField] public float Pushangle1 = 4;
        [SerializeField] public float Pushangle2 = 4;
        [SerializeField] private float PushSpeed1 = 4;
        [SerializeField] private GameObject Clearcamera;
       
      
        [SerializeField] private Collider checkpoint;
        public override void Caninteractable(PlayerManager player)
        {
            base.Caninteractable(player);
            OpenDoor(player);
        }
        private void OpenDoor(PlayerManager player)
        {
            player.playerAnimatorManager.PlayTargetactionAnimation("Push Door", false);
            open = true;
            checkpoint.enabled = false;
            Clearcamera.SetActive(true);
            playerUIManger.instance.gameObject.SetActive(false);
            Playerinputmnager.instance.enabled = false;
            Playercamera.instance.gameObject.SetActive(false);

        }

        private void Update()
        {
            if (open)
            {
                Door1.transform.rotation = Quaternion.Slerp(Door1.transform.rotation, Quaternion.Euler(0, Pushangle1, 0), Time.deltaTime * PushSpeed1);
                Door2.transform.rotation = Quaternion.Slerp(Door2.transform.rotation, Quaternion.Euler(0, Pushangle2, 0), Time.deltaTime * PushSpeed1);
                
             
                
              


            }
        }
    }
}
