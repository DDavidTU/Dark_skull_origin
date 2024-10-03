using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace sg
{

    [CreateAssetMenu(menuName = "character action/weapon action/Heavy attack")]
    public class HeavyAttackWeaponitemAction : weaponitemAction
    {
       

            [SerializeField] string Heavy_attack_1 = "Heavy_attack1";
       
        public override void AttemptToPerforaction(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
            {
                base.AttemptToPerforaction(playerpreformintaction, weaponprefromingaction);

                if (!playerpreformintaction.IsOwner)
                    return;

                if (playerpreformintaction.playerNetcodeManger.currentstamina.Value <= 0)
                    return;

                if (!playerpreformintaction.isGround)
                    return;
                preforHeavyattack(playerpreformintaction, weaponprefromingaction);
            }
            private void preforHeavyattack(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
            {
            

            if (playerpreformintaction.playercombatManager.canCombowithMainHandWeapon && playerpreformintaction.isPerformingAction)
            {
                playerpreformintaction.playercombatManager.canCombowithMainHandWeapon = false;

              
               playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.Hold_Heavy_attack_1, Heavy_attack_1, true);
                
            }

            else if (!playerpreformintaction.isPerformingAction)
            {
                playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.Hold_Heavy_attack_1, Heavy_attack_1, true);
            }

        }

    }
}
