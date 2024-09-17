using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAction : ActionContainer
{
    public float manaRequirement;
    public GameObject projectile;
    public void AttemptToPerformAction(CharacterStateManager manager)
    {
        if (manaRequirement >= manager.currentMana)
        {
            //perform action
            manager.animatorManager.PlayTargetAnimation(this, true);
        }
        
    }
}
