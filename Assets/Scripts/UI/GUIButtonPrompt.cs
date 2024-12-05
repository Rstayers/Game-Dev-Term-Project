using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GUIButtonPrompt : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction interactAction;
    public List<GameObject> GUI;
    private JournalManager journalManager;
    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        journalManager = GetComponent<JournalManager>();
    }
    private void Update()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        if(!playerInput) return;
        if (!journalManager.isJournalOpen) return;
        
        string controlScheme = playerInput.currentControlScheme;
      
        if (controlScheme == "Keyboard&Mouse")
        {
            if (GUI[0].activeSelf)
                return;
            foreach (GameObject go in GUI)
            {
                go.SetActive(true);
            }
        }
        else if (controlScheme == "Gamepad")
        {

            foreach (GameObject go in GUI)
            {
                go.SetActive(false);
            }
        }
    }
}
