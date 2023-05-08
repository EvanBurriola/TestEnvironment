using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]
    [Range(0f, 20f)]
    public float moveSpeed;
    [Range(0f, 20f)]
    public float gravityScale;
    [Range(0f, 20f)]
    public float jumpHeight;
    
    

    public float groundDrag;
    public float airDrag;
    public float jumpDelay;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    float jumpInput;
    float timeSinceLastJump;
    

    Vector3 moveDirection;
    Vector3 gravity;
    Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void Update(){
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + .05F, whatIsGround);
        MyInput();

        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = airDrag;
        }

        //Update the timer
        timeSinceLastJump += Time.fixedDeltaTime;
    }
    
    private void FixedUpdate() {
        gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
        MovePlayer();
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        if (grounded && timeSinceLastJump >= jumpDelay && jumpInput > 0)
        {
            timeSinceLastJump = 0f;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * gravity.y);
            Debug.Log(timeSinceLastJump);
            rb.AddForce(new Vector3(0,jumpForce, 0), ForceMode.Impulse);
            
        }
    }
}
