using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    CharacterStateManager stateManager;
    public string targetBool;
    public bool status; 
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateManager ==  null)
        {
            stateManager = animator.GetComponent<CharacterStateManager>();
        }
        stateManager.isPerformingAction = false;
        stateManager.combatManager.canCombo = false;
        animator.SetBool(targetBool, status);
    }
   

}
