using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _POVTransform;
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;
    public static CameraScript instance { get; private set; }
    private bool _isOnBoard;
    public bool isOnBoard
    {
        get
        {
            return _isOnBoard;
        }
        set
        {
            _isOnBoard = value;
            if (interpolationCR != null)
            {
                StopCoroutine(interpolationCR);
                interpolationCR = StartCoroutine(InterpolateCameraView());
            }
            else
            {
                interpolationCR = StartCoroutine(InterpolateCameraView());
            }
        }

    }
    public Transform targetPos;
    private bool _isInterpolating = false;
    private float _mouseX;
    private float _mouseY;
    private float _rotX;
    private float _rotY;
    private Coroutine interpolationCR;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        targetPos = PlayerActions.instance.transform;
    }

    private void Update()
    {
        if (_isInterpolating)
        {
            return;
        }
        else if (isOnBoard)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                targetPos = _POVTransform;
                isOnBoard = false;
            }
        }
        else
        {
            MoveCamera();
        }
    }

    private IEnumerator InterpolateCameraView()
    {
        float t = 0;
        Vector3 otp = transform.position;
        Quaternion otr = transform.rotation;
        _isInterpolating = true;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(otp, targetPos.position, t);
            transform.rotation = Quaternion.Lerp(otr, targetPos.rotation, t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos.position;
        transform.rotation = targetPos.rotation;
        _isInterpolating = false;
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
        PlayerActions.instance.transform.rotation = Quaternion.Euler(0, _rotY, 0);
    }
    
}
