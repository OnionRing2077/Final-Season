using UnityEngine;

/// <summary>
/// Game Manager ที่รวมระบบทั้งหมดเข้าด้วยกัน
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Character References")]
    public CharacterController2D characterController;
    public SpriteFlipController flipController;
    
    [Header("Camera Reference")]
    public FixedCameraController cameraController;
    
    [Header("Input Reference")]
    public InputManager inputManager;
    
    void Start()
    {
        SetupComponents();
        ConnectSystems();
    }
    
    void SetupComponents()
    {
        // หาคอมโพเนนต์ต่าง ๆ ถ้าไม่ได้กำหนดไว้
        if (characterController == null)
            characterController = FindObjectOfType<CharacterController2D>();
            
        if (flipController == null)
            flipController = FindObjectOfType<SpriteFlipController>();
            
        if (cameraController == null)
            cameraController = FindObjectOfType<FixedCameraController>();
            
        if (inputManager == null)
            inputManager = FindObjectOfType<InputManager>();
    }
    
    void ConnectSystems()
    {
        // เชื่อมต่อ Input Manager กับ Character Controller
        if (inputManager != null && characterController != null)
        {
            inputManager.OnMovementInput += characterController.SetMovementInput;
        }
        
        // เชื่อมต่อ Character Controller กับ Flip Controller
        if (characterController != null && flipController != null)
        {
            // สามารถเพิ่ม event system ที่นี่ถ้าต้องการ
        }
        
        // เซ็ตเป้าหมายสำหรับกล้อง
        if (cameraController != null && characterController != null)
        {
            cameraController.SetTarget(characterController.transform);
        }
    }
    
    void Update()
    {
        // อัพเดทการฟลิปตัวละครตาม input
        if (inputManager != null && flipController != null)
        {
            Vector2 input = inputManager.GetMovementInput();
            if (input.magnitude > 0.1f)
            {
                flipController.FlipToDirection(input);
            }
        }
    }
    
    void OnDestroy()
    {
        // ยกเลิกการเชื่อมต่อ event
        if (inputManager != null && characterController != null)
        {
            inputManager.OnMovementInput -= characterController.SetMovementInput;
        }
    }
}