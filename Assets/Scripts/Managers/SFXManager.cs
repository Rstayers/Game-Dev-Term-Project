using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFXClip(AudioClip clip, Transform spawn, float volume=1f)
    {
        AudioSource source = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
        source.clip = clip;
        source.volume = volume;
        source.Play();
        float clipLength = source.clip.length;
        Destroy(source.gameObject, clipLength );
    }
}
