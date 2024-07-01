using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public abstract class ButtonNavigationBase : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] buttons;

    [Header("Selected Button Settings")]
    public int selectedIndex = 0;
    public bool enableAnimation = true;

    [Header("Transition Manager")]
    public TransitionManager transitionManager;

    protected bool isTransitioning = false;

    private ActionsControllers inputActions;

    protected virtual void Awake()
    {
        inputActions = new ActionsControllers();
    }

    protected virtual void OnEnable()
    {
        inputActions.UI.Navigate.performed += OnNavigate;
        inputActions.UI.Select.performed += OnSelect;
        inputActions.UI.Back.performed += OnBack;
        inputActions.Enable();
    }

    protected virtual void OnDisable()
    {
        inputActions.UI.Navigate.performed -= OnNavigate;
        inputActions.UI.Select.performed -= OnSelect;
        inputActions.UI.Back.performed -= OnBack;
        inputActions.Disable();
    }

    protected virtual void Start()
    {
        SelectButton(selectedIndex, true);
    }

    protected abstract void OnNavigate(InputAction.CallbackContext context);
    protected abstract void OnSelect(InputAction.CallbackContext context);
    protected abstract void OnBack(InputAction.CallbackContext context);

    protected void SelectButton(int index, bool isSelected)
    {
        if (index < 0 || index >= buttons.Length) return;

        ButtonAnimation animation = buttons[index].GetComponent<ButtonAnimation>();
        if (animation != null)
        {
            animation.AnimateButton(isSelected);
        }
    }

    protected void HideOtherButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != selectedIndex)
            {
                ButtonAnimation animation = buttons[i].GetComponent<ButtonAnimation>();
                if (animation != null)
                {
                    animation.HideButton();
                }
            }
        }
    }

    protected void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
