using UnityEngine;

public class TouchMoveBehavior : ITouchBehaviour
{
    private readonly InputsListener _inputs;
    private readonly Camera _camera;
    private readonly MouseButtons _button;

    private IDraggable _item;
    private Plane _dragPlane;
    private bool _dragging;

    public TouchMoveBehavior(InputsListener inputs, Camera camera, MouseButtons button)
    {
        _inputs = inputs;
        _camera = camera;
        _button = button;
    }

    public void Start(RaycastHit hit)
    {
        if (!hit.collider.TryGetComponent(out _item))
            return;

        _item.StartDragging(hit.point);
        _dragging = true;

        _dragPlane = new Plane(-_camera.transform.forward, hit.point);
    }

    public void Execute(RaycastHit hit)
    {
        if (_dragging && _item != null)
            Drag();
    }

    public bool IsExit()
    {
        if (!_inputs.IsMouseButtonUp(_button))
            return false;

        _item = null;
        _dragging = false;
        return true;
    }

    private void Drag()
    {
        Ray ray = _camera.ScreenPointToRay(_inputs.GetMousePosition());

        if (_dragPlane.Raycast(ray, out float enter))
            _item.Dragging(ray.GetPoint(enter));
    }
}
