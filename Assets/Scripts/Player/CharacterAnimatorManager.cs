using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterCombatManager characterCombatManager;
    [Header("Movement")]
    private int vertical;
    private int horizontal;
    public void Initialize()
    {
        animator = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
        characterCombatManager = GetComponent<CharacterCombatManager>();
    }

    public void UpdateAnimatorValues(float verticalMovement,  float horizontalMovement)
    {
        /*
         * Update movement values for animator
         */
        if(verticalMovement == 2) verticalMovement = 2;

        else if (verticalMovement > .7f) verticalMovement = 1;
        else if (verticalMovement < -.7f) verticalMovement = -1;

        if (horizontalMovement == 2) horizontalMovement = 2;
        else if (horizontalMovement  > .7f) horizontalMovement = 1;
        else if (horizontalMovement < -.7f) horizontalMovement = -1;

        
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        
    }
    public void EnableCanDoCombo()
    {
        characterCombatManager.canCombo = true;
    }
    public void DisableCanDoCombo()
    {
        characterCombatManager.canCombo = false;
    }
    public void PlayTargetAnimation(ActionContainer target, bool isInteracting, float fade =0.2f)
    {
        
        animator.applyRootMotion = isInteracting;
        characterCombatManager.lastAttackPerformed = target;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(target.animation, fade);
    }
}
