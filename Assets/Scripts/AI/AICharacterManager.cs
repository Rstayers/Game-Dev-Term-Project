using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;

public class AICharacterManager : CharacterStateManager, IDamageable
{

    [Header("Managers")]
    [HideInInspector] public AILocomotionManager locomotionManager;
    [HideInInspector] public AICombatManager aiCombatManager;
    private Rigidbody rb;

    [Header("Combat")]
    [SerializeField] private float flashDuration = .1f;
    [SerializeField] private Color emissionHitColor;
    private Renderer enemyRenderer;
    private Color originalColor;

    [Header("Current State")]
    [SerializeField] private AIState currentState;

    [Header("NavMesh")]
    [HideInInspector] public NavMeshAgent navMeshAgent;

    [Header("States")]
    public AIState idle;
    public AIState pursueTarget;
    public AttackState attack;
    public AIState patrol;
    public AIState combatStance;

    public bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = stats.maxHealth;
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        locomotionManager = GetComponent<AILocomotionManager>();
        aiCombatManager = GetComponent<AICombatManager>();
        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.GetColor("_EmissionColor");
        }

        //Use a copy of scriptable object so original is not modified
        idle = Instantiate(idle);
        pursueTarget = Instantiate(pursueTarget);
        attack = Instantiate(attack);
        patrol = Instantiate(patrol);
        combatStance = Instantiate(combatStance);
        currentState = idle;
    }

    private void Update()
    {
        aiCombatManager.HandleActionRecovery(this);
        ProcessStateMachine();
    }

    public override void TakeDamage(float amount, GameObject originator)
    {
        if(isDead) return;
        base.TakeDamage(amount, originator);
        Vector3 knockbackDirection = (transform.position - originator.transform.position).normalized;
        knockbackDirection.y = 0;
        StartCoroutine(ApplyKnockback(knockbackDirection, originator.GetComponent<CharacterCombatManager>().knockbackForce));
        aiCombatManager.currentTarget = originator.GetComponent<CharacterStateManager>();
        currentState = pursueTarget;
        StartCoroutine(FlashWhite());
       
    }
    public void DealDamage(IDamageable recipient)
    {
        recipient.TakeDamage(stats.meleeAttackPower, gameObject);
    }
    public void OnDeath()
    {
        animatorManager.PlayTargetAnimation(die, true);
        isDead = true;
    
    }
    
    void ProcessStateMachine()
    {
        AIState nextState = currentState?.Tick(this);
        if (nextState != null)
        {
            currentState = nextState;
        }
        //the position/rotation should only be reset after the state machine has processed it's tick
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;

        if(aiCombatManager.currentTarget != null)
        {
            aiCombatManager.targetsDirection = aiCombatManager.currentTarget.transform.position - transform.position;
            aiCombatManager.viewableAngle = aiCombatManager.GetAngleOfTarget(transform, aiCombatManager.targetsDirection);
            aiCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aiCombatManager.currentTarget.transform.position);
        }

        if(navMeshAgent.enabled)
        {
            Vector3 destination = navMeshAgent.destination;
            float remainingDistance = Vector3.Distance(destination, transform.position);

            if(remainingDistance > navMeshAgent.stoppingDistance)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
        else
        {
            isMoving = false;
        }
        locomotionManager.animator.UpdateAnimatorFlags(isMoving);
    }
    #region Coroutines
    private IEnumerator ApplyKnockback(Vector3 direction, float force)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.1f)
        {
            // Gradually apply knockback force over time
            rb.AddForce(direction * force * Time.deltaTime, ForceMode.Impulse);

            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }
    }
    private IEnumerator FlashWhite()
    {
        // Set the emission color to white
        enemyRenderer.material.SetColor("_EmissionColor", emissionHitColor);
        enemyRenderer.material.EnableKeyword("_EMISSION");

        // Wait for a short duration
        yield return new WaitForSeconds(flashDuration);

        // Revert the emission color to the original color
        enemyRenderer.material.SetColor("_EmissionColor", originalColor);
    }
    #endregion

    public void DestroyEnemy()
    {
        // Instantiate the particle effect at the given position and with no rotation
        GameObject particleInstance = Instantiate(stats.onDeathParticles, combatManager.lockOnTransform.position, Quaternion.identity);

        // Get the ParticleSystem component
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();

        // Play the particle effect
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // Optionally, destroy the particle after it's done playing
        Destroy(particleInstance, particleSystem.main.duration);
        Destroy(gameObject);
    }


    
}
