using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace sg
{
    public class DoorCheckEnemyOpen : MonoBehaviour
    {
        [SerializeField] public bool open = false;
        [SerializeField] public Collider Opencheck;
        [SerializeField] public Transform OpenRotation;
        [SerializeField] public GameObject MagicEffect;
        [SerializeField] public float openAngle;

        public AI_characterManager enemy;
        [SerializeField]  public List<AI_characterManager> listenemy;
        [SerializeField] public int enemyamount;


        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag == "enemy1")
            {
                enemy = other.GetComponentInParent<AI_characterManager>();
                listenemy.Add(enemy);



            }
            
            if(other.tag == "player")
            {
                MagicEffect.SetActive(true);
            }
           

        }
        protected virtual void Update()
        {

           
            enemyamount = listenemy.Count;

            foreach (var enemy in listenemy)
            {
                if (enemy == null)
                {
                    enemyamount -= 1;
                }
                    }


            if(enemyamount == 0)
            {
                open = true;
                MagicEffect.SetActive(false);
            }
            else
            {
                open = false;
            }
            if(open)
            {
                OpenRotation.rotation = Quaternion.Slerp(OpenRotation.rotation, Quaternion.Euler(0, -openAngle, 0), Time.deltaTime * 5);
             
            }
          
        }

    }
}
