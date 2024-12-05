using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<AISpawn> spawns = new List<AISpawn>();
    public static EnemyManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
 
    }
    void Start()
    {
        foreach(var child in GetComponentsInChildren<AISpawn>())
        {
            spawns.Add(child);
        }
        RespawnEnemies();
    }

    public void RespawnEnemies()
    {
        foreach(var spawn in spawns)
        {
            GameObject enemy = spawn.enemy;
            if (spawn.isDead)
            {
                GameObject clone = Instantiate(enemy, spawn.transform.position, Quaternion.identity );
                clone.transform.parent = spawn.transform;
                clone.GetComponent<AICharacterManager>().spawn = spawn;
                spawn.isDead = false;
            }
        }
    }
}
