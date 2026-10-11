using UnityEngine;

public class MovimientoJugador : MonoBehaviour // Usar inglés
{

    public float speed = 5f; // Usar PascalCase

    private Rigidbody2D rb; // Usar _camelCase; Sin abreviaciones
    private Vector2 movement; // Usar _camelCase

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Leer entrada del jugador 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizar para que no corra más en diagonal
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Aplicar movimiento 
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}


