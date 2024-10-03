using sg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class DoorCheck_Close_and_Open : DoorCheckEnemyOpen
    {

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == "enemy1")
            {
                enemy = other.GetComponentInParent<Zombie_Manager>();
                listenemy.Add(enemy);



            }



        }
    }
}
