using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    public Transform target;       // ตัวละครที่จะติดตาม
    public float smoothSpeed = 0.125f;
    public Vector3 offset;         // ระยะห่างจากตัวละคร (เช่น (0, 10, -10))

    void LateUpdate()
    {
        if (target != null)
        {
            // คำนวณตำแหน่งใหม่ของกล้อง
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;

            // ให้กล้องหันมองตัวละครเสมอ
            transform.LookAt(target);
        }
    }
}
