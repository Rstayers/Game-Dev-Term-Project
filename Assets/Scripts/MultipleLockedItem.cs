using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLockedItem : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    [SerializeField] List<HitTriggerRecipient> triggers = new List<HitTriggerRecipient>();
    [SerializeField] List<Door> doors = new List<Door>();
    public ActionContainer doorOpenAction;
    private bool _interacted;

    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }
    public void Interact(CharacterAnimatorManager anim)
    {
        Debug.Log("interacted");
        anim.PlayTargetAnimation(doorOpenAction, true);
        foreach (var trigger in triggers)
        {
            if (!trigger.triggered)
                return;
        }
        foreach(var door in doors)
        {
            door.Open();
        }
    }
}
