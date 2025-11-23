using UnityEngine;

public interface IDraggable 
{
    void StartDragging(Vector3 hitPoint);
    void Dragging(Vector3 hitPoint);
}