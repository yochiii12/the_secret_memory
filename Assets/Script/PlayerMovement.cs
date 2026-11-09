using UnityEngine;

public class PlayerMovement : MonoBehaviour // Revisar: PlayerMovement también está declarada en MovimientoJugador.cs
{
    public float Speed = 5f;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementDirection;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movementDirection.x = Input.GetAxisRaw("Horizontal");
        _movementDirection.y = Input.GetAxisRaw("Vertical");

        _movementDirection = _movementDirection.normalized;
    }

    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movementDirection * Speed * Time.fixedDeltaTime);
    }
}

