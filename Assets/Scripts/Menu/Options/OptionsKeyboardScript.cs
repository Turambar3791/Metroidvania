using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OptionsKeyboardScript : MonoBehaviour
{
    public InputAction anyKeyAction;

    private void OnEnable()
    {
        anyKeyAction.Enable();
        anyKeyAction.performed += OnKeyPress;
    }

    private void OnDisable()
    {
        anyKeyAction.performed -= OnKeyPress;
        anyKeyAction.Disable();
    }

    private void OnKeyPress(InputAction.CallbackContext context)
    {
        Debug.Log($"Naci�ni�to: {context.control.displayName}");
    }

    public void BackToOptions()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
