using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float movementSpeed;
    

    [Header("Gravity Options")]
    [SerializeField] [Range(-10f, 10f)] private float gravityScale = -4f;
    [SerializeField] [Range(2f, 30f)] private float jumpHeight = 5f;

    [Header("Ground Options")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Button Options")]
    [SerializeField] private string jumpButton = "Jump";

    private Vector3 movementDirection;
    private Vector3 verticalVelocity;
    private float gravityMultiplyer = 20f;
    private bool canDoubleJump;
    private bool isGrounded;
    private float sprintSpeed;
    private float normalSpeed;

    public bool CanDoubleJump { get => canDoubleJump; set => canDoubleJump = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    void Start()
    {
        normalSpeed = movementSpeed;
        sprintSpeed = movementSpeed * 2;
        
        characterController = GetComponent<CharacterController>();        
    }

    void Update()
    {
        GroundCheck();
        PlayerSprint();
        HorizontalMovement();
        VerticalMovement();
        Jump();

    }

    private void PlayerSprint()
    {
        if (Input.GetButton("Sprint"))
        {

            movementSpeed = sprintSpeed;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            movementSpeed = normalSpeed;
        }
    }

    private void Jump()
    {
        if (IsGrounded && Input.GetButtonDown(jumpButton))
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravityScale * gravityMultiplyer);
            CanDoubleJump = true;
        }
        else if (Input.GetButtonDown(jumpButton) && CanDoubleJump)
        {
            CanDoubleJump = false;
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravityScale * gravityMultiplyer);
        }
    }

    private void VerticalMovement()
    {
        if (IsGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = 0f;
        }
        else
        {
            verticalVelocity.y += gravityScale * gravityMultiplyer * Time.deltaTime;
        }
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void HorizontalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection = transform.TransformDirection(movementDirection);

        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
    }

    private void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, whatIsGround);
    }

}
