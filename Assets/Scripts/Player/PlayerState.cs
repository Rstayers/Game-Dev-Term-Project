using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/PlayerState")]
public class PlayerState : ScriptableObject
{
    public bool hasWeapon;
    public float currentHealth = 3;

    [System.Serializable]
    public class PlayerSpawnData
    {
        public string sceneName;
        public Vector3 position;
        public Quaternion rotation;
    }

    public PlayerSpawnData lastSpawn;
}
