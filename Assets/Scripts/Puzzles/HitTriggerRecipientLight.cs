using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTriggerRecipientLight : HitTriggerRecipient, ITriggerRecipient
{
    [SerializeField]private Material lightUp;
 
    public override void Trigger(GameObject originator)
    {
        GetComponent<MeshRenderer>().material = lightUp;
        triggered = true;
    }
}
