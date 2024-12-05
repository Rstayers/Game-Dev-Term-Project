using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawn;
    [SerializeField] private string sceneName;
    public void RegisterSpawn(PlayerState playerState)
    {
        playerState.lastSpawn = GetSpawn();
    }
    
    public PlayerState.PlayerSpawnData GetSpawn()
    {
        return new PlayerState.PlayerSpawnData
        {
            sceneName = this.sceneName,
            position = spawn.position,
            rotation = spawn.rotation
        };
    }


}
