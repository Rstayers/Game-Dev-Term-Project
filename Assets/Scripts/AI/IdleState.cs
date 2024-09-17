using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/States/Idle")]
public class IdleState : AIState
{
    [SerializeField] private float idleCounter;
    private float currentCount = -1;
    public override AIState Tick(AICharacterManager character)
    {
        if(character.aiCombatManager.currentTarget != null)
        {
            //RETURN PURSUE TARGET STATE
            return SwitchState(character, character.pursueTarget);
        }
        else
        {
            character.aiCombatManager.FindATargetViaLineOfSight(character);

            if (currentCount <= 0)
            {
                currentCount = idleCounter;
                return SwitchState(character, character.patrol);
            }
            else
            {
                currentCount -= Time.deltaTime;
                return this;
            }
        }
        
    }

   
}
