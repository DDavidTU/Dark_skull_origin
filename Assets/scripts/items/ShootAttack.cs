using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class ShootAttack : MonoBehaviour
    {
        [SerializeField] float Shootspeed = 30;

         void Start()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * Shootspeed;
            
           Destroy(gameObject,5);
        }
    }
}
