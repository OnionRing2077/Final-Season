using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input Settings")]
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    
    [Header("Alternative Keys")]
    public bool useArrowKeys = true;
    
    [Header("Mobile Support")]
    public bool enableTouchInput = false;
    public float touchSensitivity = 2f;
    
    // Input values
    private Vector2 movementInput;
    private Vector2 lastTouchPosition;
    private bool isTouching = false;
    
    // Events
    public System.Action<Vector2> OnMovementInput;
    
    void Update()
    {
        HandleKeyboardInput();
        
        if (enableTouchInput)
            HandleTouchInput();
            
        // ส่ง input ไปยัง subscribers
        OnMovementInput?.Invoke(movementInput);
    }
    
    void HandleKeyboardInput()
    {
        float horizontal = 0f;
        float vertical = 0f;
        
        // ตรวจสอบการกดปุ่ม WASD
        if (Input.GetKey(moveLeftKey))
            horizontal -= 1f;
        if (Input.GetKey(moveRightKey))
            horizontal += 1f;
        if (Input.GetKey(moveUpKey))
            vertical += 1f;
        if (Input.GetKey(moveDownKey))
            vertical -= 1f;
            
        // ตรวจสอบลูกศร (ถ้าเปิดใช้งาน)
        if (useArrowKeys)
        {
            horizontal += Input.GetAxisRaw("Horizontal");
            vertical += Input.GetAxisRaw("Vertical");
        }
        
        // จำกัดค่าไม่ให้เกิน -1 ถึง 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        vertical = Mathf.Clamp(vertical, -1f, 1f);
        
        movementInput = new Vector2(horizontal, vertical);
    }
    
    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastTouchPosition = touch.position;
                    isTouching = true;
                    break;
                    
                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        Vector2 deltaPosition = touch.position - lastTouchPosition;
                        
                        // แปลงการเคลื่อนไหวของนิ้วเป็น input
                        float horizontal = deltaPosition.x / Screen.width * touchSensitivity;
                        float vertical = deltaPosition.y / Screen.height * touchSensitivity;
                        
                        movementInput = new Vector2(horizontal, vertical);
                        movementInput = Vector2.ClampMagnitude(movementInput, 1f);
                    }
                    break;
                    
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    movementInput = Vector2.zero;
                    break;
            }
        }
        else
        {
            if (isTouching)
            {
                isTouching = false;
                movementInput = Vector2.zero;
            }
        }
    }
    
    // Public methods
    public Vector2 GetMovementInput()
    {
        return movementInput;
    }
    
    public bool IsMoving()
    {
        return movementInput.magnitude > 0.1f;
    }
    
    public void SetTouchEnabled(bool enabled)
    {
        enableTouchInput = enabled;
    }
    
    public void SetArrowKeysEnabled(bool enabled)
    {
        useArrowKeys = enabled;
    }
}