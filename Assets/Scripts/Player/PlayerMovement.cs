using Cinemachine;
using DG.Tweening;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private PlayerSettings settings;
    [HideInInspector] public CharacterStateManager characterStateManager;
    [HideInInspector] public CharacterAnimatorManager animatorManager;
    private Rigidbody rb;
    
    [Header("Camera")]
    private CinemachineVirtualCamera camera;

    [Header("Grounding Info")]
    [SerializeField] private Transform grounding;
    [SerializeField] private float stepRayLength = 0.5f; 

    [Header("Movement")]
    private Vector3 input;
    private Vector3 forceDirection, lookDirection = Vector3.zero;

    [Header("Dodging")]
    private Vector3 rollDirection;
    [SerializeField] private ActionContainer dodgeAction;
    private bool isDodging = false;
    private Vector3 rollStartPosition;
    private Vector3 rollTargetPosition;
    private float rollTime;
    [Header("SFX")]
    [SerializeField] private AudioClip rollSFX;
    

    private void OnEnable ()
    {
        rb = GetComponent<Rigidbody>();
        characterStateManager = GetComponent<CharacterStateManager>();
        animatorManager = GetComponent<CharacterAnimatorManager>();
        camera = CameraManager.Instance.virtualCamera;
        animatorManager.Initialize();
    }

    private void FixedUpdate()
    {
        HandleDodgeTimer();
        if (animatorManager.animator.GetBool("isInteracting")) return;
        HandleMovement();
        HandleClimbing();
        StepClimb(); 
        HandleAnimation();
        HandleFall();
       
    }

    
    #region Dodging
    public void HandleDodge(InputAction.CallbackContext ctx)
    {
        /*
         *  Handle dodge input and perform dodge
         */

        if (!ctx.performed || isDodging || characterStateManager.canInteract) return;

        // Calculate the direction the player will roll
        rollDirection = input.x * Helpers.GetCameraRight(camera.transform) + input.y * Helpers.GetCameraForward(camera.transform);
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
        SFXManager.instance.PlaySFXClip(rollSFX, transform, .2f);

        // Instantiate the particle effect at the given position and with no rotation
        GameObject particleInstance = Instantiate(settings.dodgeVFX, transform.position, Quaternion.identity);

        // Get the VFX and play
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        Destroy(particleInstance, particleSystem.main.duration);
     
    }
    private void HandleDodgeTimer()
    {
        /*
         * Moves the player when dodging
         * Ensure player stays on the ground
         */

        if (!isDodging)
            return;

        rollTime += Time.deltaTime;

        if (IsGroundAhead(rollDirection, settings.cliffDetectionDistance))
        {
            // target position for the dodge
            Vector3 movement = rollDirection * (settings.dodgeDistance / settings.dodgeDuration) * Time.deltaTime;
            Vector3 targetPosition = rb.position + movement;

            // Raycast downward adjust y for ground contact
            RaycastHit hit;
            Vector3 rayOrigin = targetPosition + Vector3.up; 
            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
            {
                targetPosition.y = hit.point.y;
            }

            rb.MovePosition(targetPosition);

        }

        if (rollTime >= settings.dodgeDuration)
        {
            isDodging = false;
        }
    }
    #endregion

    #region Rotation and Movement
    public void MoveInput(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        /*
         *  Handle Sprint input
         */
        if (ctx.canceled)
            characterStateManager.isSprinting = false;
        if (!ctx.performed) return;

        else
            characterStateManager.isSprinting = true;
    }
    private void HandleRotation()
    {

        /*
         * Update player rotation based on whether we are locked onto a target
         */
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
            lookDirection += input.x * Helpers.GetCameraRight(camera.transform);
            lookDirection += input.y * Helpers.GetCameraForward(camera.transform);


            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * settings.rotationSpeed);

            lookDirection = Vector3.zero;
        }
    }
    private void HandleClimbing()
    {
        if (!characterStateManager.isClimbing) return;
        Vector3 moveDirection = new Vector3(0, input.y, 0);

        // Normalize the moveDirection to get a unit vector
        moveDirection = moveDirection.normalized;

        rb.AddForce(moveDirection, ForceMode.VelocityChange);

        // Reset the force direction
        forceDirection = Vector3.zero;

    }
   private void HandleMovement()
    {
        
        if (characterStateManager.isClimbing) return;

        // Calculate the direction the player is trying to move
        Vector3 moveDirection = input.x * Helpers.GetCameraRight(camera.transform) + input.y * Helpers.GetCameraForward(camera.transform);
 
        // Normalize the moveDirection to get a unit vector
        moveDirection = moveDirection.normalized;
        if(moveDirection == Vector3.zero && characterStateManager.isSprinting)
            characterStateManager.isSprinting = false;

        // Perform a raycast in the move direction to check for ground or acceptable slope
        if (IsGroundAhead(moveDirection, settings.cliffDetectionDistance))
        {
            // If ground or slope is ahead, apply movement force
            if(characterStateManager.isSprinting && !characterStateManager.isLockedOn)
                forceDirection += moveDirection * settings.sprintSpeed;
            else
                forceDirection += moveDirection * settings.speed;

        }
        else
        {
            //if we are going to go off a ledge, only use the move component that keeps us on the ground
            Vector3 parallel = GetNewMoveDirection(moveDirection);
            if (characterStateManager.isSprinting && !characterStateManager.isLockedOn)
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
    private void HandleFall()
    {
        if (characterStateManager.isClimbing) return;
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
    #endregion

    #region Animation
    private void HandleAnimation()
    {
        /*
         *  Update animator values depending on sprint status and lock on
         */
        Vector2 normalInput = input.normalized;

        if (characterStateManager.isSprinting && !characterStateManager.isLockedOn)
            animatorManager.UpdateAnimatorValues(2, 2);
     
        else if (!characterStateManager.isLockedOn)
            animatorManager.UpdateAnimatorValues(Mathf.Abs(normalInput.x), Mathf.Abs(normalInput.y));
        else if (characterStateManager.isLockedOn)
            animatorManager.UpdateAnimatorValues(-Mathf.Abs(normalInput.x), -Mathf.Abs(normalInput.y));
       

    }

   
    #endregion

    #region Helpers
    private bool IsGrounded()
    {
        /*
         *  Check if player is on the ground with a capsule check
         */
        float capsuleRadius = 0.5f;
        float capsuleHeight = 3.0f;
        Vector3 capsuleBottom = grounding.position + new Vector3(0, .02f, 0) * capsuleRadius;
        Vector3 capsuleTop = grounding.position + Vector3.up * (capsuleHeight - capsuleRadius);

        bool grounded = Physics.CheckCapsule(capsuleBottom, capsuleTop, capsuleRadius, WorldManager.Instance.GetGroundLayer());
        return grounded;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(grounding.position, grounding.position + Vector3.down * settings.maxHeightThreshold);
    }
    private Vector3 GetNewMoveDirection(Vector3 moveDirection)
    {
        RaycastHit hit;

        //make two casts in the players x and z directions to see which component we can keep
        Vector3 xDir = new Vector3(moveDirection.x, 0, 0) * settings.cliffDetectionDistance * 1.2f + grounding.position + Vector3.up;
        Vector3 zDir = new Vector3(0, 0, moveDirection.z) * settings.cliffDetectionDistance * 1.2f + grounding.position + Vector3.up;
    
        //if the x component will take us off the edge eliminate it
        if (!Physics.Raycast(xDir, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
        {

            moveDirection = new Vector3(0, moveDirection.y, moveDirection.z);
        }
        //if the z component will take us off the edge eliminate it
        if (!Physics.Raycast(zDir, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
        {
            moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0);
        }
       
        return moveDirection;
    }
    private bool IsGroundAhead(Vector3 moveDirection, float detection)
    {
        // Start ray in front of the player
        Vector3 rayOrigin = grounding.position + Vector3.up + moveDirection  * detection;

        // Perform raycast to check for ground
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, settings.maxHeightThreshold, WorldManager.Instance.GetGroundLayer()))
        {
            // Check angle of the ground 
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);


            // If the slope angle is less than the slope, it's not a cliff
            if (slopeAngle <= settings.maxSlopeAngle)
            {
                return true;  
            }
        }

        return false;  
    }

    private IEnumerator InvincibilityFrames(float frames)
    {
        characterStateManager.isInvincible = true;

       
       yield return new WaitForSeconds(frames); 
        

        characterStateManager.isInvincible = false;
    }
    #endregion
}
