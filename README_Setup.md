# วิธีการใช้งานระบบการเดินของตัวละคร 2D ใน 3D

## ไฟล์ที่สำคัญ

1. **CharacterController2D.cs** - ควบคุมการเดินของตัวละครใน 3D space
2. **SpriteFlipController.cs** - จัดการการฟลิปตัวละครเมื่อเปลี่ยนทิศทาง
3. **FixedCameraController.cs** - ระบบกล้องที่ล็อกมุมมองแบบ Among Us
4. **InputManager.cs** - จัดการ input จากคีย์บอร์ดและมือถือ
5. **GameManager.cs** - เชื่อมต่อระบบทั้งหมดเข้าด้วยกัน

## วิธีการติดตั้งใน Unity

### 1. การสร้างตัวละคร
1. สร้าง GameObject ใหม่ชื่อ "Player"
2. เพิ่ม SpriteRenderer และใส่ sprite ของตัวละคร
3. เพิ่มคอมโพเนนต์ทั้งหมดนี้:
   - `CharacterController2D`
   - `SpriteFlipController`
4. ตั้งค่า Tag เป็น "Player"

### 2. การตั้งค่ากล้อง
1. เลือกกล้องหลัก (Main Camera)
2. เพิ่มคอมโพเนนต์ `FixedCameraController`
3. ลาก Player GameObject ใส่ใน Target field
4. ปรับ Offset เป็น (0, 10, -8) สำหรับมุมมองแบบ Among Us
5. ตั้งค่า Fixed Rotation เป็น (45, 0, 0)

### 3. การตั้งค่า Input และ Game Manager
1. สร้าง GameObject ว่าง ชื่อ "GameManager"
2. เพิ่มคอมโพเนนต์ `InputManager` และ `GameManager`
3. ลากคอมโพเนนต์ต่าง ๆ ใส่ใน GameManager

### 4. การตั้งค่าคอมโพเนนต์

#### CharacterController2D
- **Move Speed**: 5 (ความเร็วการเดิน)
- **Smooth Time**: 0.1 (ความนุ่มนวลในการเคลื่อนที่)
- **Sprite Renderer**: ลาก SpriteRenderer ของตัวละคร

#### SpriteFlipController
- **Flip Method**: SpriteFlip (เร็วที่สุด) หรือ ScaleFlip (ถ้าต้องการเอฟเฟ็กต์)
- **Flip X**: เปิด
- **Flip Y**: ปิด

#### FixedCameraController
- **Offset**: (0, 10, -8)
- **Smooth Speed**: 0.125
- **Use Fixed Rotation**: เปิด
- **Fixed Rotation**: (45, 0, 0)

## การใช้งาน

### คีย์บอร์ด
- **WASD** หรือ **ลูกศร**: เดิน
- ตัวละครจะหันหน้าไปตามทิศทางที่เดินโดยอัตโนมัติ

### มือถือ (Touch)
- เปิด **Enable Touch Input** ใน InputManager
- ใช้นิ้วลากบนหน้าจอเพื่อเดิน

## คุณสมบัติพิเศษ

### การฟลิปตัวละคร
- รองรับ 3 วิธี: SpriteFlip, ScaleFlip, RotationFlip
- สามารถทำ Smooth Flip ได้
- หันหน้าตามทิศทางการเดินโดยอัตโนมัติ

### ระบบกล้อง
- มุมมองคงที่แบบ Among Us
- ติดตามตัวละครได้อย่างนุ่มนวล
- ไม่หมุนตามตัวละคร

### Input System
- รองรับทั้งคีย์บอร์ดและมือถือ
- สามารถปรับแต่งปุ่มได้
- Event-based สำหรับการเชื่อมต่อกับระบบอื่น

## การปรับแต่ง

### เปลี่ยนความเร็ว
ปรับ `moveSpeed` ใน CharacterController2D

### เปลี่ยนมุมกล้อง
ปรับ `fixedRotation` ใน FixedCameraController

### เปลี่ยนระยะห่างกล้อง
ปรับ `offset` ใน FixedCameraController

### เปลี่ยนวิธีการฟลิป
เปลี่ยน `flipMethod` ใน SpriteFlipController