using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace sg
{
    public class playerUIHUBManager : MonoBehaviour
    {
        [Header("Status_bar")]
        [SerializeField] UI_status_Bar HPbar;
        [SerializeField] UI_status_Bar staminabar;
        [Header("Quick Slots")]
        [SerializeField] public Image RightWeapon_icon;
        [SerializeField] public Image LeftWeapon_icon;
        [SerializeField] public TextMeshProUGUI TopAmount;

        [Header("Boss Healeh Bar")]
        public  Transform bossHealthBarParent;
        public GameObject bossHealthBarObject;


      

        public void SetnewHPValue(int oldHpvalue, int newHpvalue)
        {
            HPbar.Setstatus(newHpvalue);
        }
        public void SetmaxHPValue(int maxHpvalue)
        {
            HPbar.SetMaxStaus(maxHpvalue);
        }
        
        public void SetnewstaminaValue(float oldvalue,float newvalue) 
        {
            staminabar.Setstatus(Mathf.RoundToInt(newvalue));
        }
        public void SetmaxstaminaValue(int maxvalue)
        {
            staminabar.SetMaxStaus(maxvalue);
        }

        public void RefreshHUB()
        {
            HPbar.gameObject.SetActive(false);
            HPbar.gameObject.SetActive(true);
            staminabar.gameObject.SetActive(false);
            staminabar.gameObject.SetActive(true);
        }

        public void SetRightWeaponQuickSlotsIcon(int weaponID)
        {
            weaponitem weapon = worldItemDatabase.Instance.GetWeaponByID(weaponID);
            if (weapon == null)
            {
                Debug.Log("item is null");
                RightWeapon_icon.enabled = false;
                RightWeapon_icon.sprite = null;
                return;
            }
            if (weapon.itemsicon == null)
            {
                Debug.Log("item No icon");
                RightWeapon_icon.enabled = false;
                RightWeapon_icon.sprite = null;
                return;
            }
            RightWeapon_icon.sprite = weapon.itemsicon;
            RightWeapon_icon.enabled = true;
            

        }
        public void SetLeftWeaponQuickSlotsIcon(int weaponID)
        {
            weaponitem weapon = worldItemDatabase.Instance.GetWeaponByID(weaponID);
            if (weapon == null)
            {
                Debug.Log("item is null");
                LeftWeapon_icon.enabled = false;
                LeftWeapon_icon.sprite = null;
                return;
            }
            if (weapon.itemsicon == null)
            {
                Debug.Log("item No icon");
                LeftWeapon_icon.enabled = false;
                LeftWeapon_icon.sprite = null;
                return;
            }
            LeftWeapon_icon.sprite = weapon.itemsicon;
            LeftWeapon_icon.enabled = true;


        }

        
    }
}
