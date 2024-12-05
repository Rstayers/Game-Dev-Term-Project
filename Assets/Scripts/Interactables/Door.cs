using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 rotation = Vector3.zero;
    private bool opened = false;
    [SerializeField] private AudioClip openSFX;
    public void Open()
    {
        if (opened) return;
        SFXManager.instance.PlaySFXClip(openSFX, transform);
        transform.DORotate(rotation, 5f).SetEase(Ease.Linear);
        opened = true;
        
    }
}
