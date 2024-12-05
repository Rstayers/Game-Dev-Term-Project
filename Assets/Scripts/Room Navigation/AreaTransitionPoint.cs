using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class AreaTransitionPoint : PlayerSpawn
{
    [Header("Transition Settings")]
    public string targetSceneName; // The name of the scene to load
    public string targetPointName; // The name of the target transition point in the new scene

    [Header("Fade Settings")]
    public float fadeDuration = 1f; // Duration for fade in/out, if applicable

    private bool isTransitioning = false;

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning) return;
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            StartCoroutine(TransitionToScene());
        }
    }

    private IEnumerator TransitionToScene()
    {
        isTransitioning = true;


        WorldManager.Instance.FadeToBlack(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        Destroy(player);
        // Load the target scene additively
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
       

        // Wait for scene activation
        yield return new WaitUntil(() => SceneManager.GetSceneByName(targetSceneName).isLoaded);

        // Find the target point in the new scene
        Scene targetScene = SceneManager.GetSceneByName(targetSceneName);
        AreaTransitionPoint targetPoint = null;

        while (targetPoint == null)
        {
            targetPoint = FindTargetPointInScene(targetPointName, targetScene);
            yield return null; // Keep looking until the object is registered
        }


        // Spawn the player at the target point

        
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(targetScene);
        GameManager.Instance.SpawnPlayerAtPoint(targetPoint.GetSpawn());
        SceneManager.UnloadSceneAsync(currentScene);


        
        isTransitioning = false;
    }

    private AreaTransitionPoint FindTargetPointInScene(string pointName, Scene scene)
    {
        if (!scene.isLoaded) return null;

        foreach (GameObject rootObject in scene.GetRootGameObjects())
        {
            AreaTransitionPoint point = rootObject.GetComponentInChildren<AreaTransitionPoint>();
            if (point != null && point.name == pointName)
            {
                return point;
            }
        }

        return null;
    }
}
