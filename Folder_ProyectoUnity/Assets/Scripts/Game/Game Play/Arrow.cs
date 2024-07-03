using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed ; 
    private GameController gameController; 
    private RectTransform rectTransform; 
    public bool isHit = false; 
    public VersusBarController versusBarController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rectTransform = GetComponent<RectTransform>();
        versusBarController = FindObjectOfType<VersusBarController>();


    }

    private void Update()
    {
        transform.localPosition = transform.localPosition + Vector3.up * speed * Time.deltaTime;

        float currentY = transform.localPosition.y;        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            versusBarController.UpdateScore(-0.05f); // Disminuye 10%

            Debug.Log("Flecha colision� con el l�mite y se destruir�.");
            Debug.Log("Flecha position: " + transform.position);

            Destroy(gameObject);

            gameController.ResetCombo();
            Debug.Log("Flecha alcanz� el Boundary, disminuyendo barra de versus");


            return; 
        }
        if (other.gameObject.CompareTag("EnemyLImit")) // Reemplaza "SpecialTag" con el tag espec�fico que necesitas
        {

            float reductionAmount = 0.05f; // Disminuye un 5%
            versusBarController.UpdateScore(-reductionAmount, true); // Special collision, no puede llegar a 0%

            Debug.Log("Flecha colision� con SpecialTag y la puntuaci�n de BF se reduce al m�nimo del 5%.");

            Destroy(gameObject);
            Debug.Log("Flecha alcanz� el SpecialTag, disminuyendo la barra de versus a un valor m�nimo");
            return;
        }
    }

    public void CalculateScore(float currentY)
    {
        if (isHit) return;
       
        if (currentY >= 2481f && currentY < 2500f)
        {
            Debug.Log("Perfecto! Puntuaci�n: 20");
            gameController.AddScore(20);
            gameController.ShowMessage("Fatal!");
        }
        else if (currentY >= 2451f && currentY < 2480f)
        {
            Debug.Log("Fatal! Puntuaci�n: 50");
            gameController.AddScore(50);
            gameController.ShowMessage("Regular!");

        }
        else if (currentY >= 2410f && currentY < 2450f)
        {
            Debug.Log("Regular! Puntuaci�n: 60");
            gameController.AddScore(60);
            gameController.ShowMessage("Bien!");

        }
        else if (currentY >= 2400f && currentY < 2440f)
        {
            Debug.Log("Perfecto! Puntuaci�n: 100");
            gameController.AddScore(100);
            gameController.ShowMessage("Perfecto!");

        }
        else if (currentY >= 2360f && currentY < 2399f)
        {
            Debug.Log("Bueno! Puntuaci�n: 50");
            gameController.AddScore(50);
            gameController.ShowMessage("Bien!");


        }
        else if (currentY >= 2350f && currentY < 2359.99f)
        {
            Debug.Log("Regular! Puntuaci�n: 60");
            gameController.AddScore(60);
            gameController.ShowMessage("Regular!");

        }
        else if (currentY >= 2300f && currentY < 2349f)
        {
            Debug.Log("Fatal! Puntuaci�n: 20");
            gameController.AddScore(20);
            gameController.ShowMessage("Fatal!");

        }
        else
        {
            Debug.Log("Fallo! Puntuaci�n: 0");
            gameController.AddScore(0);
        }

        isHit = true;
    }
    public void MarkAsHit()
    {
        if (!isHit) 
        {
            Debug.Log("Flecha acertada, aumentando barra de versus");
            Destroy(gameObject);

            float currentY = transform.localPosition.y;
            CalculateScore(currentY);
            gameController.IncreaseCombo(); 
            versusBarController.UpdateScore(0.05f); 

            Destroy(gameObject);
        }
    }
    
    

}





