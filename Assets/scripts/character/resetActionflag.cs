using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg{
public class resetActionflag : StateMachineBehaviour
{
        charactermanger character;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (character == null)
            {
                character=animator.GetComponent<charactermanger>();
            }
            character.isPerformingAction = false;
            character.applyRootMotion = false;
            character.canMove = true;
            character.canRotation = true;
            character.characterlocomotionmanger.isRolling = false;
            
            character.characterAnimatorforManager.DisablecanCombo();
            if (character.IsOwner)
            {
                character.characternetcodeManager.isjump.Value = false;
                character.characternetcodeManager.isInvulnerable.Value = false;
            }
            
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
}