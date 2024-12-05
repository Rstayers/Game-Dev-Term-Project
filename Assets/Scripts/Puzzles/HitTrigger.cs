using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class HitTrigger : MonoBehaviour, IDamageable
{
    [SerializeField] private HitTriggerRecipient recipient;
    [SerializeField] private Material hitMaterial;
    private Material original;
    private bool triggered = false;

    [SerializeField] private AudioClip hitAudioClip;
    private void Start()
    {
        original = GetComponent<MeshRenderer>().material;
    }
    public virtual void TakeDamage(float amount, GameObject originator)
    {
        if (triggered) return;
        GetComponent<MeshRenderer>().material = hitMaterial;
        triggered = true;
        SFXManager.instance.PlaySFXClip(hitAudioClip, transform);
        recipient.Trigger(gameObject);
    }
    public void DealDamage(IDamageable recipient)
    {
        return;
    }
    public void Clear()
    {
        GetComponent<MeshRenderer>().material = original;
        triggered= false;
    }
    public void OnDeath()
    {
        return;
    }
}
