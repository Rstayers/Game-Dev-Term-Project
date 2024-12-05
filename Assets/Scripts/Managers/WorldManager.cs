using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;
public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    [Header("Layers")]
    [SerializeField] LayerMask attackableLayers;
    [SerializeField] LayerMask playerLayers;
    [SerializeField] LayerMask enviromentLayers;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask interactionLayers;
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera cam;
    private CinemachineImpulseSource cameraShake;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject fadePrefab;
    private Image fadeScreen;
    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
   
    public CinemachineImpulseSource GetCameraShake()
    {
        return cameraShake;
    }
    public LayerMask GetAttackableLayer()
    {
        return attackableLayers;
    }
    public LayerMask GetEnviroLayers()
    {
        return enviromentLayers;
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    public LayerMask getPlayerLayers()
    {
        return playerLayers;
    }
    public LayerMask GetGroundLayer()
    {
        return groundLayer;
    }
    public LayerMask GetInteractionLayers()
    {
        return interactionLayers;
    }
    public CinemachineVirtualCamera GetCamera()
    {
        return cam;
    }

    private void InitializeFadeScreen()
    {
        if (fadePrefab == null)
        {
            Debug.LogError("Fade prefab is not assigned in the WorldManager!");
            return;
        }

        // Instantiate the fade prefab and set it as a child of the canvas
        GameObject fadeObject = Instantiate(fadePrefab);
        Canvas canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        if (canvas != null)
        {
            fadeObject.transform.SetParent(canvas.transform, false); // Match the canvas transform
        }

        fadeScreen = fadeObject.GetComponent<Image>();
        if (fadeScreen != null)
        {
            fadeScreen.color = new Color(0, 0, 0, 0); // Start as fully transparent
        }
        else
        {
            Debug.LogError("Fade prefab does not have an Image component!");
        }
        return; 
    }

    public void FadeToBlack(float fadeTime)
    {
       
        InitializeFadeScreen();

        if (fadeScreen != null)
        {
            StartCoroutine(FadeInAndOut(fadeTime));
        }
        else
        {
            Debug.LogError("Fade screen could not be initialized!");
        }
    }

    private IEnumerator FadeInAndOut(float fadeTime)
    {
        // Fade to black
        yield return fadeScreen.DOFade(1f, fadeTime).WaitForCompletion();
        yield return new WaitForSeconds(fadeTime*.5f);
        // Fade back to transparent
        yield return fadeScreen.DOFade(0f, fadeTime * 2.5f).WaitForCompletion();
        Destroy(fadeScreen);
    }
}
