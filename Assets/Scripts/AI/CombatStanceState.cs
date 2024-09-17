using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Combat/Stance")]

public class CombatStanceState : AIState
{
    //1. Select an attack for the attack state
    //2. process any combat logic while waiting for attack
    //3. If target moves out of combat range, go to persue state
    [Header("Attacks")]
    public List<AIAttackAction> attacks;
    private List<AIAttackAction> potentialAttacks;
    private AIAttackAction previousAttack;
    private AIAttackAction chosenAttack;
    protected bool hasAttack = false;
    [Header("Combos")]
    [SerializeField] protected bool canPerformCombo = false;
    [SerializeField] protected int chanceToPerformCombo = 25;
     protected bool hasRolledForCombo = false;

    [Header("Engagement Distance")]
    [SerializeField] protected float maximumEngagementDistance = 5f;

    public override AIState Tick(AICharacterManager character)
    {
        if (character.isPerformingAction)
            return this;
        if (!character.navMeshAgent.enabled)
            character.navMeshAgent.enabled = true;

        if(!character.isMoving)
        {
            if (character.aiCombatManager.viewableAngle < -30 || character.aiCombatManager.viewableAngle > 30)
                character.aiCombatManager.PivotTowardsTarget(character);

        }
        //Rotate to face target 
        character.aiCombatManager.RotateTowardsAgent(character);
        //If our target is no longer there, go back to idle
        if (character.aiCombatManager.currentTarget == null)
            return SwitchState(character, character.idle);
        if(!hasAttack)
        {
            GetNewAttack(character);
        }
        else
        {
            character.attack.currentAttack = chosenAttack;
            //Check recovery timer
            //pass attack to attack state
            //roll for combo
            //switch state
            return SwitchState(character, character.attack);
        }
        //If player leaves engagement distance
        if(character.aiCombatManager.distanceFromTarget > maximumEngagementDistance)
            return SwitchState(character, character.pursueTarget);


        NavMeshPath navMeshPath = new NavMeshPath();
        character.navMeshAgent.CalculatePath(character.aiCombatManager.currentTarget.transform.position, navMeshPath);
        character.navMeshAgent.SetPath(navMeshPath);
        return this;
    }

    protected virtual void GetNewAttack(AICharacterManager character)
    {
        potentialAttacks = new List<AIAttackAction>();

        //1. Sort through possible attacks
        foreach (AIAttackAction action in attacks)
        {
            //2. Remove Attacks that cannot be performed
            //if we are too close or too far, disregard potential attack option
            if (action.minimumAttackDistance > character.aiCombatManager.distanceFromTarget)
                continue;
            if (action.maximumAttackDistance < character.aiCombatManager.distanceFromTarget)
                continue;
            //if we are outside FOV, disregard potential attack option

            if (action.minimumAttackAngle > character.aiCombatManager.viewableAngle)
                continue;
            if (action.maximumAttackAngle < character.aiCombatManager.viewableAngle)
                continue;

            potentialAttacks.Add(action);

        }
        if (potentialAttacks.Count <= 0)
            return;

        //3. Pick an attack 
        var totalWeight = 0;
        foreach(var attack in potentialAttacks)
        {
            totalWeight += attack.attackWeight;
        }
        var randomWeightValue = Random.Range(1, totalWeight + 1);
        var processedWeight = 0;
        foreach (var attack in potentialAttacks)
        {
            processedWeight += attack.attackWeight;

            if(randomWeightValue <= processedWeight)
            {
                //This is our attack
                chosenAttack = attack;
                previousAttack = chosenAttack;
                hasAttack = true;
                return;
            }
        }
            //4. Pass attack to attack state
        }
    protected virtual bool RollForOutcomeChance(int outcomeChance)
    {
        bool outcomeWillBePerformed = false;

        int randomPercentage = Random.Range(0, 100);
        if(randomPercentage < outcomeChance)
        {
            outcomeWillBePerformed = true;
        }
        return outcomeWillBePerformed;
    }
    protected override AIState ResetStateFlags(AICharacterManager character)
    {
        hasRolledForCombo = false;
        hasAttack = false;
        return this;
    }
}
