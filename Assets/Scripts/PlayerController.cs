using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions input;

    [Header("Local")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D groundCheckCollider;

    [Header("Variables")]
    [SerializeField] private AnimationCurve horizontalMoveForce;
    [SerializeField] private AnimationCurve jumpForce;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float maxJumpSpeed;

    private Vector2 movement => input.Player.Move.ReadValue<Vector2>();

    private Vector2 horizontalMovement => movement.x * Vector2.right;
    private int horizontalMoveDir => (int)Mathf.Sign(horizontalMovement.x);
    private int prevHorizontalMoveDir;
    private bool isMovingHorizontal => horizontalMovement.x != 0.0f;
    private float horizontalMovementT;

    private List<Collider2D> groundContacts = new List<Collider2D>();
    private bool isGrounded => groundCheckCollider.GetContacts(groundContacts) > 0;

    void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();

        input.Player.Jump.performed += (ctx) => Jump();
    }

    void Update()
    {
        if(isMovingHorizontal) Move();

        rb.linearVelocity = 
            Mathf.Clamp(rb.linearVelocity.x, -maxHorizontalSpeed, maxHorizontalSpeed) * Vector2.right +
            Mathf.Clamp(rb.linearVelocity.y, float.NegativeInfinity, maxJumpSpeed) * Vector2.up; //Clamp velocity
    }

    private void Move()
    {
        bool hasTurned = horizontalMoveDir != prevHorizontalMoveDir;
        if(hasTurned)
        {
            rb.linearVelocity = rb.linearVelocity.y * Vector2.up; //Instant turnaround, set x to 0
            horizontalMovementT = 0.0f;
        }

        Vector2 horizontalForce = horizontalMoveForce.Evaluate(horizontalMovementT) * horizontalMovement;
        rb.AddForce(horizontalForce);

        prevHorizontalMoveDir = horizontalMoveDir;
        horizontalMovementT += Time.deltaTime;
    }

    private void Jump()
    {
        if(isGrounded) rb.AddForce(jumpForce.Evaluate(0.0f) * Vector2.up);
    }
}
