using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class AI_characterAnimatorForManager : CharacterAnimatorforManager
    {
        AI_characterManager aI_Character;
       PlayerManager player;

        [Header("Doorseal")]
        [SerializeField] GameObject DoorSealEffect;
        protected override void Awake()
        {
            base.Awake();
            aI_Character = GetComponent<AI_characterManager>();
            player=FindObjectOfType<PlayerManager>();
            
        }
        // Start is called before the first frame update
        private void OnAnimatorMove()
        {
            if (aI_Character.IsOwner)
            {
                if (!aI_Character.isGround)
                    return;

                Vector3 velocity = aI_Character.animator.deltaPosition;

                aI_Character.aI_CharacterController.Move(velocity);
                aI_Character.transform.rotation *= aI_Character.animator.deltaRotation;


            }
            else
            {
                if (!aI_Character.isGround)
                    return;

                Vector3 velocity = aI_Character.animator.deltaPosition;

                aI_Character.aI_CharacterController.Move(velocity);
                aI_Character.transform.position = Vector3.SmoothDamp(aI_Character.transform.position, aI_Character.characternetcodeManager.networkposition.Value,
                    ref aI_Character.characternetcodeManager.networkpositionVelocity, aI_Character.characternetcodeManager.networkrotationTime);

                aI_Character.transform.rotation *= aI_Character.animator.deltaRotation;
            }
        }
        protected override void Update()
        {
            base.Update();
            if (player.IsDead.Value)
                aI_Character.animator.SetBool("playerDead", true);
            
        }
        public void BiteBodySound()
        {
            aI_Character.charactersoundFXsource.playZombieBiteSound();
        }
        public void Opendoorseal()
        {
            Destroy(DoorSealEffect);
        }


    }
}
