using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsControllers : MonoBehaviour
{
    public InputActionMap UI;

    public static InputActionsControllers Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        UI.Enable();
    }

    private void OnDisable()
    {
        UI.Disable();
    }
}