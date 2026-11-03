// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteraccionJugador : MonoBehaviour // Usar inglés
{
    public float rangoInteraccion = 1f; // Usar PascalCase; Usar inglés
    public LayerMask capaInteractuable; // Usar PascalCase; Usar inglés

    private bool puedeUsarX = true; // Usar _camelCase; Usar inglés
    private float cooldownX = 30f; // Usar _camelCase

    private bool puedeInteractuar = true; // Usar _camelCase; Usar inglés

    // ← NUEVO: Objetos que aparecerán cuando presionas X
    public List<GameObject> objetosOcultos; // Usar PascalCase; Respetar el orden de acceso y static; Usar inglés

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RevisarInteraccion();
        }

        if (Input.GetKeyDown(KeyCode.X) && puedeUsarX)
        {
            StartCoroutine(ResaltarInteractuables());
            StartCoroutine(CooldownX());
        }
    }

    void RevisarInteraccion() // Usar inglés
    {
        if (!puedeInteractuar) return; // Agregar llaves al if

        Collider2D col = Physics2D.OverlapCircle(transform.position, rangoInteraccion, capaInteractuable); // Sin abreviaciones

        if (col != null) // Revisar si se puede usar retorno temprano
        {
            Interactuable interactuable = col.GetComponent<Interactuable>(); // Usar inglés
            if (interactuable != null)
            {
                puedeInteractuar = false;
                interactuable.Interactuar();
            }
        }
    }

    public void DesbloquearInteraccion() // Respetar el orden de acceso y static; Usar inglés
    {
        puedeInteractuar = true;
    }

    IEnumerator ResaltarInteractuables() // Usar inglés
    {
        // 1. Resaltar interactuables
        Interactuable[] objetos = Object.FindObjectsByType<Interactuable>(FindObjectsSortMode.None); // Usar inglés

        foreach (Interactuable obj in objetos)
            obj.CambiarColor(Color.yellow);

        // 2. Activar objetos ocultos 
        foreach (GameObject obj in objetosOcultos)
            obj.SetActive(true);

        // Esperar 3 segundos 
        yield return new WaitForSeconds(3f); // Revisar: usar una constante o configuración con nombre

        // 3. Restaurar color 
        foreach (Interactuable obj in objetos)
            obj.RestaurarColor();

        // 4. Ocultar objetos otra vez 
        foreach (GameObject obj in objetosOcultos)
            obj.SetActive(false);
    }

    IEnumerator CooldownX() // Usar un verbo en el nombre del método
    {
        puedeUsarX = false;
        yield return new WaitForSeconds(cooldownX);
        puedeUsarX = true;
    }
}
