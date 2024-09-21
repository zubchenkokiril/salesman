using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 300f;
    public float fallThreshold = -0.1f; 
    public float groundCheckDistance = 0.1f; 

    private float xRotation = 0f;
    private bool isGrounded;
    private bool isJumping;
    private bool isFalling;
    private float lastYPosition;

    public Transform playerBody;
    public Transform playerCamera;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; 
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lastYPosition = transform.position.y; 
    }

    void Update()
    {
        HandleJump();
        HandleCursorLock();
        HandleMovement();
        UpdateAnimatorParameters();
        CheckFalling();
    }

    void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.unscaledDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.unscaledDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = sprintSpeed;
        }

        float moveX = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move);

        float speed = new Vector3(moveX, 0, moveZ).magnitude * 10f;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftControl) && speed > 0.1f);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true; 
        } /*else {
            isJumping = false;
            isGrounded = true;
        }*/
    }
  

    

    void UpdateAnimatorParameters()
    {
        
        animator.SetBool("isGrounded", isGrounded);

        
        float verticalSpeed = rb.velocity.y;
        animator.SetFloat("verticalSpeed", verticalSpeed);

        
        animator.SetBool("isFalling", isFalling);
    }

    void CheckFalling()
    {
        
        if (isJumping)
        {
            float currentYPosition = transform.position.y;

            
            if (currentYPosition < lastYPosition)
            {
                isFalling = true;
                animator.SetBool("isFalling", true);
            }

            lastYPosition = currentYPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false; 
            isFalling = false; 
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrounded", isGrounded);
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
            
        }
    }

    void HandleCursorLock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
