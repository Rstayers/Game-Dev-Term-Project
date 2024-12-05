using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitTriggerPuzzle : HitTriggerRecipient, ITriggerRecipient
{
    private List<GameObject> hitTriggers = new List<GameObject>();
    private bool solved = false;
    public List<GameObject> answerOrder = new List<GameObject>();

    public GameObject enemy;
    public Transform spawnPoint;
    public override void Trigger(GameObject originator)
    {
        if (hitTriggers.Contains(originator))
            return;
        hitTriggers.Add(originator);
        if (hitTriggers.Count == answerOrder.Count)
        {
            for(int i = 0; i < hitTriggers.Count; i++)
            {
                if (hitTriggers[i] != answerOrder[i])
                {
                    //spawn enemy
                    Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                    //clear triggers
                    foreach(GameObject trigger in hitTriggers)
                    {
                        trigger.GetComponent<HitTrigger>().Clear();
                    }
                    hitTriggers.Clear();
                    return;
                }

            }
            solved = true;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), 5f).SetEase(Ease.Linear);
        }
    }
}
