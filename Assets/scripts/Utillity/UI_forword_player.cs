using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace sg
{
    public class UI_forword_player : MonoBehaviour
    {

        public Playercamera player;
        private void Start()
        {
            player = Playercamera.instance;
        }
        private void Update()
        {
            Vector3 rotationDirection = player.transform.position - transform.position;
           
            

            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = new Quaternion(targetRotation.x, targetRotation.y,0, targetRotation.w);
            
        }

    }
}
