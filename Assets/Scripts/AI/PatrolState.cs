using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Patrol")]

public class PatrolState : AIState
{
    
    public override AIState Tick(AICharacterManager character)
    {
        if (character.aiCombatManager.currentTarget != null)
        {
            //RETURN PURSUE TARGET STATE
            return SwitchState(character, character.pursueTarget);
        }
        if (character.locomotionManager.isPatrolling && character.locomotionManager.distanceToPoint <= character.navMeshAgent.stoppingDistance)
        {
            character.locomotionManager.isPatrolling = false;
            character.isMoving = false;
            return SwitchState(character, character.idle);
            
        }
        if (!character.locomotionManager.isPatrolling)
            character.locomotionManager.Patrol(character);
        character.locomotionManager.UpdatePatrol(character);
        return this;
    }
}
