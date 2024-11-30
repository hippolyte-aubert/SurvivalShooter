using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    public InputSystem_Actions.PlayerActions movement;
    
    public Rigidbody rb;
    
    public float speed = 5.0f;
    
    // Awake is called once before the first execution of Start after the MonoBehaviour is created
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputActions = new InputSystem_Actions();
        movement = inputActions.Player;
        movement.Enable();
        rb = GetComponent<Rigidbody>();
        
        movement.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        
        movement.Jump.performed += ctx => Jump();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = movement.Move.ReadValue<Vector2>();
        Move(moveInput);
        
        if (movement.Jump.triggered)
        {
            Jump();
        }
    }
    
    void Move(Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, 0, direction.y);
        Vector3 directionToMove = transform.right * move.x + transform.forward * move.z;
        transform.Translate(directionToMove * (speed * Time.deltaTime));
    }
    
    public void Jump()
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }
}
