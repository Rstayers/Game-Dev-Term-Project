using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;
using DG.Tweening;

public class InputPopUp : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    private CinemachineVirtualCamera cam;

    private PlayerInput playerInput; 
    private InputAction interactAction; 
    private void Awake()
    {
        inputText = GetComponentInChildren<TextMeshProUGUI>();
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCirc);
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        playerInput = FindObjectOfType<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }
  
    public void Dissapear()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => {
            DestroyImmediate(gameObject);

        });
    }
    private void Update()
    {
 
        inputText.text = UpdatePopupKey();
        Vector3 directionToCamera = transform.position - cam.transform.position;
        transform.rotation = Quaternion.LookRotation(directionToCamera);
        transform.Rotate(transform.rotation.x + 90, transform.rotation.y, transform.rotation.z); 
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
