using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

interface IInteractable
{
    public void Interact(CharacterAnimatorManager anim);
}
public class PlayerInteraction : MonoBehaviour
{
    private CharacterStateManager stateManager;
    [SerializeField] [Range(0, 2f)] private float interactRange = 2f;
    [SerializeField] private Transform interactPosition;
    [SerializeField] private Collider interactionZone;

    private Collider interactable;
    private CharacterAnimatorManager playerAnimator;
    private void Awake()
    {
        playerAnimator = GetComponent<CharacterAnimatorManager>();
        stateManager = GetComponent<CharacterStateManager>();
    }
    private void Update()
    {
        interactable = GetHit();
        if (!interactable)
        {
            stateManager.canInteract = false;
            return;
        }

        //hit something, tell player
        stateManager.canInteract = true;
    }

    private void ExecuteInteract(GameObject hit)
    {
        if (hit.TryGetComponent(out IInteractable interact))
        {
            
            interact.Interact(playerAnimator);
        }
        
    }

    //called when button is hit
    public void Interact(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Collider hit = GetHit();
    
        if(hit != null)
        {
            ExecuteInteract(hit.gameObject);
        }
    }

    private Collider GetHit()
    {
        Collider[] hits = Physics.OverlapSphere(interactPosition.position, interactRange, WorldManager.Instance.GetInteractionLayers());

        if (hits.Length == 0 )
            return null;
        return hits[0];
  
        
    }

}
