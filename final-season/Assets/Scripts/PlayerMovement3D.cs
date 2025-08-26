using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody rb;
    public Transform cameraTransform; // ใส่ Main Camera เข้ามา

    Vector3 movement;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A,D
        float moveZ = Input.GetAxisRaw("Vertical");   // W,S

        // แกนของกล้อง
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // ตัดค่า Y ออก (ไม่ให้เดินขึ้นฟ้า/ลงดิน)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // คำนวณทิศการเดินตามมุมกล้อง
        movement = (forward * moveZ + right * moveX).normalized;

        // ถ้ามีการเคลื่อนที่ → หันตามทิศที่เดิน
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
