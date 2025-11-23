using UnityEngine;

public class Toucher 
{
    private InputsListener _inputsListener;
    private Camera _camera;
    private bool _isEffectActive;
    private ITouchBehaviour _touchBehaviour;
    private MouseButtons _buttonActivate;

    public Toucher(InputsListener inputsListener, Camera camera, MouseButtons buttonActivate, ITouchBehaviour touchBehaviour)
    {
        _inputsListener = inputsListener;
        _buttonActivate = buttonActivate;
        _touchBehaviour = touchBehaviour;
        _camera = camera;
    }

    public void Touch()
    {
        if (_inputsListener.IsAnyButtonPressed() == false)
            return;

        if (_inputsListener.IsMouseButtonDown(_buttonActivate))
        {
            _isEffectActive = true;
            _touchBehaviour.Start(GetHit());
        }
    }

    public void Effect()
    {
        if (_isEffectActive == false)
            return;

        _isEffectActive = !_touchBehaviour.IsExit();
        _touchBehaviour.Execute(GetHit());
        
    }

    public RaycastHit GetHit()
    {
        Ray ray = _camera.ScreenPointToRay(_inputsListener.GetMousePosition());
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }
}