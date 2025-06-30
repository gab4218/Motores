using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// Gabriel Benes
public class Blocks : MonoBehaviour, IInteractable
{
    [SerializeField] private float posInterpolationSpeed;
    public bool isHeld;
    public bool canBeHeldByEnemy {  get; private set; } = true;
    public Rigidbody _rb {  get; private set; }
    public Transform carrierTransform;
    [SerializeField] public Board.BoardType type;
    [SerializeField] public int value;

    public void OnClick()
    {
        isHeld = true;
        _rb.useGravity = false;
        PlayerActions.instance.heldItemTransform = transform;
        canBeHeldByEnemy = false;
        carrierTransform = PlayerActions.instance.holdTransform;
        Invoke("HoldWait", 0.5f);
    }

    public void OnRelease()
    {
        if (carrierTransform == PlayerActions.instance.holdTransform)
        { 
            isHeld = false;
            _rb.useGravity = true;
            PlayerActions.instance.heldItemTransform = null;
            canBeHeldByEnemy = true;
        }
    }
    private void HoldWait()
    {
        canBeHeldByEnemy = true;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (isHeld)
        {
            _rb.MovePosition(Vector3.Lerp(transform.position, carrierTransform.position, 0.25f));
        }
        
        
    }

    

}
