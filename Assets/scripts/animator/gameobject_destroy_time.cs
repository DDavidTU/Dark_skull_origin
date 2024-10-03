using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class gameobject_destroy_time : MonoBehaviour
    {
        [SerializeField] float TIme_Deatroy=5;

        private void Awake()
        {
            Destroy(gameObject,TIme_Deatroy);
        }
    }
}
