using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpJournalEntry : MonoBehaviour, IInteractable
{
    public ActionContainer pickupAction;
    private JournalManager journalManager;
    public JournalPage journalPage;

    public bool isWeapon;
    private bool _interacted;

    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }
    private void Awake()
    {
        journalManager = FindObjectOfType<JournalManager>();
    }
    public void Interact(CharacterAnimatorManager anim)
    {
        if (isWeapon)
        {
            anim.characterCombatManager.stateManager.playerState.hasWeapon = true;
            anim.characterCombatManager.stateManager.GiveWeapon();
        }
        anim.PlayTargetAnimation(pickupAction, true);
        journalManager.DiscoverPage(journalPage);
        Destroy(gameObject);
    }
}
