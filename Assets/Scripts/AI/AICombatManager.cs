using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AICombatManager : CharacterCombatManager
{
    [Header("Target Information")]
    public float viewableAngle;
    public Vector3 targetsDirection;
    public float distanceFromTarget;

    [Header("Detection Stats")]
    [SerializeField] private float detectionRadius = 10f;
    public float minimumFOV = -35f;
    public float maximumFOV = 35f;

    [Header("Combat Animation Actions")]
    public ActionContainer turnR, turnL;

    [Header("Recovery Timer")]
    public float actionRecoveryTimer = 0;

    [Header("Combat Stats")]
    [SerializeField] private float attackRotationSpeed;
   
    public void FindATargetViaLineOfSight(AICharacterManager character)
    {
        if (currentTarget != null)
            return;

        Collider[] colliders = Physics.OverlapSphere(character.transform.position, detectionRadius, WorldManager.Instance.getPlayerLayers()) ;

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStateManager targetCharacter = colliders[i].transform.GetComponent<CharacterStateManager>();
            if (targetCharacter == null)
                continue;
            if (targetCharacter == character)
                continue;

            Vector3 targetDirection = targetCharacter.transform.position - character.transform.position;

            float angleOfPotentialTarget = Vector3.Angle(targetDirection, character.transform.forward);
            if (angleOfPotentialTarget < maximumFOV && angleOfPotentialTarget > minimumFOV)
            {
                //check for blocks
                if (Physics.Linecast(character.aiCombatManager.lockOnTransform.position, targetCharacter.combatManager.lockOnTransform.position, WorldManager.Instance.GetEnviroLayers()))
                {
                    Debug.DrawRay(character.aiCombatManager.lockOnTransform.position, targetCharacter.combatManager.lockOnTransform.position);
                }
                else
                {
                    targetsDirection = targetCharacter.transform.position - transform.position;
                    this.viewableAngle = GetAngleOfTarget(transform, targetsDirection);
                    character.aiCombatManager.currentTarget = targetCharacter;
                    PivotTowardsTarget(character);
                }
            }
        }
    }
    public void AIMeleeAttack()
    {
        /*
         *  Animation event that spawns an overlap sphere to see if we do damage
         */

        Collider[] hits = Physics.OverlapSphere(meleeAttackPoint.position, meleeAttackRadius, WorldManager.Instance.getPlayerLayers());
        foreach (Collider hit in hits)
        {
            Debug.Log(hit.gameObject.name);
            //see if it is dameagable
            if (hit.gameObject.TryGetComponent(out CharacterStateManager player))
            {
                
                if (player.isDead || hit.gameObject == gameObject)
                    continue;

                stateManager.DealDamage(player);
              
            }

        }
    }
    public void PivotTowardsTarget(AICharacterManager targetCharacter)
    {
        // PLAY PIVOT DEPENDING ON ANGLE
        if (targetCharacter.isPerformingAction)
            return;
        if (viewableAngle >= 50)
        {
            targetCharacter.animatorManager.PlayTargetAnimation(turnR, true);
        }
        if (viewableAngle <= -50)
        {
            targetCharacter.animatorManager.PlayTargetAnimation(turnL, true);
        }
    }
    public float GetAngleOfTarget(Transform characterTransform, Vector3 targetDirection)
    {
        targetsDirection.y = 0; 
        float angle = Vector3.Angle(characterTransform.forward, targetDirection);
        Vector3 cross = Vector3.Cross(characterTransform.forward, targetDirection);

        if (cross.y < 0) angle = -angle;

        return angle;
    }
    public void RotateTowardsAgent(AICharacterManager character)
    {
        if(character.isMoving)
        {
            character.transform.rotation = character.navMeshAgent.transform.rotation; 
        }
    }
    public void RotateTowardsTargetWhileAttacking(AICharacterManager character)
    {
        if(currentTarget == null) return;
        if (!character.isPerformingAction) return;
        Vector3 direction = currentTarget.transform.position - character.transform.position;
        direction.y = 0;
        direction.Normalize();

        if(direction == Vector3.zero)
        {
            direction = character.transform.forward;
        }

        Quaternion rotation = Quaternion.LookRotation(targetsDirection);

        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rotation, attackRotationSpeed * Time.deltaTime);
    }
    public void HandleActionRecovery(AICharacterManager character)
    {
        if(actionRecoveryTimer > 0)
        {
            if(!character.isPerformingAction)
            {
                actionRecoveryTimer -= Time.deltaTime;
            }
        }
    }
}
