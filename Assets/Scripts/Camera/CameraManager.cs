using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using System.Net;


public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    [Header("Player")]
    private CharacterStateManager player;

    [Header("Lock On")]
    [SerializeField] private float lockOnDetectionDistance = 20;
    private bool isLocked = false;

    [Header("Lock On Camera Angles")]
    [SerializeField] float topDownAngle = 20f; 
    [SerializeField] float changeSpeed = 0.5f;
    private Transform pivot;
    private Quaternion standardRotation;
    private bool arrived = false;

    [Header("Lock On Target Info")]
    [HideInInspector] public List<CharacterStateManager> availableTargets = new List<CharacterStateManager>();
    [HideInInspector] public CharacterStateManager nearestLockOnTarget, rightLockOnTarget, leftLockOnTarget, downLockOnTarget, upLockOnTarget;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<CharacterStateManager>();
        pivot = transform.parent.transform;
        standardRotation = pivot.rotation;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = player.cameraLock;
    }
    private void Update()
    {
        if (arrived)
            return;
        if (isLocked)
        {
            EnterLockOn();
        }
        else
        {
            ExitLockOn();
        }
    }
    public void EnterLockOn()
    {
        
       
        // Optionally, adjust the rotation towards the top-down angle if needed
        Quaternion targetRotation = Quaternion.Euler(topDownAngle + pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
        pivot.rotation = Quaternion.Slerp(pivot.localRotation, targetRotation, changeSpeed * Time.deltaTime);

        // Check if the rotation has arrived at the target rotation
        if (Quaternion.Angle(pivot.rotation, targetRotation) < 0.001f)
            arrived = true;
    }
    public void ExitLockOn()
    {
        if (player != null)
        {


            // Smoothly return the camera to its default rotation
            Quaternion targetRotation = standardRotation;
            pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, changeSpeed * Time.deltaTime);

            // Check if the camera has returned to its default rotation
            if (Quaternion.Angle(pivot.rotation, standardRotation) < 0.001f)
            {
                arrived = true;
            }
        }
    }
    public void ToggleLockOn(bool locked)
    {
        isLocked = locked;
        arrived = false;
    }
    public void HandleLockOnTargets()
    {
        //set bounds
        float shortest = Mathf.Infinity;
        float shortestToLeft = -Mathf.Infinity;
        float shortestToRight = Mathf.Infinity;
        float shortestToDown = -Mathf.Infinity;
        float shortestToUp = Mathf.Infinity;
       
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, lockOnDetectionDistance, WorldManager.Instance.GetAttackableLayer());
        
        foreach(Collider collider in colliders)
        {
            CharacterStateManager lockOnTarget = collider.GetComponent<CharacterStateManager>();

            if(lockOnTarget != null )
            {
                //Check if in FOV
                Vector3 lockOnDir = lockOnTarget.transform.position - player.transform.position;
                float distance = Vector3.Distance(player.transform.position, lockOnTarget.transform.position);
                float FOV = Vector3.Angle(lockOnDir, transform.forward);

                //if target is ourselves
                if (lockOnTarget.transform.root == player.transform.root)
                    continue;
                if (lockOnTarget.isDead)
                    continue;
                if(!Physics.Linecast(transform.position, lockOnTarget.combatManager.lockOnTransform.position, WorldManager.Instance.GetEnviroLayers()))
                {
                    availableTargets.Add(lockOnTarget);
                }
            }
        }
        foreach(CharacterStateManager target in availableTargets)
        {
            if (target != null)
            {
                float distance = Vector3.Distance(player.transform.position, target.transform.position);

                if(distance < shortest)
                {
                    shortest = distance;
                    nearestLockOnTarget = target;
                }
                //search for left/right/up/down targets
                Vector3 enemyRelPosition = player.transform.InverseTransformPoint(target.transform.position);
                var distanceFromLeft = enemyRelPosition.x;
                var distanceFromRight = enemyRelPosition.x;

                var distanceFromUp = enemyRelPosition.z;
                var distanceFromDown = enemyRelPosition.z;

                if (target == player.combatManager.currentTarget)
                    continue;
                if(enemyRelPosition.x <= 0.00 && distanceFromLeft > shortestToLeft)
                {
                    shortestToLeft = distanceFromLeft;
                    leftLockOnTarget = target;
                }
                if (enemyRelPosition.x >= 0.00 && distanceFromRight > shortestToRight)
                {
                    shortestToRight = distanceFromRight;
                    rightLockOnTarget = target;
                }
                if (enemyRelPosition.z <= 0.00 && distanceFromDown > shortestToDown)
                {
                    shortestToDown = distanceFromDown;
                    downLockOnTarget = target;
                }
                if (enemyRelPosition.z >= 0.00 && distanceFromUp > shortestToUp)
                {
                    shortestToUp = distanceFromUp;
                    upLockOnTarget = target;
                }
             

            }
        }
    }
}
