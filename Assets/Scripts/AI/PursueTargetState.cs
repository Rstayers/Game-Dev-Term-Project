using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Pursue Target")]
public class PursueTargetState : AIState
{
    public override AIState Tick(AICharacterManager character)
    {
        
        //Check if we are performing an action
        if (character.isPerformingAction)
            return this;

        //Check if our target is null, go to idle
        if(character.aiCombatManager.currentTarget == null)
            return this;
        //Make sure our navmeshagent is active
        if(!character.navMeshAgent.enabled)
            character.navMeshAgent.enabled = true;
        if (character.aiCombatManager.viewableAngle < character.aiCombatManager.minimumFOV
            || character.aiCombatManager.viewableAngle < character.aiCombatManager.maximumFOV)
            character.aiCombatManager.PivotTowardsTarget(character);
        character.locomotionManager.RotateTowardsAgent(character);
        //If we are within combat range, switch state
        if(character.locomotionManager.distanceFromTarget <= character.navMeshAgent.stoppingDistance && character.locomotionManager.distanceFromTarget > 0)
            return SwitchState(character, character.attack);
        //If target is not reachable, return home

        //Pursue target
        NavMeshPath navMeshPath = new NavMeshPath();
        character.navMeshAgent.CalculatePath(character.aiCombatManager.currentTarget.transform.position, navMeshPath);
        character.navMeshAgent.SetPath(navMeshPath);
        return this;
    }
}
