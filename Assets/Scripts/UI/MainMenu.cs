using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip selectSFX;
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private GameObject playButton;
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Verdant Grove");
        if(GameManager.Instance != null)
            GameManager.Instance.Initialize();
    }
    public void OnSelect()
    {
        SFXManager.instance.PlaySFXClip(selectSFX, transform);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
