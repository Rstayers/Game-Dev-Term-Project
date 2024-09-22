using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public ActionContainer doorOpenAction;
    public void Interact(CharacterAnimatorManager anim)
    {
        anim.PlayTargetAnimation(doorOpenAction, true);
        
    }
}
