using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour, IInteractable
{
    [SerializeField] private float posInterpolationSpeed;
    private bool _pickedUp;
    private Rigidbody _rb;
    private Transform _holdTransform;

    public void OnClick()
    {
        _pickedUp = true;
    }

    public void OnRelease()
    {
        _pickedUp = false;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _holdTransform = PlayerActions.instance.holdTransform;
    }

    private void FixedUpdate()
    {
        if (_pickedUp)
        {
            _rb.MovePosition(Vector3.Lerp(transform.position, _holdTransform.position, 0.25f));
        }

    }


}
