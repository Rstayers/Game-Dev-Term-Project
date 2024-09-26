using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount, GameObject originator);
    void DealDamage(IDamageable recipient);

    void OnDeath();
}
public class CharacterStateManager : MonoBehaviour, IDamageable
{
    [Header("Managers")]
    [HideInInspector] public CharacterAnimatorManager animatorManager;
    [HideInInspector] public CharacterCombatManager combatManager;
    [Header("Stats")]
    public Stats stats;
    public float currentHealth;
    public float currentStamina;
    public float currentMana;

    [Header("State Flags")]
    public bool isPlayer;
    public bool isPerformingAction;
    public bool isSprinting;
    public bool isInvincible = false;
    public bool isLockedOn = false;
    public bool isDead = false;
    public bool canInteract = false;
    [Header("State Animations")]
    public ActionContainer die;
    public ActionContainer takeDamage;
    [Header("Health Bar")]
    public HealthBar healthBar;
    [Header("Combat")]
    [SerializeField] private float flashDuration = .1f;
    [SerializeField] private Color emissionHitColor;
    private Renderer _renderer;
    private Color originalColor;
    private void FixedUpdate()
    {
        isPerformingAction = animatorManager.animator.GetBool("isInteracting");
        
    }
    private void Start()
    {
        animatorManager = GetComponent<CharacterAnimatorManager>();
        currentHealth = stats.maxHealth;
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer != null)
        {
            originalColor = _renderer.material.GetColor("_EmissionColor");
        }
        if (isPlayer)
            UIHealthBar.instance.SetUpHearts((int)currentHealth);
        combatManager = GetComponent<CharacterCombatManager>(); 
        currentMana = stats.maxMana;
        currentStamina = stats.maxStamina;
    }
    public virtual void TakeDamage(float amount, GameObject originator)
    {
        /*
         * Called by an enemy
         * Play animations and update stats
         */
        if (isInvincible)
            return;
       
        currentHealth -= amount;
        if(isPlayer)
            UIHealthBar.instance.RemoveHearts(amount);
        else
            healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);
        animatorManager.PlayTargetAnimation(takeDamage, true);
       
        if (currentHealth <= 0)
        {
            OnDeath();
        }
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
    public IEnumerator FlashWhite()
    {
        // Set the emission color to white
        _renderer.material.SetColor("_EmissionColor", emissionHitColor);
        _renderer.material.EnableKeyword("_EMISSION");

        // Wait for a short duration
        yield return new WaitForSeconds(flashDuration);

        // Revert the emission color to the original color
        _renderer.material.SetColor("_EmissionColor", originalColor);
    }
}