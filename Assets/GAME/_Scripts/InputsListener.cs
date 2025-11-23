using UnityEngine;

public class InputsListener
{
    public InputsListener()
    {

    }

    public bool IsAnyButtonPressed()
    {
        return Input.anyKey;
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public bool IsMouseButtonDown(MouseButtons mouseButton)
    {
        return Input.GetMouseButtonDown((int)mouseButton);
    }

    public bool IsMouseButtonHold(MouseButtons mouseButton)
    {
        return Input.GetMouseButton((int)mouseButton);
    }

    public bool IsMouseButtonUp(MouseButtons mouseButton)
    {
        return Input.GetMouseButtonUp((int)mouseButton);
    }

    public bool IsRestartButttonPressed()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}