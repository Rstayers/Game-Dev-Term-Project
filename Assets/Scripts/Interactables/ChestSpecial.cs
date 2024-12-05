using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpecial : MonoBehaviour, IInteractable
{
    public ActionContainer chestOpenedAction;
    [SerializeField] private Transform lidPivot;

    [Header("Fine tuning stats")]
    [SerializeField] private int chestAngle;
    [Range(0f, 1f)]
    [SerializeField] private float timeToOpen;

    private bool _interacted;

    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }
    // Start is called before the first frame update
    public void Interact(CharacterAnimatorManager anim)
    {
        Vector3 angle = new Vector3(-chestAngle, lidPivot.eulerAngles.y, lidPivot.eulerAngles.z);
        lidPivot.DORotate(angle, timeToOpen).SetEase(Ease.Linear).OnComplete(()=> {
            anim.PlayTargetAnimation(chestOpenedAction, true);
            anim.characterCombatManager.TurnPlayerToCamera();
        });

    }
}
