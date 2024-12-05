using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIButtonPrompt : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    private PlayerInput playerInput;
    private InputAction interactAction;
    public string action = "Open Journal";
    private void OnEnable()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            interactAction = playerInput.actions[action];

    }
    private void Update()
    {

        if (playerInput != null)
        {
            inputText.text = UpdatePopupKey();
            return;
        }

        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null ) 
            interactAction = playerInput.actions[action];
        
    }

    private string UpdatePopupKey()
    {
        string interactKey = "";

        // Get the current control scheme (e.g., "Keyboard&Mouse" or "Gamepad")
        string controlScheme = playerInput.currentControlScheme;
        int index = -1;
        if (controlScheme == "Keyboard&Mouse")

        {
            index = 0;
        }
        else if (controlScheme == "Gamepad")
        {
            index = 1;
        }
        // Get the binding display string for the specific control scheme
        interactKey = interactAction.GetBindingDisplayString(index);

        return interactKey;
    }
}
