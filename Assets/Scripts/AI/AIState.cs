using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIState : ScriptableObject
{
   
    public virtual AIState Tick(AICharacterManager character)
    {
        //DO SOME LOGIC
        return this;
    }
    protected virtual AIState SwitchState(AICharacterManager character, AIState newState)
    {
        ResetStateFlags(character);
        return newState; 
    }
    protected virtual AIState ResetStateFlags(AICharacterManager character)
    {
        return this;
        //reset all state flags
    }
}
