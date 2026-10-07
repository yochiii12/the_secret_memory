using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteraccionJugador : MonoBehaviour
{
    public float rangoInteraccion = 1f;
    public LayerMask capaInteractuable;

    private bool puedeUsarX = true;
    private float cooldownX = 30f;

    private bool puedeInteractuar = true;

    // ← NUEVO: Objetos que aparecerán cuando presionas X
    public List<GameObject> objetosOcultos;

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

    void RevisarInteraccion()
    {
        if (!puedeInteractuar) return;

        Collider2D col = Physics2D.OverlapCircle(transform.position, rangoInteraccion, capaInteractuable);

        if (col != null)
        {
            Interactuable interactuable = col.GetComponent<Interactuable>();
            if (interactuable != null)
            {
                puedeInteractuar = false;
                interactuable.Interactuar();
            }
        }
    }

    public void DesbloquearInteraccion()
    {
        puedeInteractuar = true;
    }

    IEnumerator ResaltarInteractuables()
    {
        // 1. Resaltar interactuables
        Interactuable[] objetos = Object.FindObjectsByType<Interactuable>(FindObjectsSortMode.None);

        foreach (Interactuable obj in objetos)
            obj.CambiarColor(Color.yellow);

        // 2. Activar objetos ocultos
        foreach (GameObject obj in objetosOcultos)
            obj.SetActive(true);

        // Esperar 3 segundos
        yield return new WaitForSeconds(3f);

        // 3. Restaurar color
        foreach (Interactuable obj in objetos)
            obj.RestaurarColor();

        // 4. Ocultar objetos otra vez
        foreach (GameObject obj in objetosOcultos)
            obj.SetActive(false);
    }

    IEnumerator CooldownX()
    {
        puedeUsarX = false;
        yield return new WaitForSeconds(cooldownX);
        puedeUsarX = true;
    }
}
