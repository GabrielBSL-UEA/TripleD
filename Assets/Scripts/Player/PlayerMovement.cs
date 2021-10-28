using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 20;

    private PlayerInput inputActions;
    private Vector2 movementDirection;
    private Vector3 respawnPoint;

    private Rigidbody m_Rb;

    private bool m_IsJumping;
    private bool m_CanMove = false;

    private void Awake()
    {
        inputActions = new PlayerInput();

        inputActions.Player.Movement.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => movementDirection = Vector2.zero;

        inputActions.Player.Jump.performed += _ => Jump();

        TryGetComponent(out m_Rb);
        respawnPoint = m_Rb.position;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void FixedUpdate()
    {
        if (!m_CanMove) return;

        if(m_Rb.position.y < -10f) Respawn();
        
        m_Rb.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y) * movementSpeed, ForceMode.Impulse);
        //m_Rb.MovePosition(m_Rb.position + new Vector3(movementDirection.x, 0, movementDirection.y) * movementSpeed * Time.fixedDeltaTime);
    }

    private void Respawn()
    {
        m_Rb.MovePosition(respawnPoint);
        m_Rb.velocity = Vector3.zero;
        m_Rb.angularVelocity = Vector3.zero;
    }

    private void Jump()
    {
        if (m_IsJumping || !m_CanMove) return;

        m_IsJumping = true;
        m_Rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    public void UnlockPlayer(bool value)
    {
        m_CanMove = value;

        if (value) return;

        m_Rb.angularVelocity = Vector3.zero;
        m_Rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        m_IsJumping = false;
    }
}
