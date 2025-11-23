using UnityEngine;

public class TouchExplosiveBehavior : ITouchBehaviour
{
    private Collider[] colliders = new Collider[20];
    private LayerMask _layerMask;
    private float _explosionPower;
    private float _explosionRadius;
    private GameObject _explosionPrefab;

    public TouchExplosiveBehavior(GameObject explosionPrefab, LayerMask layerMask, float explosionPower, float explosionRadius)
    {
        _explosionPrefab = explosionPrefab;
        _layerMask = layerMask;
        _explosionPower = explosionPower;
        _explosionRadius = explosionRadius;
    }

    public void Start(RaycastHit hit)
    {

    }

    public void Execute(RaycastHit explosionCenter)
    {
        GameObject.Instantiate(_explosionPrefab, explosionCenter.point, Quaternion.identity);

        if (Physics.OverlapSphereNonAlloc(explosionCenter.point, _explosionRadius, colliders, _layerMask.value) > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider == null)
                    continue;

                if (collider.TryGetComponent<IExplosible>(out IExplosible explosible)) {
                    Vector3 directionNormalized = (collider.transform.position - explosionCenter.point).normalized;
                    explosible.BlowUp(directionNormalized, _explosionPower);
                }
            }
        }
    }

    public bool IsExit()
    {
        return true;
    }
}