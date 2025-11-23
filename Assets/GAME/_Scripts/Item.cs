using UnityEngine;

public class Item : MonoBehaviour, IExplosible, IDraggable
{
    [SerializeField] private float _moveForce = 15f;
    [SerializeField] private float _rotationForce = 20f;

    private Rigidbody _rigidbody;
    private Vector3 _offset;

    private Mover _mover;
    private Rotator _rotator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mover = new Mover(_rigidbody, _moveForce);
        _rotator = new Rotator(_rigidbody, _rotationForce);
    }

    public void BlowUp(Vector3 direction, float blowPower)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * blowPower, ForceMode.Impulse);
    }

    public void StartDragging(Vector3 hitPoint)
    {
        _rigidbody.isKinematic = false;
        _offset = transform.position - hitPoint;
        _offset.z = 0;
    }

    public void Dragging(Vector3 position)
    {
        Move(position);

        Rotate();
    }

    private void Move(Vector3 position)
    {
        Vector3 newPosition = position + _offset;
        Vector3 target = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        Vector3 direction = (target - transform.position);

        _mover.Move(direction);
    }

    private void Rotate()
    {
        _rotator.Rotate();
    }
}