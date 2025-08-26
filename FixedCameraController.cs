using UnityEngine;

public class FixedCameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // ตัวละครที่กล้องจะติดตาม
    public Vector3 offset = new Vector3(0, 10, -8); // ระยะห่างของกล้องจากตัวละคร
    public float smoothSpeed = 0.125f; // ความเร็วในการติดตาม
    public bool followTarget = true; // เปิด/ปิดการติดตามตัวละคร
    
    [Header("Fixed Camera Mode")]
    public bool useFixedRotation = true; // ใช้มุมกล้องคงที่
    public Vector3 fixedRotation = new Vector3(45f, 0f, 0f); // มุมกล้องคงที่ (สไตล์ Among Us)
    
    [Header("Movement Constraints")]
    public bool constrainMovement = false; // จำกัดการเคลื่อนที่ของกล้อง
    public Vector2 minPosition = new Vector2(-10f, -10f); // ขอบเขตต่ำสุด (X, Z)
    public Vector2 maxPosition = new Vector2(10f, 10f);   // ขอบเขตสูงสุด (X, Z)
    
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        // ตั้งค่ามุมกล้องเริ่มต้น
        if (useFixedRotation)
        {
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
        
        // หาตัวละครถ้าไม่ได้กำหนดไว้
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }
    
    void LateUpdate()
    {
        if (target == null || !followTarget) return;
        
        FollowTarget();
        
        // รักษามุมกล้องให้คงที่
        if (useFixedRotation)
        {
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
    
    void FollowTarget()
    {
        // คำนวณตำแหน่งที่กล้องควรจะอยู่
        Vector3 desiredPosition = target.position + offset;
        
        // จำกัดการเคลื่อนที่ถ้าเปิดใช้งาน
        if (constrainMovement)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, minPosition.y, maxPosition.y);
        }
        
        // เคลื่อนที่อย่างนุ่มนวลไปยังตำแหน่งเป้าหมาย
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
    
    // Method สำหรับเปลี่ยนการติดตาม
    public void SetFollowTarget(bool follow)
    {
        followTarget = follow;
    }
    
    // Method สำหรับเปลี่ยนเป้าหมาย
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
    // Method สำหรับเซ็ตตำแหน่งกล้องโดยตรง
    public void SetCameraPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    // Method สำหรับเปลี่ยน offset
    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
    
    // Method สำหรับรีเซ็ตกล้องกลับไปที่ตำแหน่งเริ่มต้น
    public void ResetCamera()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            if (useFixedRotation)
            {
                transform.rotation = Quaternion.Euler(fixedRotation);
            }
        }
    }
    
    // Method สำหรับเปลี่ยนมุมกล้อง
    public void SetCameraAngle(Vector3 newRotation)
    {
        fixedRotation = newRotation;
        if (useFixedRotation)
        {
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
}