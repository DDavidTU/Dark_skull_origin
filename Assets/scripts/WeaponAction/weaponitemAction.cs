using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    [CreateAssetMenu(menuName ="character action/weapon action/test action")]
    public class weaponitemAction : ScriptableObject
    {
        public int actionID;


        public virtual void AttemptToPerforaction(PlayerManager playerpreformintaction, weaponitem weaponprefromingaction)
        {
            if (playerpreformintaction.IsOwner)
            {
                playerpreformintaction.playerNetcodeManger.currentweaponBeingused.Value = weaponprefromingaction.itemID;
            }
          
        }
    }
}
