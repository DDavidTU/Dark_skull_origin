using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class weaponmodelinitialSlot : MonoBehaviour
    {
        public weaponModelsolt weaponsolt;
        public GameObject currentModelweapon;
        
        

       public void Awake()
        {
            Destroy(currentModelweapon);
        }
        

        public void unLoadWeapon()
        {
            if(currentModelweapon!=null)
            {
                Destroy(currentModelweapon);
            }
        }
        public void LoadWeapon(GameObject weaponModel)
        {
            currentModelweapon = weaponModel;
            weaponModel.transform.parent=transform;
            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale=Vector3.one;

        }
    }
}
