using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave.Models;
using UnityEngine;

namespace sg
{
    
    public class consumable_item : item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;
        [Header("Item Model")]
        public GameObject itemModel;
        [Header("Amimations")]
        public string consumableAnimation;
        public bool isinteracting;
        
        public virtual void AttemptoconsumeItem(weaponmodelinitialSlot playercurrentweaponposition, playerEffectManager playerEffect)
        {
         

        }
    }
}
