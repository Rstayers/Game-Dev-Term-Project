using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CharacterCombatManager : MonoBehaviour
{
    [Header("Managers")]
    [HideInInspector] public CharacterAnimatorManager animatorManager;
    [HideInInspector] public CameraManager cameraManager;
    [HideInInspector] public CharacterStateManager stateManager;

    [Header("Target Info")]
    public CharacterStateManager currentTarget = null;

    [Header("Lock On Info")]
    private Vector3 lockOnSelectionInput;
    public Transform lockOnTransform;
    private bool canChangeSelection = true;

    [Header("Melee Combat")]
    public ActionContainer meleeAttackAction01;
    public ActionContainer meleeAttackAction02;
    public GameObject melee01AttackVFX;
    public float knockbackForce;
    [HideInInspector] public ActionContainer lastAttackPerformed;
    public Transform meleeAttackPoint;
    public float meleeAttackRadius;

    [Header("Ranged Combat")]

    [Header("General Combat")]
    public int waitFrames = 2;

    [Header("Flags")]
    public bool canCombo = false;
    public bool inFiringMode = false;
    [Header("SFX")]
    [SerializeField] AudioClip attack01SFX;
    [SerializeField] AudioClip attack02SFX;
    [SerializeField] AudioClip enemyHitSFX;
    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        animatorManager = GetComponent<CharacterAnimatorManager>();
        stateManager = GetComponent<CharacterStateManager>();
    }
    private void Update()
    {
        if (stateManager.isLockedOn)
        {
            HandleLockOn();
        }
    }

    #region Magic Attack
    public void FiringModeInput(InputAction.CallbackContext ctx)

    {
        if (ctx.canceled)
            inFiringMode = false;
        if (ctx.performed)
            inFiringMode = true;
    }
    public void FiringInput(InputAction.CallbackContext ctx)
    {
        if (!inFiringMode || ctx.canceled || !stateManager.playerState.hasWeapon) return;

      
        FireSpell(ctx.ReadValue<Vector2>());
    }

    public void FireSpell(Vector2 spell)
    {
        //check what slot we are trying to fire
        //check if there is a spell in the selected slot
        //perform the attack if it exists
    }
    #endregion

    #region Melee Attack
    public void HandleMelee(InputAction.CallbackContext ctx)
    {
        /*
         * 
         * Handle Playing which Melee Attack animation to play
         * (combo, regular, other, etc)
         * 
         */

        if (!ctx.performed || inFiringMode || !stateManager.playerState.hasWeapon) return;

        if (canCombo && stateManager.isPerformingAction)
        {
            canCombo = false;
            if (lastAttackPerformed == meleeAttackAction01)
            {
                animatorManager.PlayTargetAnimation(meleeAttackAction02, true);
                SFXManager.instance.PlaySFXClip(attack02SFX, transform, 0.5f);
            }
            else
            {
                animatorManager.PlayTargetAnimation(meleeAttackAction01, true);
                SFXManager.instance.PlaySFXClip(attack01SFX, transform, 0.5f);

            }
            
        }
        else if(!stateManager.isPerformingAction)
        {
            animatorManager.PlayTargetAnimation(meleeAttackAction01, true);
            SFXManager.instance.PlaySFXClip(attack01SFX, transform, 0.5f);
        }

        
    }
    public void Attack()
    {
        /*
         *  Animation event that spawns an overlap sphere to see if we do damage
         */

        Collider[] hits = Physics.OverlapSphere(meleeAttackPoint.position, meleeAttackRadius, WorldManager.Instance.GetAttackableLayer());
        foreach (Collider hit in hits)
        {
            //see if it is dameagable
            if (hit.gameObject.TryGetComponent(out IDamageable enemy))
            {
                if (hit.gameObject.TryGetComponent(out CharacterStateManager manager))
                    if (manager.isDead || hit.gameObject == gameObject || manager.isInvincible)
                        continue;
                SFXManager.instance.PlaySFXClip(enemyHitSFX, transform, .75f);

                stateManager.DealDamage(enemy);
                TriggerHitPause(waitFrames);
                
            }

        }
    }
    public void Melee01VFX()
    {
        Quaternion playerRotation = Quaternion.LookRotation(transform.forward);

        Quaternion vfxOriginalRotation = melee01AttackVFX.transform.rotation;

        Quaternion combinedRotation = playerRotation * vfxOriginalRotation;

        GameObject particleInstance = Instantiate(melee01AttackVFX, lockOnTransform.position, combinedRotation);

       
        // Get the VFX and play
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        Destroy(particleInstance, particleSystem.main.duration);
    }
    #endregion
    #region Combos

    public void EnableCanDoCombo()
    {
        canCombo = true;
    }
    public void DisableCanDoCombo()
    {
        canCombo = false;
    }
    #endregion
    #region Lock On
    public void HandleLockOn()
    {

        cameraManager.HandleLockOnTargets();
        if (currentTarget != null)
            if (currentTarget.isDead)
            {
                currentTarget.healthBar.ToggleHealthBar(false);
                currentTarget = null;
            }
        if (cameraManager.nearestLockOnTarget != null && currentTarget == null)
        {
            currentTarget = (AICharacterManager)cameraManager.nearestLockOnTarget;
            cameraManager.availableTargets.Clear();
            currentTarget.healthBar.ToggleHealthBar(true);
        }
    }
    public void LockOnSelectionInput(InputAction.CallbackContext ctx)
    {
        /*
         *  Handle switching current lock on target
         */

        if (!stateManager.isLockedOn)
            return;
        lockOnSelectionInput = ctx.ReadValue<Vector2>();
        if (lockOnSelectionInput == Vector3.zero)
        {
            canChangeSelection = true;
            return;
        }
        if (!canChangeSelection)
            return;
        HandleLockOnSelectionChanged();
    }
    public void HandleLockOnInput(InputAction.CallbackContext ctx)
    {
        /*
        *   Handle entering lock on mode
        */
        if (ctx.canceled)
        {
            stateManager.isLockedOn = false;
            ClearLockOn();
            canChangeSelection = true;
            cameraManager.ToggleLockOn(false);
        }

        if (!ctx.performed) return;

        stateManager.isLockedOn = true;
        cameraManager.ToggleLockOn(true);

    }
   
    public void HandleLockOnSelectionChanged()
    {
        /*
         *  Calculate which targets are to the left, right, above, and below
         *  and assign new target based on input
         */

        cameraManager.HandleLockOnTargets();
        CharacterStateManager newTarget = null;
        if(lockOnSelectionInput.x < 0 && cameraManager.leftLockOnTarget != null)// left target
        {
            newTarget = cameraManager.leftLockOnTarget;
        }
        else if(lockOnSelectionInput.x > 0 && cameraManager.rightLockOnTarget != null)//right target
        {
            newTarget = cameraManager.rightLockOnTarget;

        }
        else if (lockOnSelectionInput == Vector3.down && cameraManager.downLockOnTarget != null)//down target
        {
            newTarget = cameraManager.downLockOnTarget;

        }
        else if (lockOnSelectionInput == Vector3.up && cameraManager.upLockOnTarget != null)//up target
        {
            newTarget = cameraManager.upLockOnTarget;

        }
        if(currentTarget != null && newTarget != null)
        {
            currentTarget.healthBar.ToggleHealthBar(false);
        }
        currentTarget = newTarget;
        if (currentTarget != null)
        {
            canChangeSelection = false;
            currentTarget.healthBar.ToggleHealthBar(true);
        }
    }
    private void ClearLockOn()
    {
        if(currentTarget != null) 
            currentTarget.healthBar.ToggleHealthBar(false);

        currentTarget = null;
        //clear adjacent targets
        cameraManager.leftLockOnTarget = null;
        cameraManager.rightLockOnTarget = null;
        cameraManager.upLockOnTarget = null;
        cameraManager.downLockOnTarget = null;
    }
    #endregion
    #region Wait Frames
    public void TriggerHitPause(int frames)
    {
        StartCoroutine(PauseForFrames(frames));
    }
   
    private IEnumerator PauseForFrames(int frames)
    {
        // Set the time scale to 0 to pause the game
        Time.timeScale = 0;

        // Wait for the specified number of frames
        for (int i = 0; i < frames; i++)
        {
            yield return null; // Wait for one frame
        }

        // Resume the game
        Time.timeScale = 1;
    }
    public void TurnPlayerToCamera()
    {

        // Get the direction from the player to the camera
        Vector3 cameraDirection = -cameraManager.transform.forward;
        cameraDirection.y = 0; // Flatten the direction to the Y-plane
        cameraDirection.Normalize();

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(cameraDirection, Vector3.up);

        // Rotate the player to face the camera over time using DOTween
        transform.DORotateQuaternion(targetRotation, 0.3f);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackRadius);
    }
}
