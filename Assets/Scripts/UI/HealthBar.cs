using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    private GameObject parent;
    [SerializeField] CinemachineVirtualCamera cam;
    private void Awake()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        healthBar = GetComponent<Slider>();
        parent = transform.GetChild(0).gameObject;
    }
    public void UpdateHealthBar(float currentValue, float maxvalue)
    {
        healthBar.value = currentValue / maxvalue;
    }
    public void ToggleHealthBar(bool enabled)
    {
        parent.SetActive(enabled);
    }
    private void Update()
    {
        if (!parent.activeSelf) return;
        transform.LookAt(cam.transform);
   }
}
