using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float speed;
    private float speedSprint;
    private float jumpForce;
    private float groundRayLength;
    private LayerMask groundLayerMask;
    private float speedCurrent;
    private Vector3 dir;
    public Rigidbody rb;
    private Ray groundRay;
    public static bool _board;
    private Transform transform;
    public Player(float speed, float speedSprint, float jumpForce, float groundRayLength, LayerMask groundLayerMask, Rigidbody rb, Transform transform)
    {
        this.speed = speed;
        this.speedSprint = speedSprint;
        this.jumpForce = jumpForce;
        this.groundRayLength = groundRayLength;
        this.groundLayerMask = groundLayerMask;
        this.rb = rb;
        this.transform = transform;
    }

    public void OnAwake()
    {
        speedCurrent = speed;
        _board = false;
    }

    public void OnUpdate()
    {
        if (_board)
        {
            return;
        }
        dir = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;
        dir.Normalize();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sprint();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedCurrent = speed;
        }
    }

    public void OnFixedUpdate()
    {
        if (_board)
        {
            return;
        }
        if (rb != null)
        {
            rb.velocity = dir * speedCurrent + Vector3.up * rb.velocity.y;
        }
    }

    private void Jump()
    {
        if (IsGrounded() == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Sprint()
    {
        speedCurrent *= speedSprint;
    }

    private bool IsGrounded()
    {
        groundRay = new Ray(transform.position, -transform.up);
        return Physics.Raycast(groundRay, groundRayLength, groundLayerMask);
    }
}
