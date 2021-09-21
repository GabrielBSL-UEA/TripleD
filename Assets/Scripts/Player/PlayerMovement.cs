using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 20;

    private PlayerInput inputActions;
    private Vector2 movementDirection;

    private Rigidbody m_Rb;
    private bool m_IsJumping;

    private void Awake()
    {
        inputActions = new PlayerInput();

        inputActions.Player.Movement.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => movementDirection = Vector2.zero;

        inputActions.Player.Jump.performed += _ => Jump();

        TryGetComponent(out m_Rb);
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void FixedUpdate()
    {
        m_Rb.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y), ForceMode.Impulse);
        //m_Rb.MovePosition(m_Rb.position + new Vector3(movementDirection.x, 0, movementDirection.y) * movementSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (m_IsJumping) return;

        m_IsJumping = true;
        m_Rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_IsJumping = false;
    }
}
