using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Combat/Melee Attack")]

public class AttackState : AIState
{
    [Header("Current Attack")]
    [HideInInspector] public AIAttackAction currentAttack;
    [HideInInspector] public bool willPerformCombo = false;

    [Header("State Flags")]
    [SerializeField] bool pivotAfterAttack = false;
    protected bool hasPerformedAttack = false;
    protected bool hasPerformedCombo = false;
    public override AIState Tick(AICharacterManager character)
    {
        if (character.aiCombatManager.currentTarget == null)
            return SwitchState(character, character.idle);


        if (character.aiCombatManager.currentTarget == null)
        {
            return SwitchState(character, character.idle);
        }
        
        
        //rotate while attacking

        //set movement values to 0
        //Perform a combo
        if(willPerformCombo && !hasPerformedCombo)
        {
            if(currentAttack.comboAction != null)
            {
               // hasPerformedCombo = true;
               // currentAttack.comboAction.AttemptToPerformAction(character);
            }
        }
        //IF we are still recovering , wait
        if(!hasPerformedAttack)
        {
            if (character.aiCombatManager.actionRecoveryTimer > 0)
                return this;
            if (character.isPerformingAction)
                return this;

            PerformAttack(character);
            return this;
        }
        if (pivotAfterAttack)
            character.aiCombatManager.PivotTowardsTarget(character);

        return SwitchState(character, character.combatStance);
        
        
    }

    protected void PerformAttack(AICharacterManager character)
    {
       
        hasPerformedAttack = true;
        currentAttack.AttemptToPerformAction(character);
        character.aiCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
    }
    protected override AIState ResetStateFlags(AICharacterManager character)
    {
        hasPerformedAttack = false;
        hasPerformedCombo = false;
        return this;
    }
}
