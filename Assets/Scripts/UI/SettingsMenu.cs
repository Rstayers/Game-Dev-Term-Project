using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SettingsMenu : MonoBehaviour
{

    [SerializeField] private GameObject backButton;
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        EventSystem.current.SetSelectedGameObject(backButton);
    }    
}
