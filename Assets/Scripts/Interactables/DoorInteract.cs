using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public ActionContainer doorOpenAction;
    private bool _interacted;

    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }
    public void Interact(CharacterAnimatorManager anim)
    {
        anim.PlayTargetAnimation(doorOpenAction, true);
        interacted = true;
    }
}
