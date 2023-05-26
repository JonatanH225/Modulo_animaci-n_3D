using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InputController))]
public class Bloqueada : MonoBehaviour
{
    [SerializeField] float _mouseSensibility = 0f;
    [SerializeField] Transform _cameraAnchor = null;

    InputController _inputController = null;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    void Update()
    {
        MouseCamera();
    }

    void MouseCamera()
    {
        Vector2 _input = _inputController.MouseInput();

        transform.Rotate(Vector3.up * _input.x * _mouseSensibility * Time.deltaTime);

        Vector3 angle = _cameraAnchor.eulerAngles;
        angle.x -= _input.y * _mouseSensibility * Time.deltaTime;

        _cameraAnchor.eulerAngles = angle;
    }
}
