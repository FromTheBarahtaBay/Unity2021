using UnityEngine;

public class TouchMoveBehavior : ITouchBehaviour
{
    private readonly InputsListener _inputsListener;
    private readonly Camera _camera;
    private readonly MouseButtons _buttonKey;

    private IDraggable _item;
    private Plane _dragPlane;
    private bool _isDragging;

    public TouchMoveBehavior(InputsListener inputs, Camera camera, MouseButtons button)
    {
        _inputsListener = inputs;
        _camera = camera;
        _buttonKey = button;
    }

    public void Start(RaycastHit hit)
    {
        if (!hit.collider.TryGetComponent(out _item))
            return;

        _item.StartDragging(hit.point);
        _isDragging = true;

        _dragPlane = new Plane(-_camera.transform.forward, hit.point);
    }

    public void Execute(RaycastHit hit)
    {
        if (_isDragging && _item != null)
            Drag();
    }

    public bool IsExit()
    {
        if (_inputsListener.IsMouseButtonDown(_buttonKey) || _inputsListener.IsMouseButtonHold(_buttonKey))
            return false;

        _item = null;
        _isDragging = false;
        return true;
    }

    private void Drag()
    {
        Ray ray = _camera.ScreenPointToRay(_inputsListener.GetMousePosition());

        if (_dragPlane.Raycast(ray, out float enter))
            _item.Dragging(ray.GetPoint(enter));
    }
}