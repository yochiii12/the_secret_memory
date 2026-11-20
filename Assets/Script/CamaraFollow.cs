using UnityEngine;

public class CamaraFollow : MonoBehaviour // Usar inglés
{
    public Transform objetivo; // Usar inglés | Usar PascalCase
    public float suavizado = 5f; // Usar inglés | Usar PascalCase

    void LateUpdate()
    {
        if (objetivo == null) return; // Agregar llaves al if

        Vector3 posicionDeseada = new Vector3( // Usar inglés
            objetivo.position.x,
            objetivo.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavizado * Time.deltaTime
        );
    }
}
