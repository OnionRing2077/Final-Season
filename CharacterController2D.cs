using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;
    
    [Header("Character References")]
    public SpriteRenderer spriteRenderer;
    
    // Private variables
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private bool facingRight = true; // ตัวละครหันหน้าไปทางขวาเป็นค่าเริ่มต้น
    private Vector3 movementInput;
    
    // Input variables
    private float horizontalInput;
    private float verticalInput;
    
    void Start()
    {
        // ตั้งค่าตำแหน่งเริ่มต้น
        targetPosition = transform.position;
        
        // หา SpriteRenderer ถ้าไม่ได้กำหนดไว้
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleCharacterFlipping();
    }
    
    void HandleInput()
    {
        // รับ input จากคีย์บอร์ด
        horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D หรือ ลูกศรซ้าย/ขวา
        verticalInput = Input.GetAxisRaw("Vertical");     // W/S หรือ ลูกศรบน/ล่าง
        
        // สร้าง movement vector (ใช้แค่ X และ Z axis สำหรับ 3D movement)
        movementInput = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }
    
    void HandleMovement()
    {
        // คำนวณตำแหน่งใหม่
        if (movementInput.magnitude > 0)
        {
            targetPosition += movementInput * moveSpeed * Time.deltaTime;
        }
        
        // เคลื่อนที่อย่างนุ่มนวลไปยังตำแหน่งเป้าหมาย
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    
    void HandleCharacterFlipping()
    {
        // ตรวจสอบว่าควรหันหน้าไปทางไหน
        if (horizontalInput > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    
    void FlipCharacter()
    {
        // สลับสถานะการหันหน้า
        facingRight = !facingRight;
        
        // ฟลิป sprite โดยการเปลี่ยนค่า scale ของ X axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    // Method สำหรับรับค่า movement จากระบบอื่น (เช่น Mobile input)
    public void SetMovementInput(Vector2 input)
    {
        movementInput = new Vector3(input.x, 0, input.y).normalized;
        horizontalInput = input.x;
        verticalInput = input.y;
    }
    
    // Method สำหรับตรวจสอบว่าตัวละครกำลังเดินอยู่หรือไม่
    public bool IsMoving()
    {
        return movementInput.magnitude > 0;
    }
    
    // Method สำหรับเซ็ตตำแหน่งโดยตรง
    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        targetPosition = newPosition;
    }
}