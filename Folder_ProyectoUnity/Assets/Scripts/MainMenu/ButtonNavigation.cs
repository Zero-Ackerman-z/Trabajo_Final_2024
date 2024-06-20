using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class ButtonNavigation : MonoBehaviour
{
    public Button[] buttons; // Arreglo de botones en tu MainMenuScene
    private int selectedIndex = 0;
    private InputAcctionsControllers inputActions;

    [SerializeField] private float originalButtonScale = 2f;
    [SerializeField] private float scaledButtonScale = 2.2f;
    [SerializeField] private float moveDistance = 30f; // Distancia de movimiento a la derecha

    // Arreglo para almacenar las posiciones originales de los botones
    private Vector3D[] originalPositions;
    public BackgroundTransition backgroundTransition;

    private void Awake()
    {
        inputActions = new InputAcctionsControllers();
        originalPositions = new Vector3D[buttons.Length];
    }

    private void OnEnable()
    {
        inputActions.UI.Navigate.performed += OnNavigate;
        inputActions.UI.Select.performed += OnSelect;
        inputActions.UI.Back.performed += OnBack;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Navigate.performed -= OnNavigate;
        inputActions.UI.Select.performed -= OnSelect;
        inputActions.UI.Back.performed -= OnBack;
        inputActions.Disable();
    }

    private void Start()
    {
        // Guardar las posiciones originales de los botones
        for (int i = 0; i < buttons.Length; i++)
        {
            originalPositions[i] = Vector3D.FromVector3(buttons[i].transform.localPosition);
        }

        SelectButton(selectedIndex); // Seleccionar el primer botón al inicio
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 navigationInput = context.ReadValue<Vector2>();
        float verticalInput = navigationInput.y;

        if (verticalInput != 0)
        {
            selectedIndex = (selectedIndex + (verticalInput > 0 ? -1 : 1) + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex);
            AudioManager.Instance?.PlayNavigateSFX();

        }
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        buttons[selectedIndex].onClick.Invoke(); // Invocar el evento de click del botón seleccionado
        AudioManager.Instance?.PlaySelectSFX();
        //backgroundTransition?.StartTransitionWithCallback;

    }

    private void OnBack(InputAction.CallbackContext context)
    {
        // Retroceder a la pantalla SplashScreenScene al presionar Escape
        // Aquí asumo que manejas las escenas de Unity de manera apropiada
        if (context.performed)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreenScene");
        }
    }

    private void SelectButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            // Escalar el botón seleccionado y volver al estado normal los demás
            float scale = (i == index) ? scaledButtonScale : originalButtonScale;
            buttons[i].transform.DOScale(scale, 0.2f);

            // Restaurar la posición original del botón no seleccionado
            if (i != index)
            {
                buttons[i].transform.DOLocalMove(originalPositions[i].ToUnityVector3(), 0.2f);
            }
        }

        Vector3D targetPosition = new Vector3D(originalPositions[index].X + moveDistance, originalPositions[index].Y, originalPositions[index].Z);
        buttons[index].transform.DOLocalMoveX(targetPosition.X, 0.2f);
    }
}
