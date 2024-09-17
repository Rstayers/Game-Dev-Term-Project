using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action/AI Attack Action")]

public class AIAttackAction : ActionContainer
{
    [Header("Combo Action")]
    public AIAttackAction comboAction;

    [Header("Action Values")]
    public int attackWeight = 50;
    //ATTACK TYPE
    //ATTACK CAN BE REPEATED
    public float actionRecoveryTime = 1.5f;
    public float minimumAttackAngle = -35;
    public float maximumAttackAngle = 35;
    public float minimumAttackDistance = 0;
    public float maximumAttackDistance = 3;
    public void AttemptToPerformAction(AICharacterManager aiCharacterManager)
    {
        aiCharacterManager.animatorManager.PlayTargetAnimation(this, true);
    }
}
