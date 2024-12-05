using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class RestSpotInteractable : PlayerSpawn, IInteractable
{
    private bool _interacted;

    [SerializeField] private float fadeDuration = 1f; // Duration of fade in/out
    [SerializeField] private AudioClip interactSound;
    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }



    public void Interact(CharacterAnimatorManager anim)
    {
        if (_interacted) return;
        GameManager manager = FindObjectOfType<GameManager>();
        RegisterSpawn(GameManager.Instance.playerState);
        _interacted = true;
        SFXManager.instance.PlaySFXClip(interactSound, transform);
        UIHealthBar.instance.FillHearts();
        EnemyManager.Instance.RespawnEnemies();
        WorldManager.Instance.FadeToBlack(fadeDuration);
        StartCoroutine(InteractTimer());
    }

    private IEnumerator InteractTimer()
    {

        // Fade to black
        yield return new WaitForSeconds(fadeDuration);
        _interacted = false;

    }
}
