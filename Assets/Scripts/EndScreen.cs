using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject endUI;
    [SerializeField] private GameObject startUI;
    void OnEnable()
    {
        Initialize();
        DontDestroyOnLoad(gameObject);
    }
    public void Initialize()
    {
        EventSystem.current.SetSelectedGameObject(startUI);
    }
    private void OnTriggerEnter(Collider other)
    {
        endUI.SetActive(true);
       
        endUI.transform.DOScale(1, 1f).SetEase(Ease.InFlash).OnComplete(() =>
        {
           
        }
        );
    }
    public void ExitToGame()
    {
        endUI.transform.DOScale(0, 1f).SetEase(Ease.InFlash).OnComplete(() =>
        {
            endUI.SetActive(false);
        }
        );
    }
    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        endUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");

    }
}
