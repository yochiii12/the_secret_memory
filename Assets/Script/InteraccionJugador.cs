using UnityEngine;

public class InteraccionJugador : MonoBehaviour
{
    public float rangoInteraccion = 1f;
    public LayerMask capaInteractuable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Botón de acción
        {
            RevisarInteraccion();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E PRESIONADA");
            RevisarInteraccion();
        }
    }

    void RevisarInteraccion()
    {
        // Detecta objetos cerca del jugador
        Collider2D col = Physics2D.OverlapCircle(transform.position, rangoInteraccion, capaInteractuable);

        if (col != null)
        {
            Interactuable interactuable = col.GetComponent<Interactuable>();
            if (interactuable != null)
            {
                interactuable.Interactuar();
            }
        }
    }
    }
