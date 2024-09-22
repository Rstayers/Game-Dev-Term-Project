using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteractable : MonoBehaviour, IInteractable
{
    public ActionContainer pickupAction;
    public void Interact(CharacterAnimatorManager anim)
    {
        anim.PlayTargetAnimation(pickupAction, true);
        
    }
}
