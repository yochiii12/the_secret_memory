using UnityEngine;

public class PlayerMovement : MonoBehaviour // Revisar: PlayerMovement también está declarada en MovimientoJugador.cs
{
    public float Speed = 5f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _movementDirection;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _movementDirection.x = Input.GetAxisRaw("Horizontal");
        _movementDirection.y = Input.GetAxisRaw("Vertical");

        if (_movementDirection.x != 0)
        {
            _movementDirection.y = 0;
        }

        _movementDirection = _movementDirection.normalized;

        if (_movementDirection != Vector2.zero)
        {
            _animator.SetFloat("moveX", _movementDirection.x);
            _animator.SetFloat("moveY", _movementDirection.y);
        }

        bool isMoving = _movementDirection != Vector2.zero;
        _animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movementDirection * Speed * Time.fixedDeltaTime);
    }
}