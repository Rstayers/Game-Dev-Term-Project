using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CombatAreaEnter : MonoBehaviour
{
    [SerializeField] private GameObject enterGate, exitGate;
    [SerializeField] private Transform enterUp, enterDown, exitUp, exitDown;
    [SerializeField] private List<AISpawn> enemies;

    private CharacterStateManager player;
    private List<AISpawn> enemiesAlive;
    private bool entered = false;
    private bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        ResetEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (!entered) return;
        foreach (var enemy in enemies)
        {
            if (enemy.isDead)
                enemiesAlive.Remove(enemy);
            else
                if (!enemiesAlive.Contains(enemy))
                    enemiesAlive.Add(enemy);
            if(enemiesAlive.Count == 0)
                completed = true;
        }
        if (player.isDead)
            Failed();
        if (completed)
            OnComplete();

    }

    private void OnComplete()
    {
        enterGate.transform.DOMove(enterDown.position, 2).SetEase(Ease.Linear);
        exitGate.transform.DOMove(exitDown.position, 2).SetEase(Ease.Linear);
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if(entered) return; 
        player = other.gameObject.GetComponent<CharacterStateManager>();    
        enterGate.transform.DOMove(enterUp.position, 1).SetEase(Ease.Linear);
        exitGate.transform.DOMove(exitUp.position, 1).SetEase (Ease.Linear);
        entered = true;
    }
    private void Failed()
    {
        player = null;
        OnComplete();
        entered = false;
    }
    private void ResetEnemies()
    {
        entered = false;
        completed = false;
        enemiesAlive = enemies;
    }
}
