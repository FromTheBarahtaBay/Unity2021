using UnityEngine;

public interface ITouchBehaviour
{
    void Start(RaycastHit hit);
    void Execute(RaycastHit hit);
    bool IsExit();
}