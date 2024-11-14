using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 rotation = Vector3.zero;
    private bool opened = false;

    public void Open()
    {
        if (opened) return;
        transform.DORotate(rotation, 5f).SetEase(Ease.Linear);
        opened = true;
    }
}