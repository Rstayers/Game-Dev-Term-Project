using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    [Header("Layers")]
    [SerializeField] LayerMask attackableLayers;
    [SerializeField] LayerMask playerLayers;
    [SerializeField] LayerMask enviromentLayers;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask interactionLayers;
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera cam;
    private CinemachineImpulseSource cameraShake;
    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public CinemachineImpulseSource GetCameraShake()
    {
        return cameraShake;
    }
    public LayerMask GetAttackableLayer()
    {
        return attackableLayers;
    }
    public LayerMask GetEnviroLayers()
    {
        return enviromentLayers;
    }
    public LayerMask getPlayerLayers()
    {
        return playerLayers;
    }
    public LayerMask GetGroundLayer()
    {
        return groundLayer;
    }
    public LayerMask GetInteractionLayers()
    {
        return interactionLayers;
    }
    public CinemachineVirtualCamera GetCamera()
    {
        return cam;
    }
}
