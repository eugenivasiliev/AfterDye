using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions input;

    [Header("Local")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Variables")]
    [SerializeField] private AnimationCurve moveForce;
    [SerializeField] private float maxSpeed;

    private Vector2 movement => input.Player.Move.ReadValue<Vector2>();
    private Vector2 horizontalMovement => movement.x * Vector2.right;
    private int horizontalMoveDir => (int)Mathf.Sign(horizontalMovement.x);
    private int prevHorizontalMoveDir;
    private bool isMovingHorizontal => horizontalMovement.x != 0.0f;
    private float horizontalMovementT;

    void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();
    }

    void Update()
    {
        if(isMovingHorizontal) Move();
    }

    private void Move()
    {
        bool hasTurned = horizontalMoveDir != prevHorizontalMoveDir;
        if(hasTurned)
        {
            rb.linearVelocity = Vector2.zero; //Instant turnaround
            horizontalMovementT = 0.0f;
        }

        Vector2 horizontalForce = moveForce.Evaluate(horizontalMovementT) * horizontalMovement;
        rb.AddForce(horizontalForce);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

        prevHorizontalMoveDir = horizontalMoveDir;
        horizontalMovementT += Time.deltaTime;
    }
}
