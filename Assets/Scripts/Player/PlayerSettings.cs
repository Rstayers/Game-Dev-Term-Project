using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Stats/Player Stats")]
public class PlayerSettings : Stats
{
    [Header("Falling Attributes")]
    public float gravityMultiplier;
    public float groundedThreshold;
    public float maxHeightThreshold;
    public float groundStickForce;
    public float cliffDetectionDistance; 
    [Header("Speed")]
    public float speed;
    public float sprintSpeed;
    [Header("Dodging")]
    public float dodgeDuration;
    public float dodgeDistance;
    public float invincibilityFrames;
    public GameObject dodgeVFX;
    [Header("Rotation smoothness")]
    public float rotationSpeed;
    [Header("Navigation")]
    [Range(0, 90)]
    public float maxSlopeAngle = 45f;
    public float stepHeight = 0.3f;

}
