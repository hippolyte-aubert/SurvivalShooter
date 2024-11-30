using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public InputSystem_Actions inputActions;
    public InputSystem_Actions.PlayerActions look;
    public Transform playerBody;
    public float mouseSensitivity = 100.0f;
    public float maxLookAngle = 90.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    
    // Awake is called once before the first execution of Start after the MonoBehaviour is created
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputActions = new InputSystem_Actions();
        look = inputActions.Player;
        look.Enable();
        
        // Subscribe to the OnLook event
        look.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = look.Look.ReadValue<Vector2>();
        Look(lookInput);
    }
    
    void Look(Vector2 lookInput)
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        yRotation += mouseX;
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
