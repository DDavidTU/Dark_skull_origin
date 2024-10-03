using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace sg
{
    public class DoorClose : DoorCheckEnemyOpen
    {
        [SerializeField] Collider check;
        [SerializeField] private GameObject door;
        [SerializeField] private GameObject Fire;
        [SerializeField] GameObject infontofeffect;  
        protected override void OnTriggerEnter(Collider other)
        {
            if(other.tag=="player")
            {

                open = true;
                
                Fire.SetActive(true);
                MagicEffect.SetActive(true);
                infontofeffect.SetActive(true);
            }
            
        }
        protected override void Update()
        {
            if(open)
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, Quaternion.Euler(0,openAngle, 0), Time.deltaTime * 5);
        }
        
    }
}
