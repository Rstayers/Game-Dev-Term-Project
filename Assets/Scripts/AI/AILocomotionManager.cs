using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class AILocomotionManager : MonoBehaviour
{
    [HideInInspector] public AIAnimator animator;
    public float distanceFromTarget;
    private AICombatManager combatManager;
    public float patrolRadius = 5f; // Radius within which the enemy can pick random points
    public float originalRange = 10f; // The maximum distance the enemy can move from its original position

    public Vector3 originalPosition;
    public Vector3 patrolPoint;
    [HideInInspector] public bool isPatrolling = false;
    [HideInInspector] public float distanceToPoint;
    private void FixedUpdate()
    {
        if(combatManager?.currentTarget != null)
            distanceFromTarget = Vector3.Distance(combatManager.currentTarget.transform.position, transform.position);
        
    }
    public void RotateTowardsAgent(AICharacterManager character)
    {
        if(character.isMoving)
        {
            character.transform.rotation = character.navMeshAgent.transform.rotation;
        }
    }
    private void Awake()
    {
        animator = GetComponent<AIAnimator>();
        animator.Initialize();
        combatManager = GetComponent<AICombatManager>();
        originalPosition = transform.position;

    }

    public void Patrol(AICharacterManager manager)
    {
        PickNewPatrolPoint(manager);
        // Move the enemy towards the patrol point
        manager.navMeshAgent.SetDestination(patrolPoint);
        isPatrolling = true;
        manager.isMoving = true;
        
       
    }
    public void UpdatePatrol(AICharacterManager character)
    {
        if (isPatrolling)
        {
            distanceToPoint = Vector3.Distance(transform.position, patrolPoint);
            character.transform.rotation = character.navMeshAgent.transform.rotation;
        }
    }
    void PickNewPatrolPoint(AICharacterManager character)
    {
        bool validPointFound = false;

        while (!validPointFound)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
           
            Vector3 potentialPoint = originalPosition + randomDirection;
            // Check if the new point is within the original range
            if (Vector3.Distance(originalPosition, potentialPoint) <= originalRange)
            {
                NavMeshHit hit;
                // Check if the point is reachable on the NavMesh
                if (NavMesh.SamplePosition(potentialPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    patrolPoint = hit.position;
                    
                    validPointFound = true;
                }
            }
        }
    }
}
