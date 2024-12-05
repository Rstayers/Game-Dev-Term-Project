using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnLoad : PlayerSpawn
{
    private void Start()
    {
        if (GameManager.Instance.playerState.lastSpawn == null)
            RegisterSpawn(GameManager.Instance.playerState);
        if (spawn == null)
            spawn = transform;
    }
}
