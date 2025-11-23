using UnityEngine;

public class Rotator
{
    private Rigidbody _rigidbody;
    private float _rotationForce;

    public Rotator(Rigidbody rigidbody, float rotationForce)
    {
        _rigidbody = rigidbody;
        _rotationForce = rotationForce;
    }

    public void Rotate()
    {
        Vector3 torqueDirection = Vector3.Cross(_rigidbody.transform.up, Vector3.up);
        _rigidbody.AddTorque(torqueDirection * _rotationForce, ForceMode.Acceleration);
    }
}