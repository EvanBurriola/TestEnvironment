using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    Animator animator;
    public Transform groundCheck;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float sprintSpeed = 5f;
    [SerializeField]
    private float gravityScale = 5f;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float turnSpeed = 360f;
    [SerializeField]
    private float jumpDelay = 3f;
    
    [Header("Ground Check")]
    public Vector3 boxSize;
    public float maxDistance;
    [SerializeField]
    private float playerHeight = 3f;
    public LayerMask ground;
    public bool grounded;

    private Vector3 input;
    //Storing axis input in vector instead of individual floats
    /*float horizontalInput;
    float verticalInput;
    */
    float jumpInput;
    float timeSinceLastJump;
    bool sprintInput;

    Vector3 gravity;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void Update(){
        //ground check
        grounded = Physics.CheckSphere(groundCheck.position, maxDistance, ground);
        MyInput();
        Look();

        //Update the timer
        timeSinceLastJump += Time.fixedDeltaTime;
    }
    
    private void FixedUpdate(){
        gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
        MovePlayer();
        Animate();
    }

    private void MyInput(){
        /*horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        */
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        jumpInput = Input.GetAxisRaw("Jump");
        sprintInput = Input.GetKey(KeyCode.LeftShift);
    }

    private void Look(){
        if (input == Vector3.zero) return;        
            var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);     
    }

    private void MovePlayer(){
        /*moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);*/
        if (sprintInput)
            rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * sprintSpeed * Time.deltaTime);
        else
            rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * moveSpeed * Time.deltaTime);

        if (grounded && timeSinceLastJump >= jumpDelay && jumpInput > 0){
            timeSinceLastJump = 0f;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * gravity.y);
            rb.AddForce(new Vector3(0,jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void Animate() {
        bool isWalking = (input != Vector3.zero) ;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isSprinting", sprintInput && isWalking);       
    }


}

//this class could be made into a seperate script called Helpers to make extension methods that work across also objects/scripts
public static class Helpers{ 
    //Extension methods for quaternion rotation that are made outside of Look() so they are not calculated every frame
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
