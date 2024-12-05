using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerRecipient 
{
    void Trigger(GameObject originator);
}
public class HitTriggerRecipient : MonoBehaviour, ITriggerRecipient
{
    // Start is called before the first frame update
    public bool triggered = false;
    public virtual void Trigger(GameObject originator)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
