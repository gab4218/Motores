using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _POVTransform;
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;
    private float _mouseX;
    private float _mouseY;
    private float _rotX;
    private float _rotY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        _mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensitivityX;
        _mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensitivityY;
        _rotX -= _mouseY;
        _rotY += _mouseX;
        _rotX = Mathf.Clamp(_rotX, -90f, 90f);

        if (Input.GetKeyDown(KeyCode.H)) _sensitivityX -= 50;
        if (Input.GetKeyDown(KeyCode.J)) _sensitivityX += 50;
        if (Input.GetKeyDown(KeyCode.K)) _sensitivityY -= 50;
        if (Input.GetKeyDown(KeyCode.L)) _sensitivityY += 50;

        transform.rotation = Quaternion.Euler(_rotX, _rotY, 0);
        transform.position = _POVTransform.position;
        _playerTransform.rotation = Quaternion.Euler(0, _rotY, 0);
    }
}
