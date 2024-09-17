using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    [HideInInspector] public CharacterStateManager characterStateManager;
    [SerializeField] private Transform grounding;
    [SerializeField] private float stepRayLength = 0.5f; // How far to check in front of the player
    [SerializeField] private ActionContainer dodgeAction;
    private Rigidbody rb;
    private Camera camera;
    
    private Vector3 input;
    private Vector3 forceDirection, lookDirection = Vector3.zero;
    private Vector3 rollDirection;
    private bool isDodging = false;

    private Vector3 rollStartPosition;
    private Vector3 rollTargetPosition;
    private float rollTime;

    //Animation 
    [HideInInspector] public CharacterAnimatorManager animatorManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterStateManager = GetComponent<CharacterStateManager>();
        animatorManager = GetComponent<CharacterAnimatorManager>();
        camera = Camera.main;
        animatorManager.Initialize();
    }

    private void FixedUpdate()
    {
        HandleDodgeTimer();
        if (animatorManager.animator.GetBool("isInteracting")) return;
        
        HandleMovement();
        StepClimb(); 
        HandleAnimation();
        HandleFall();
       
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
      
    }
    
    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
            characterStateManager.isSprinting = false;
        if (!ctx.performed) return;
  
        else
            characterStateManager.isSprinting = true;
    }
    public void HandleDodge(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || isDodging || characterStateManager.canInteract) return;

        // Calculate the direction the player will roll
        rollDirection = input.x * Helpers.GetCameraRight(camera) + input.y * Helpers.GetCameraForward(camera);
        rollDirection = rollDirection.normalized;
        if (rollDirection == Vector3.zero)
            rollDirection = transform.forward;

        // Calculate roll target position
        rollStartPosition = transform.position;
        rollTargetPosition = rollStartPosition + rollDirection * settings.dodgeDistance;
        Quaternion targetRotation = Quaternion.LookRotation(rollDirection, Vector3.up);
        transform.rotation = targetRotation;
        isDodging = true;
        StartCoroutine(InvincibilityFrames(settings.invincibilityFrames));
        rollTime = 0;
        //animate and VFX
        animatorManager.PlayTargetAnimation(dodgeAction, true, 0);
        // Instantiate the particle effect at the given position and with no rotation
        GameObject particleInstance = Instantiate(settings.dodgeVFX, transform.position, Quaternion.identity);

        // Get the ParticleSystem component
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();

        // Play the particle effect
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // Optionally, destroy the particle after it's done playing
        Destroy(particleInstance, particleSystem.main.duration);
     
    }
    private void HandleDodgeTimer()
    {
        if (!isDodging)
            return;

        rollTime += Time.deltaTime;

        RaycastHit hit;
        Vector3 dodgeDirection = rollDirection.normalized;
        float dodgeDistanceCovered = Vector3.Distance(rollStartPosition, Vector3.Lerp(rollStartPosition, rollTargetPosition, rollTime / settings.dodgeDuration));

        // Check for obstacles in the dodge direction using a raycast
        if (Physics.Raycast(transform.position, dodgeDirection, out hit, dodgeDistanceCovered, WorldManager.Instance.GetGroundLayer()))
        {
            rollTargetPosition = hit.point;
        }

        // Move the player 
        if (IsGroundAhead(rollDirection, settings.cliffDetectionDistance))
        {
            Vector3 targetPosition = Vector3.Lerp(rollStartPosition, rollTargetPosition, rollTime / settings.dodgeDuration);
            rb.MovePosition(targetPosition);  
        }

        if (rollTime >= settings.dodgeDuration)
        {
            isDodging = false;
        }
    }

    private void HandleRotation()
    {
        if (characterStateManager.isLockedOn && characterStateManager.combatManager.currentTarget != null)
        {
            lookDirection = characterStateManager.combatManager.currentTarget.combatManager.lockOnTransform.position - transform.position;
            lookDirection.y = 0;    
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * settings.rotationSpeed);

            lookDirection = Vector3.zero;
        }
        else
        {
            if (input == Vector3.zero) { lookDirection = Vector3.zero; return; }
            lookDirection += input.x * Helpers.GetCameraRight(camera);
            lookDirection += input.y * Helpers.GetCameraForward(camera);


            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * settings.rotationSpeed);

            lookDirection = Vector3.zero;
        }
    }

   private void HandleMovement()
    {

        // Calculate the direction the player is trying to move
        Vector3 moveDirection = input.x * Helpers.GetCameraRight(camera) + input.y * Helpers.GetCameraForward(camera);
 
        // Normalize the moveDirection to get a unit vector
        moveDirection = moveDirection.normalized;
        if(moveDirection == Vector3.zero && characterStateManager.isSprinting)
            characterStateManager.isSprinting = false;
        // Perform a raycast in the move direction to check for ground or acceptable slope
        if (IsGroundAhead(moveDirection, settings.cliffDetectionDistance))
        {
            // If ground or slope is ahead, apply movement force
            
            if(characterStateManager.isSprinting)
                forceDirection += moveDirection * settings.sprintSpeed;
            else
                forceDirection += moveDirection * settings.speed;

        }
        else
        {
            //Debug.Log(transform.localPosition);

            Vector3 parallel = GetNewMoveDirection(moveDirection);
            if (characterStateManager.isSprinting)
                forceDirection += parallel * settings.sprintSpeed;
            else
                forceDirection += parallel * settings.speed;
        }

       
        rb.AddForce(forceDirection, ForceMode.Impulse);


        // Reset the force direction
        forceDirection = Vector3.zero;

        // Handle the rotation of the player to face the movement direction
        HandleRotation();
    }

    private void HandleAnimation()
    {
        Vector2 normalInput = input.normalized;
        if (characterStateManager.isSprinting)
            animatorManager.UpdateAnimatorValues(2, 2);

        else if (!characterStateManager.isLockedOn)
            animatorManager.UpdateAnimatorValues(Mathf.Abs(normalInput.x), Mathf.Abs(normalInput.y));
        else if (characterStateManager.isLockedOn)
            animatorManager.UpdateAnimatorValues(-Mathf.Abs(normalInput.x), -Mathf.Abs(normalInput.y));

    }
    private void HandleFall()
    {
        
        if (IsGrounded())
        {
            // Apply a small downward force to keep the player grounded on slopes
            rb.AddForce(Vector3.down * settings.groundStickForce, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(Vector3.down * settings.gravityMultiplier, ForceMode.Impulse);
        }


    }
    private bool IsGrounded()
    {
        // Define the radius and height of the capsule
        float capsuleRadius = 0.5f;  // Adjust based on your character's size
        float capsuleHeight = 3.0f;  // Adjust based on your character's height
        Vector3 capsuleBottom = grounding.position + new Vector3(0, .02f, 0) * capsuleRadius;
        Vector3 capsuleTop = grounding.position + Vector3.up * (capsuleHeight - capsuleRadius);

        // Perform the capsule cast
        bool grounded = Physics.CheckCapsule(capsuleBottom, capsuleTop, capsuleRadius, WorldManager.Instance.GetGroundLayer());
        return grounded;
    }
    private void StepClimb()
    {
        // Raycast slightly in front of the player
        RaycastHit hitLower;
        Vector3 rayOriginLower = transform.position + Vector3.up * 0.1f; // Slightly above ground
        if (Physics.Raycast(rayOriginLower, transform.forward, out hitLower, stepRayLength, WorldManager.Instance.GetGroundLayer()))
        {
            float slopeAngle = Vector3.Angle(hitLower.normal, Vector3.up);
            if (slopeAngle >= settings.maxSlopeAngle)
            {
                Vector3 rayOriginUpper = transform.position + Vector3.up * settings.stepHeight; // Higher up for the step
                if (!Physics.Raycast(rayOriginUpper, transform.forward, stepRayLength)) // No obstacle above
                {
                    Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + settings.stepHeight, transform.position.z);
                    rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, .2f));
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Draw a ray in the Scene view to visualize the raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(grounding.position, grounding.position + Vector3.down * settings.maxHeightThreshold);
    }
    private Vector3 GetNewMoveDirection(Vector3 moveDirection)
    {
        RaycastHit hit;
        //make two Raycasts in the players x and z directions to see which component we can keep
        Vector3 xDir = new Vector3(moveDirection.x, 0, 0) * settings.cliffDetectionDistance * 1.2f + grounding.position + Vector3.up;
        Vector3 zDir = new Vector3(0, 0, moveDirection.z) * settings.cliffDetectionDistance * 1.2f + grounding.position + Vector3.up;
    
        //if the x component will take us off the edge, eliminate it
        if (!Physics.Raycast(xDir, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
        {

            moveDirection = new Vector3(0, moveDirection.y, moveDirection.z);
        }
        //if the z component will take us off the edge, eliminate it
        if (!Physics.Raycast(zDir, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
        {
            moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0);
        }
       
        return moveDirection;
    }
    private bool IsGroundAhead(Vector3 moveDirection, float detection)
    {
        // Start the ray from a position slightly in front of the player's feet
        Vector3 rayOrigin = grounding.position + Vector3.up + moveDirection  * detection;

        // Perform a raycast downwards to check for ground
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
        {
            // Check the angle of the ground hit by the raycast
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);


            // If the slope angle is less than the maximum allowable slope, it's not a cliff
            if (slopeAngle <= settings.maxSlopeAngle)
            {
                return true;  // Ground detected and it's a walkable slope
            }
        }

        return false;  // No ground detected or it's a steep cliff
    }

    private IEnumerator InvincibilityFrames(float frames)
    {
        characterStateManager.isInvincible = true;

       
       yield return new WaitForSeconds(frames); // Wait for one frame
        

        characterStateManager.isInvincible = false;
    }
}
