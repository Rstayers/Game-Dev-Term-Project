using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening;

interface IInteractable
{
    public void Interact(CharacterAnimatorManager anim);
    bool interacted { get; set; }   
}
public class PlayerInteraction : MonoBehaviour
{
    private CharacterStateManager stateManager;
    [SerializeField] [Range(0, 2f)] private float interactRange = 2f;
    [SerializeField] private Transform interactPosition;
    [SerializeField] private Collider interactionZone;
    [Header("UI Popup")]
    private Collider interactable;
    private CharacterAnimatorManager playerAnimator;
    private Collider currentHit = null;
    private InputPopUp currentPopUp = null;
    [SerializeField] GameObject uiPrompt;
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
            if (interact.interacted) return;
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
    private bool IsInColliders(Collider[] colliders, Collider target)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == target)
                return true;
        }
        return false;
    }
    private Collider GetHit()
    {
        Collider[] hits = Physics.OverlapSphere(interactPosition.position, interactRange, WorldManager.Instance.GetInteractionLayers());

        if (hits.Length == 0)
        {
            if(currentPopUp != null)
            {
                currentPopUp.Dissapear();
                currentPopUp = null;
            }
            if(currentHit != null) 
                currentHit = null;
            return null;
        }
        
        if (currentHit == null)
        {
            currentHit = hits[0];
            Vector3 interactablePosition = currentHit.transform.position;

            Vector3 playerPosition = interactPosition.position;

            Vector3 directionToPlayer = (playerPosition - interactablePosition).normalized * 0.7f;

            // spawn position outside the interactable collider and toward the player
            Vector3 popupSpawnPosition = new Vector3(playerPosition.x, playerPosition.y + 2, playerPosition.z) + -directionToPlayer;

          
            GameObject clone = Instantiate(uiPrompt, popupSpawnPosition, Quaternion.identity);
            currentPopUp = clone.GetComponent<InputPopUp>();
        }

       
        return currentHit;
        
        
    }

}
