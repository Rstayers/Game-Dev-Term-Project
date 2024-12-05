using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class ChestBasic : MonoBehaviour, IInteractable
{
    
    [SerializeField] private Transform lidPivot;
    [SerializeField] private Transform chestCenter; // Chest center to spawn objects
    [Header("Fine tuning stats")]
    [SerializeField] private int chestAngle;
    [Range(0f, 1f)]
    [SerializeField] private float timeToOpen;
    [Header("Drops")]
    [SerializeField] private List<GameObject> drops;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float parabolaHeight = 3f; 
    [Range(0f, 1f)]
    [SerializeField] private float objectMoveDuration = 1f;
    [SerializeField] private GameObject particleEffectPrefab; 
    [SerializeField] private float particleEffectLifetime = 2f; 


    private bool _interacted;

    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }
    // Start is called before the first frame update
    public void Interact(CharacterAnimatorManager anim)
    {
        interacted = true;
        SpawnParticleEffect();
        Vector3 angle = new Vector3(-chestAngle, lidPivot.eulerAngles.y, lidPivot.eulerAngles.z);
        lidPivot.DORotate(angle, timeToOpen).SetEase(Ease.OutFlash).OnComplete(()=>{
            SpawnObjects();
        });
        
    }
    private void SpawnParticleEffect()
    {
        if (particleEffectPrefab != null)
        {
            // Instantiate the particle effect at the chest center
            GameObject effect =  Instantiate(particleEffectPrefab, chestCenter.position, particleEffectPrefab.transform.rotation);
            Destroy(effect, particleEffectLifetime);
        }
        else
        {
            Debug.LogWarning("Particle effect prefab is not assigned!");
        }
    }
    private void SpawnObjects()
    {
        if(drops.Count == 0)
        {
            return;
        }
        for (int i = 0; i < drops.Count; i++)
        {
            
            GameObject selectedObject = drops[Random.Range(0, drops.Count)];

            // Calculate the angle for each object
            float angle = i * (360f / drops.Count);
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            // Instantiate the object at the chest's center
            GameObject spawnedObject = Instantiate(selectedObject, chestCenter.position, Quaternion.identity);

            // Calculate the target position on the ground
            Vector3 targetPosition = chestCenter.position + direction * explosionRadius;

            // Create a parabolic path
            Vector3 peakPosition = new Vector3(targetPosition.x, chestCenter.position.y + parabolaHeight, targetPosition.z);

            // Animate the object along the parabolic path
            Sequence parabolicMovement = DOTween.Sequence();
            parabolicMovement.Append(spawnedObject.transform.DOMove(peakPosition, objectMoveDuration / 2).SetEase(Ease.OutQuad)); // Move up
            parabolicMovement.Append(spawnedObject.transform.DOMove(targetPosition, objectMoveDuration / 2).SetEase(Ease.InQuad)); // Move down
        }
    }
}
