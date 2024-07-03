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

            Debug.Log("Flecha colisionó con el límite y se destruirá.");
            Debug.Log("Flecha position: " + transform.position);

            Destroy(gameObject);

            gameController.ResetCombo();
            Debug.Log("Flecha alcanzó el Boundary, disminuyendo barra de versus");


            return; 
        }
        if (other.gameObject.CompareTag("EnemyLImit")) // Reemplaza "SpecialTag" con el tag específico que necesitas
        {

            float reductionAmount = 0.05f; // Disminuye un 5%
            versusBarController.UpdateScore(-reductionAmount, true); // Special collision, no puede llegar a 0%

            Debug.Log("Flecha colisionó con SpecialTag y la puntuación de BF se reduce al mínimo del 5%.");

            Destroy(gameObject);
            Debug.Log("Flecha alcanzó el SpecialTag, disminuyendo la barra de versus a un valor mínimo");
            return;
        }
    }

    public void CalculateScore(float currentY)
    {
        if (isHit) return;
       
        if (currentY >= 2481f && currentY < 2500f)
        {
            Debug.Log("Perfecto! Puntuación: 20");
            gameController.AddScore(20);
            gameController.ShowMessage("Fatal!");
        }
        else if (currentY >= 2451f && currentY < 2480f)
        {
            Debug.Log("Fatal! Puntuación: 50");
            gameController.AddScore(50);
            gameController.ShowMessage("Regular!");

        }
        else if (currentY >= 2410f && currentY < 2450f)
        {
            Debug.Log("Regular! Puntuación: 60");
            gameController.AddScore(60);
            gameController.ShowMessage("Bien!");

        }
        else if (currentY >= 2400f && currentY < 2440f)
        {
            Debug.Log("Perfecto! Puntuación: 100");
            gameController.AddScore(100);
            gameController.ShowMessage("Perfecto!");

        }
        else if (currentY >= 2360f && currentY < 2399f)
        {
            Debug.Log("Bueno! Puntuación: 50");
            gameController.AddScore(50);
            gameController.ShowMessage("Bien!");


        }
        else if (currentY >= 2350f && currentY < 2359.99f)
        {
            Debug.Log("Regular! Puntuación: 60");
            gameController.AddScore(60);
            gameController.ShowMessage("Regular!");

        }
        else if (currentY >= 2300f && currentY < 2349f)
        {
            Debug.Log("Fatal! Puntuación: 20");
            gameController.AddScore(20);
            gameController.ShowMessage("Fatal!");

        }
        else
        {
            Debug.Log("Fallo! Puntuación: 0");
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





