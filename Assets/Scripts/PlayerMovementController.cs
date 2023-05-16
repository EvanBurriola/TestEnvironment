using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gravityScale;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float jumpDelay;
    
    [Header("Ground Check")]
    [SerializeField]
    private float playerHeight;
    public LayerMask whatIsGround;
    [SerializeField]
    private bool grounded;

    private Vector3 input;
    private float mouseX;
    private float mouseY;

    //Storing axis input in vector instead of individual floats
    /*float horizontalInput;
    float verticalInput;
    */
    float jumpInput;
    float timeSinceLastJump;
    

    Vector3 moveDirection;
    Vector3 gravity;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void Update(){
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + .05F, whatIsGround);
        MyInput();
        Look();

        //Update the timer
        timeSinceLastJump += Time.fixedDeltaTime;
    }
    
    private void FixedUpdate() {
        gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
        MovePlayer();
    }

    private void MyInput(){
        /*horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        */
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        jumpInput = Input.GetAxisRaw("Jump");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    private void Look()
    {
        if (input == Vector3.zero) return;
        var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        
    }
    private void pointTowardsMouse() {

        transform.rotation = Quaternion.Euler(mouseX, 0, mouseY);
    }
    private void MovePlayer(){
        /*moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);*/
        rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * moveSpeed * Time.deltaTime);
        
        
        if (grounded && timeSinceLastJump >= jumpDelay && jumpInput > 0)
        {
            timeSinceLastJump = 0f;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * gravity.y);
            Debug.Log(timeSinceLastJump);
            rb.AddForce(new Vector3(0,jumpForce, 0), ForceMode.Impulse);
            
        }
    }
}


public static class Helpers //this class could be made into a seperate script called Helpers to make extension methods that work across also objects/scripts
{
    //Extension methods for quaternion rotation that are made outside of Look() so they are not calculated every frame
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
