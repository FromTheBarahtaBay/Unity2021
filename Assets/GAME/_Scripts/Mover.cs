using UnityEngine;

public class Mover
{
    private Rigidbody _rigidbody;
    private float _moveForce;

    public Mover(Rigidbody rigidbody, float moveForce)
    {
        _rigidbody = rigidbody;
        _moveForce = moveForce;
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * _moveForce;
    }
}