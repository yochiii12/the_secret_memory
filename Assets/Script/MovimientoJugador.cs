using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

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


