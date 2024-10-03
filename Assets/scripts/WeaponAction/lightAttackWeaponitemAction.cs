using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave.Internal;
using UnityEngine;

namespace sg
{
    [CreateAssetMenu(menuName = "character action/weapon action/light attack")]
    public class lightAttackWeaponitemAction : weaponitemAction
    {

        [SerializeField] string light_attack = "Light_Attack01_";
        [SerializeField] string light_attack_2_ = "Light_Attack01_02";
        [SerializeField] string light_attack_3 = "Light_Attack01_03";

        public override void AttemptToPerforaction(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
        {

            

            base.AttemptToPerforaction(playerpreformintaction, weaponprefromingaction);

            if (!playerpreformintaction.IsOwner)
                return;

            if (playerpreformintaction.playerNetcodeManger.currentstamina.Value <= 0)
                return;

            if (!playerpreformintaction.isGround)
                return;
            preforLighymattack(playerpreformintaction, weaponprefromingaction);
        }
        private void preforLighymattack(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
        {
            if(playerpreformintaction.playercombatManager.canCombowithMainHandWeapon && playerpreformintaction.isPerformingAction)
            {
                playerpreformintaction.playercombatManager.canCombowithMainHandWeapon = false;

                 if(playerpreformintaction.playercombatManager.lastAttackAnimationperformed == light_attack)
                {
                    playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_2, light_attack_2_, true);

                }
               

                else if (playerpreformintaction.playercombatManager.lastAttackAnimationperformed == light_attack_2_)
                {
                    playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_3, light_attack_3, true);

                }
                else
                {
                    playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_1, light_attack, true);
                }




            }
            
            else if(!playerpreformintaction.isPerformingAction)
            {
                playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_1, light_attack, true);
            }

            if (playerpreformintaction.playercombatManager.canCombowithoffHandWeapon && playerpreformintaction.isPerformingAction)
            {
                playerpreformintaction.playercombatManager.canCombowithoffHandWeapon = false;

                if (playerpreformintaction.playercombatManager.lastAttackAnimationperformed == light_attack)
                {
                    playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_2, light_attack_2_, true);

                }


                else if (playerpreformintaction.playercombatManager.lastAttackAnimationperformed == light_attack_2_)
                {
                    playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_3, light_attack_3, true);

                }
                else
                {
                    playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_1, light_attack, true);
                }




            }

            else if (!playerpreformintaction.isPerformingAction)
            {
                playerpreformintaction.playerAnimatorManager.PlayTargetAttackactionAnimation(Attacktype.light_attack_1, light_attack, true);
            }

        }
    }
}
