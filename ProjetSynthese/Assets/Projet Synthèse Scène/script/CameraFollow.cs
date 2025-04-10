using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;
    private Vector3 _cornerA = new Vector3(-7f, 3f, -23f);
    private Vector3 _cornerB = new Vector3(-13f, 23f, -49f);
    private Vector3 _offset;
    private Vector3 _controlPosition;

    void Start()
    {
        _offset = _camera.position - _player.position;
    }

    void Update()
    {
        _camera.position = _player.position + _offset;
        Vector3 _currentPosition = _camera.position;
        _currentPosition.x = Mathf.Clamp(_currentPosition.x, _cornerB.x, _cornerA.x);
        _currentPosition.z = Mathf.Clamp(_currentPosition.z, _cornerB.z, _cornerA.z);
        _controlPosition = new Vector3(_currentPosition.x, _currentPosition.y, _currentPosition.z);
        _camera.position = _controlPosition;
    }
}
