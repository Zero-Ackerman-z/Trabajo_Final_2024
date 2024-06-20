using UnityEngine;

public class Nota : MonoBehaviour
{
    public float time; // Tiempo en el que aparece la nota
    public int type; // Tipo de nota (por ejemplo: 0 = flecha, 1 = bomba, etc.)

    public Nota(float time, int type)
    {
        this.time = time;
        this.type = type;
    }
}
