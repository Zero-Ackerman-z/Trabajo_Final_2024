using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private ActionsControllers controls;
    public GameController gameController;

   // public GraphControl graphControl;
   // public NodeControl currentNode;
    // Referencia al ScriptableObject
    public GameModeSettings gameModeSettings;
    public ArrowSpriteController arrowLeft;
    public ArrowSpriteController arrowRight;
    public ArrowSpriteController arrowUp;
    public ArrowSpriteController arrowDown;
    private bool isDFJKPanelEnabled = false; // Variable de estado

    private void Awake()
    {
        controls = new ActionsControllers();
        controls.GameUI.Select.performed += ctx => OnSelectPressed(ctx);
        controls.GameUI.Back.performed += ctx => OnBackPressed(ctx);
        controls.PaneController.DFJKPanel.performed += ctx => OnDFJKPanelPressed(ctx);

        // Asignar las acciones en base al modo de juego actual
        AssignActions();
    }
    private void OnEnable()
    {
        controls.Game.Enable();
        controls.PaneController.Enable();

        controls.GameUI.Enable();
    }

    private void OnDisable()
    {
        controls.Game.Disable();
        controls.PaneController.Disable();
        controls.GameUI.Disable();
    }
    void Start()
    {

        if (arrowLeft != null) arrowLeft.SetNormalSprite();
        if (arrowRight != null) arrowRight.SetNormalSprite();
        if (arrowUp != null) arrowUp.SetNormalSprite();
        if (arrowDown != null) arrowDown.SetNormalSprite();
    }

    private void AssignActions()
    {
        // Obtener las acciones para el modo actual
        InputActionReference[] currentActions = gameModeSettings.GetCurrentModeActions();

        // Iterar sobre las acciones y asignarlas
        for (int i = 0; i < currentActions.Length; i++)
        {
            InputActionReference action = currentActions[i];

            switch (action.action.name)
            {
            case "D":
                controls.Game.D.performed += OnDPressed;
                controls.Game.D.canceled += OnDReleased;
                break;
            case "F":
                controls.Game.F.performed += OnFPressed;
                controls.Game.F.canceled += OnFReleased;
                break;
            case "J":
                controls.Game.J.performed += OnJPressed;
                controls.Game.J.canceled += OnJReleased;
                break;
            case "K":
                controls.Game.K.performed += OnKPressed;
                controls.Game.K.canceled += OnKReleased;
                break;
            case "A":
                controls.Game.A.performed += OnAPressed;
                controls.Game.A.canceled += OnAReleased;
                break;
            case "S":
                controls.Game.S.performed += OnSPressed;
                controls.Game.S.canceled += OnSReleased;
                break;
            case "W":
                controls.Game.W.performed += OnWPressed;
                controls.Game.W.canceled += OnWReleased;
                break;
            case "D1":
                controls.Game.D1.performed += OnD1Pressed;
                controls.Game.D1.canceled += OnD1Released;
                break;

                case "FlechasLeft":
                controls.Game.FlechasLeft.performed += OnFlechasLeftPressed;
                controls.Game.FlechasLeft.canceled += OnFlechasLeftReleased;
                break;
            case "FlechasUp":
                controls.Game.FlechasUp.performed += OnFlechasUpPressed;
                controls.Game.FlechasUp.canceled += OnFlechasUpReleased;
                break;
            case "FlechasDown":
                controls.Game.FlechasDown.performed += OnFlechasDownPressed;
                controls.Game.FlechasDown.canceled += OnFlechasDownReleased;
                break;
            case "FlechasRight":
                controls.Game.FlechasRight.performed += OnFlechasRightPressed;
                controls.Game.FlechasRight.canceled += OnFlechasRightReleased;
                break;
            default:
                Debug.LogWarning("Action " + action.action.name + " not assigned.");
                break;
            }
        }
    }

    private void OnDPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button D Pressed");
        arrowLeft.SetPressedSprite();
        CheckHit("Left");

    }

    private void OnFPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button F Pressed");
        arrowUp.SetPressedSprite();
        CheckHit("Up");


    }

    private void OnJPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button J Pressed");
        arrowDown.SetPressedSprite();
        CheckHit("Down");



    }

    private void OnKPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button K Pressed");
        arrowRight.SetPressedSprite();
        CheckHit("Right");



    }

    private void OnAPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button A Pressed");
        arrowLeft.SetPressedSprite();
        CheckHit("Left");


    }
    private void OnSPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button S Pressed");
        arrowDown.SetPressedSprite();
        CheckHit("Down");


    }
    private void OnWPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button W Pressed");
        arrowUp.SetPressedSprite();
        CheckHit("Up");


    }
    private void OnD1Pressed(InputAction.CallbackContext context)
    {

        arrowRight.SetPressedSprite();
        CheckHit("Right");




    }
    private void OnFlechasLeftPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Flechas Left Pressed");
        arrowLeft.SetPressedSprite();
        CheckHit("Left");

    }
    private void OnFlechasUpPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Flechas Up Pressed");
        arrowUp.SetPressedSprite();
        CheckHit("Up");

    }
    private void OnFlechasDownPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Flechas Down Pressed");
        arrowDown.SetPressedSprite();
        CheckHit("Down");

    }
    private void OnFlechasRightPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Flechas Right Pressed");
        arrowRight.SetPressedSprite();
        CheckHit("Right");

    }
    private void OnSelectPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Select Pressed");
    }
    private void OnBackPressed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    

    private void OnDFJKPanelPressed(InputAction.CallbackContext context)
    {
    }
    private void OnDReleased(InputAction.CallbackContext context)
    {
        arrowLeft.SetNormalSprite();
    }

    private void OnFReleased(InputAction.CallbackContext context)
    {
        arrowUp.SetNormalSprite();
    }

    private void OnJReleased(InputAction.CallbackContext context)
    {
        arrowDown.SetNormalSprite();
    }

    private void OnKReleased(InputAction.CallbackContext context)
    {
        arrowRight.SetNormalSprite();
    }

    private void OnAReleased(InputAction.CallbackContext context)
    {
        arrowLeft.SetNormalSprite();
    }

    private void OnSReleased(InputAction.CallbackContext context)
    {
        arrowDown.SetNormalSprite();
    }

    private void OnWReleased(InputAction.CallbackContext context)
    {
        arrowUp.SetNormalSprite();
    }
    private void OnD1Released(InputAction.CallbackContext context)
    {
        arrowRight.SetNormalSprite();
    }

    private void OnFlechasLeftReleased(InputAction.CallbackContext context)
    {
        arrowLeft.SetNormalSprite();
    }

    private void OnFlechasUpReleased(InputAction.CallbackContext context)
    {
        arrowUp.SetNormalSprite();
    }

    private void OnFlechasDownReleased(InputAction.CallbackContext context)
    {
        arrowDown.SetNormalSprite();
    }

    private void OnFlechasRightReleased(InputAction.CallbackContext context)
    {
        arrowRight.SetNormalSprite();
    }
    // Cambiar el modo de juego
    public void ChangeGameMode(int direction)
    {
        gameModeSettings.ChangeMode(direction);
        AssignActions();
    }
    
    private void CheckHit(string direction)
    {
        Arrow[] arrows = FindObjectsOfType<Arrow>();

        for (int i = 0; i < arrows.Length; i++)
        {
            Arrow arrow = arrows[i];

            if (arrow.CompareTag(direction))
            {
                if (arrow.transform.localPosition.y >= 640f && arrow.transform.localPosition.y < 800f)
                {
                    arrow.MarkAsHit();
                    switch (direction)
                    {
                        case "Left":
                            arrowLeft.SetHitSprite();
                            break;
                        case "Right":
                            arrowRight.SetHitSprite();
                            break;
                        case "Up":
                            arrowUp.SetHitSprite();
                            break;
                        case "Down":
                            arrowDown.SetHitSprite();
                            break;
                    }
                    break; // Salir del bucle una vez que se encuentre y destruya la flecha adecuada
                }
            }
        }
    }
    public void EnableDFJKPanel()
    {
        if (!isDFJKPanelEnabled)
        {
            controls.PaneController.Enable();
            isDFJKPanelEnabled = true;
            Debug.Log("DFJK Panel enabled");
        }
    }
    public void DisableDFJKPanel()
    {
        if (isDFJKPanelEnabled)
        {
            controls.PaneController.Disable();
            isDFJKPanelEnabled = false;
            Debug.Log("DFJK Panel disabled");
        }
    }

}

