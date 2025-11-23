using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Items Settings")]
    [SerializeField] private LayerMask _interactableLayers;

    [Header("Explosion Settings")]
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _explosionPower = 5;
    [SerializeField] private float _explosionRadius = 6;

    private Toucher _toucherExplosion;
    private Toucher _toucherMover;

    private List<Toucher> _touchers = new List<Toucher>();

    private InputsListener _inputsListener;

    private Camera _camera;

    private void Awake()
    {
        _inputsListener = new InputsListener();
        _camera = Camera.main;

        TouchExplosiveBehavior touchExplosiveBehavior = new TouchExplosiveBehavior(
            _explosionPrefab,
            _interactableLayers,
            _explosionPower,
            _explosionRadius);

        TouchMoveBehavior touchMoveBehavior = new TouchMoveBehavior(
            _inputsListener,
            _camera,
            MouseButtons.LeftMouseButton
            );

        _toucherExplosion = new Toucher(_inputsListener, _camera, MouseButtons.RightMouseButton, touchExplosiveBehavior);
        _toucherMover = new Toucher(_inputsListener, _camera, MouseButtons.LeftMouseButton, touchMoveBehavior);

        _touchers.Add(_toucherExplosion);
        _touchers.Add(_toucherMover);
    }

    private void Update()
    {
        foreach (var toucher in _touchers)
            toucher.Touch();

        if (_inputsListener.IsRestartButttonPressed())
            RestartLevel();
    }

    private void FixedUpdate()
    {
        foreach (var toucher in _touchers)
            toucher.Effect();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}