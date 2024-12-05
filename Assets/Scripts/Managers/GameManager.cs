using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currencyCount = 0;
    [SerializeField] private TextMeshProUGUI currencyText;
    public PlayerState playerState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        currencyText.text = currencyCount.ToString();
        SpawnPlayer(playerState.lastSpawn);
    }
    public void SpawnPlayer(PlayerState.PlayerSpawnData spawnData)
    {
        if (spawnData != null)
        {
            if (SceneManager.GetActiveScene().name != spawnData.sceneName)
            {
                StartCoroutine(LoadSceneAndRespawn(spawnData.sceneName));
            }
            else
            {
                SpawnPlayerAtPoint(spawnData);
            }
        }
        else
        {
            Debug.LogWarning("No valid spawn point. Respawning at default.");
            SpawnPlayerAtDefault();
        }
    }

    private IEnumerator LoadSceneAndRespawn(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);


        SpawnPlayerAtPoint(playerState.lastSpawn);
    }

    public void SpawnPlayerAtPoint(PlayerState.PlayerSpawnData spawnData)
    {
        GameObject player = Instantiate(WorldManager.Instance.GetPlayer(), spawnData.position, spawnData.rotation);
        CameraManager.Instance.SetPlayer(player.GetComponent<CharacterStateManager>());
        EnemyManager.Instance.RespawnEnemies();
    }

    private void SpawnPlayerAtDefault()
    {
        Vector3 defaultPosition = Vector3.zero; // Replace with your default spawn position
        Quaternion defaultRotation = Quaternion.identity;
        GameObject player = Instantiate(WorldManager.Instance.GetPlayer(), defaultPosition, defaultRotation);
        CameraManager.Instance.SetPlayer(player.GetComponent<CharacterStateManager>());
        EnemyManager.Instance.RespawnEnemies();
    }

    public void UpdateCurrency(int currency)
    {
        currencyCount += currency;
        if (currencyCount < 0)
        {
            currencyCount = 0;
        }
        currencyText.text = currencyCount.ToString();
    }
}
