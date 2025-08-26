using UnityEngine;

public class SpriteFlipController : MonoBehaviour
{
    [Header("Flip Settings")]
    public SpriteRenderer spriteRenderer;
    public bool flipX = true; // ฟลิปแกน X
    public bool flipY = false; // ฟลิปแกน Y (มักไม่ใช้สำหรับตัวละคร 2D)
    
    [Header("Flip Method")]
    public FlipMethod flipMethod = FlipMethod.SpriteFlip;
    
    [Header("Scale Flip Settings")]
    public bool smoothFlip = false; // ฟลิปอย่างนุ่มนวล
    public float flipSpeed = 10f; // ความเร็วในการฟลิป
    
    // Enum สำหรับเลือกวิธีการฟลิป
    public enum FlipMethod
    {
        SpriteFlip,     // ใช้ SpriteRenderer.flipX
        ScaleFlip,      // ใช้การเปลี่ยน scale
        RotationFlip    // ใช้การหมุน (สำหรับกรณีพิเศษ)
    }
    
    private bool facingRight = true;
    private Vector3 originalScale;
    private Vector3 targetScale;
    
    void Start()
    {
        // หา SpriteRenderer ถ้าไม่ได้กำหนด
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
            
        // เก็บ scale เริ่มต้น
        originalScale = transform.localScale;
        targetScale = originalScale;
    }
    
    void Update()
    {
        // อัพเดท smooth flip ถ้าเปิดใช้งาน
        if (smoothFlip && flipMethod == FlipMethod.ScaleFlip)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, flipSpeed * Time.deltaTime);
        }
    }
    
    /// <summary>
    /// ฟลิปตัวละครตามทิศทางที่กำหนด
    /// </summary>
    /// <param name="direction">ทิศทางการเคลื่อนที่ (เฉพาะแกน X)</param>
    public void FlipToDirection(float direction)
    {
        if (direction > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (direction < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    
    /// <summary>
    /// ฟลิปตัวละครตาม Vector2
    /// </summary>
    /// <param name="movementVector">Vector การเคลื่อนที่</param>
    public void FlipToDirection(Vector2 movementVector)
    {
        FlipToDirection(movementVector.x);
    }
    
    /// <summary>
    /// ฟลิปตัวละครโดยตรง
    /// </summary>
    public void FlipCharacter()
    {
        facingRight = !facingRight;
        
        switch (flipMethod)
        {
            case FlipMethod.SpriteFlip:
                FlipUsingSprite();
                break;
                
            case FlipMethod.ScaleFlip:
                FlipUsingScale();
                break;
                
            case FlipMethod.RotationFlip:
                FlipUsingRotation();
                break;
        }
    }
    
    /// <summary>
    /// ฟลิปโดยใช้ SpriteRenderer.flipX (วิธีที่เร็วที่สุด)
    /// </summary>
    void FlipUsingSprite()
    {
        if (spriteRenderer != null && flipX)
        {
            spriteRenderer.flipX = !facingRight;
        }
        
        if (spriteRenderer != null && flipY)
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }
    
    /// <summary>
    /// ฟลิปโดยใช้การเปลี่ยน scale
    /// </summary>
    void FlipUsingScale()
    {
        Vector3 newScale = originalScale;
        
        if (flipX)
        {
            newScale.x = facingRight ? originalScale.x : -originalScale.x;
        }
        
        if (flipY)
        {
            newScale.y = facingRight ? originalScale.y : -originalScale.y;
        }
        
        if (smoothFlip)
        {
            targetScale = newScale;
        }
        else
        {
            transform.localScale = newScale;
        }
    }
    
    /// <summary>
    /// ฟลิปโดยใช้การหมุน (สำหรับกรณีพิเศษ)
    /// </summary>
    void FlipUsingRotation()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = facingRight ? 0f : 180f;
        transform.rotation = Quaternion.Euler(rotation);
    }
    
    /// <summary>
    /// บังคับให้หันหน้าไปทางขวา
    /// </summary>
    public void FaceRight()
    {
        if (!facingRight)
            FlipCharacter();
    }
    
    /// <summary>
    /// บังคับให้หันหน้าไปทางซ้าย
    /// </summary>
    public void FaceLeft()
    {
        if (facingRight)
            FlipCharacter();
    }
    
    /// <summary>
    /// ตรวจสอบว่าหันหน้าไปทางขวาหรือไม่
    /// </summary>
    public bool IsFacingRight()
    {
        return facingRight;
    }
    
    /// <summary>
    /// เซ็ตทิศทางโดยตรง
    /// </summary>
    public void SetFacingRight(bool faceRight)
    {
        if (facingRight != faceRight)
            FlipCharacter();
    }
    
    /// <summary>
    /// รีเซ็ตกลับไปทิศทางเริ่มต้น
    /// </summary>
    public void ResetToDefault()
    {
        facingRight = true;
        
        switch (flipMethod)
        {
            case FlipMethod.SpriteFlip:
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = false;
                    spriteRenderer.flipY = false;
                }
                break;
                
            case FlipMethod.ScaleFlip:
                transform.localScale = originalScale;
                targetScale = originalScale;
                break;
                
            case FlipMethod.RotationFlip:
                transform.rotation = Quaternion.identity;
                break;
        }
    }
}