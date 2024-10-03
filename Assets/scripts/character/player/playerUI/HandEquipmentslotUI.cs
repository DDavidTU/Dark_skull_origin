using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace sg
{
    public class HandEquipmentslotUI : MonoBehaviour
    {
        public Image icon;
        public weaponitem weapon;

        public bool righthandslot1;
        public bool righthandslot2;
        public bool righthandslot3;
        public bool lefthandslot1;
        public bool lefthandslot2;
        public bool lefthandslot3;

        public void AddItem(weaponitem newitem)
        {
            weapon = newitem;
            icon.sprite = newitem.itemsicon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void RemoveItem(weaponitem newitem)
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
