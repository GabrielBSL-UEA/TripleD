using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;

    private PlayerInput inputActions;
    private Vector2 movementDirection;

    private Rigidbody m_Rb;

    private void Awake()
    {
        inputActions = new PlayerInput();

        inputActions.Player.Movement.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => movementDirection = Vector2.zero;

        TryGetComponent(out m_Rb);
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void FixedUpdate()
    {
        m_Rb.MovePosition(m_Rb.position + (Vector3)movementDirection * movementSpeed * Time.fixedDeltaTime);
    }
}
