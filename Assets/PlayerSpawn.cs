using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void Awake()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
