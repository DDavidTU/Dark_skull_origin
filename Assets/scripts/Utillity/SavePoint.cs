using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class SavePoint : interactable
    {
        [SerializeField] Collider saverange;
        [SerializeField] GameObject savering;


        public override void Caninteractable(PlayerManager player)
        {
            base.Caninteractable(player);
            SavePointGame(player);
        }
        private void SavePointGame(PlayerManager player)
        {
          

            player.playerAnimatorManager.PlayTargetactionAnimation("Save", false);

        }
    }
}
