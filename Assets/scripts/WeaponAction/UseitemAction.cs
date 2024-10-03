using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    [CreateAssetMenu(menuName = "character action/weapon action/Use item")]
    public class UseitemAction : weaponitemAction
    {
        [SerializeField] string Useaction;
        public override void AttemptToPerforaction(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
        {
            base.AttemptToPerforaction(playerpreformintaction, weaponprefromingaction);

            if (!playerpreformintaction.IsOwner)
                return;

           

            if (!playerpreformintaction.isGround)
                return;
            preforuseitem(playerpreformintaction, weaponprefromingaction);
        }
        private void preforuseitem(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
        {
          
            if ( !playerpreformintaction.isPerformingAction)
            {
             

                
                playerpreformintaction.playerAnimatorManager.PlayTargetactionAnimation(Useaction, true,false,true,true);

            }

           
        }

    }
}

