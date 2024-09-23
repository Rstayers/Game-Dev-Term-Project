using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats/Regular Stats")]
public class Stats : ScriptableObject
{
    [Header("Base Stats")]
    public int maxHealth;
    public float meleeAttackPower;
    public int maxStamina;
    public int maxMana;
    public GameObject onDeathParticles;
}
