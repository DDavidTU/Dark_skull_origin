using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace sg
{

    public class characterlocomotionmanger : MonoBehaviour
    {
        charactermanger  character;

        [Header("Ground Check & Jumping")]
        [SerializeField] protected Vector3 Yvelocity;

        [SerializeField] protected float GroundedYvelocity = -20;
        [SerializeField] protected float fallstartYvelocity = -5;
        [SerializeField]protected float gravityForce = -40;
        
        
        protected bool  fallingVelocityset = false;
        protected float inAirTimer=0;

        
        [SerializeField] LayerMask GroundLayer;
        [SerializeField] float ChecksphereRadius = 0.3f;
        [Header("Flags")]
        public bool isRolling = false;

            
            
            
        protected virtual void Awake( )
        {
            character=GetComponent<charactermanger>();
        }
        protected virtual void Update()
        {
            HandleGroundChecked();

            if(character.isGround)
            {
                if (Yvelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityset = false;
                    Yvelocity.y = GroundedYvelocity;
                }
            }
            else
            {
                if(!character.characternetcodeManager.isjump.Value && !fallingVelocityset)
                {
                    fallingVelocityset=true;
                    Yvelocity.y = fallstartYvelocity ;
                }

                inAirTimer += Time.deltaTime;
                
                character.animator.SetFloat("Airtimer", inAirTimer);

                Yvelocity.y += gravityForce * Time.deltaTime;
                
            }
           
            character.Characactercontroller.Move(Yvelocity * Time.deltaTime);
            
        }

        protected void HandleGroundChecked()
        {
            character.isGround=Physics.CheckSphere(character.transform.position,ChecksphereRadius,GroundLayer);
            
        }
        protected void OnDrawGizmosSelected()
        {
           // Gizmos.color = Color.yellow;
           // Gizmos.DrawWireSphere(character.transform.position, ChecksphereRadius);
        }

        public void EnableCanRotate()
        {
            character.canRotation = true;
        }
        public void DisableCanRotate()
        {
            character.canRotation = false;
        }


    }
}
