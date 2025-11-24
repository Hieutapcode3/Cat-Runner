# Phân Tích Dự Án CatRun - Các File Code Chính và Luồng Hoạt Động

## 📁 Các File Code Chính

### 1. **GameManager.cs** - Quản Lý State Machine
**Vị trí:** `Assets/Scripts/GameManager/GameManager.cs`

**Chức năng:**
- Quản lý State Machine của game (State Pattern)
- Chuyển đổi giữa các state: Loadout → Game → GameOver
- Quản lý stack các state (có thể push/pop state)
- Singleton pattern để truy cập từ mọi nơi

**Các phương thức quan trọng:**
- `SwitchState(string newState)`: Chuyển đổi state
- `PushState(string name)`: Thêm state vào stack
- `PopState()`: Xóa state khỏi stack
- `FindState(string stateName)`: Tìm state theo tên

---

### 2. **GameState.cs** - State Khi Đang Chơi
**Vị trí:** `Assets/Scripts/GameManager/GameState.cs`

**Chức năng:**
- Quản lý gameplay khi người chơi đang chơi
- Cập nhật UI (coin, score, distance, life)
- Xử lý pause/resume
- Quản lý tutorial
- Xử lý game over và second wind (xem quảng cáo để tiếp tục)

**Luồng hoạt động:**
1. `Enter()`: Khởi tạo game, setup UI, bắt đầu nhạc
2. `StartGame()`: Bắt đầu game, khởi tạo character, track
3. `Tick()`: Mỗi frame cập nhật:
   - Kiểm tra life của character
   - Cập nhật consumables (powerups)
   - Cập nhật UI
   - Kiểm tra tutorial
4. `WaitForGameOver()`: Khi character chết, chờ 2 giây rồi hiện popup game over

---

### 3. **LoadoutState.cs** - Menu Chọn Nhân Vật/Theme
**Vị trí:** `Assets/Scripts/GameManager/LoadoutState.cs`

**Chức năng:**
- Menu chính trước khi vào game
- Cho phép chọn character, theme, accessory, powerup
- Hiển thị missions, leaderboard
- Nút "Run!" để bắt đầu game

**Luồng hoạt động:**
1. `Enter()`: Load UI, hiển thị character/theme hiện tại
2. `Tick()`: Xoay character preview, kiểm tra khi nào có thể bắt đầu game
3. `StartGame()`: Chuyển sang GameState

---

### 4. **GameOverState.cs** - Màn Hình Game Over
**Vị trí:** `Assets/Scripts/GameManager/GameOverState.cs`

**Chức năng:**
- Hiển thị khi game kết thúc
- Lưu điểm cao, coins, premium vào PlayerData
- Gửi analytics events
- Cho phép quay lại Loadout hoặc chơi lại

---

### 5. **TrackManager.cs** - Quản Lý Đường Chạy
**Vị trí:** `Assets/Scripts/Tracks/TrackManager.cs`

**Chức năng:**
- Tạo và quản lý các track segments (đoạn đường)
- Di chuyển character dọc theo track
- Spawn obstacles, coins, powerups
- Quản lý tốc độ game (tăng dần theo thời gian)
- Tính điểm, multiplier
- Floating origin (reset vị trí khi đi quá xa để tránh lỗi floating point)

**Luồng hoạt động:**
1. `Begin()`: Khởi tạo game:
   - Load character từ Addressables
   - Setup theme, sky, fog
   - Tạo coin pool
   - Bắt đầu countdown
2. `Update()`: Mỗi frame:
   - Di chuyển character theo track
   - Tạo segments mới khi cần
   - Spawn obstacles/coins/powerups
   - Tăng tốc độ dần
   - Tính điểm
   - Xử lý floating origin
3. `SpawnNewSegment()`: Tạo segment mới từ theme data
4. `SpawnObstacle()`: Spawn obstacles vào segment
5. `SpawnCoinAndPowerup()`: Spawn coins và powerups

---

### 6. **CharacterInputController.cs** - Điều Khiển Nhân Vật
**Vị trí:** `Assets/Scripts/Characters/CharacterInputController.cs`

**Chức năng:**
- Xử lý input từ bàn phím (desktop) và touch (mobile/WebGL)
- Điều khiển nhảy, trượt, đổi lane
- Quản lý life, coins, premium
- Quản lý consumables (powerups)

**Input:**
- **Desktop/WebGL (bàn phím):**
  - Mũi tên trái/phải: Đổi lane
  - Mũi tên lên: Nhảy
  - Mũi tên xuống: Trượt
- **Mobile/WebGL (touch):**
  - Vuốt trái/phải: Đổi lane
  - Vuốt lên: Nhảy
  - Vuốt xuống: Trượt

**Luồng hoạt động:**
1. `Update()`: Đọc input mỗi frame
2. `Jump()`: Thực hiện nhảy (animation + di chuyển theo đường cong)
3. `Slide()`: Thực hiện trượt (thay đổi collider size)
4. `ChangeLane(int direction)`: Đổi lane (-1: trái, +1: phải)

---

### 7. **CharacterCollider.cs** - Xử Lý Va Chạm
**Vị trí:** `Assets/Scripts/Characters/CharacterCollider.cs`

**Chức năng:**
- Xử lý collision với coins, obstacles, powerups
- Quản lý invincibility (bất tử tạm thời sau khi bị hit)
- Thu thập coins/premium
- Ghi nhận dữ liệu khi chết (cho analytics)

**Luồng hoạt động:**
1. `OnTriggerEnter()`: Khi có collision:
   - **Coin (Layer 8)**: Thu thập coin, cộng vào PlayerData
   - **Obstacle (Layer 9)**: 
     - Giảm life
     - Nếu còn life: Bật invincibility 2 giây
     - Nếu hết life: Ghi nhận death data, trigger game over
   - **Powerup (Layer 10)**: Kích hoạt consumable
2. `SetInvincible()`: Bật bất tử, nhấp nháy character
3. `Slide()`: Thay đổi kích thước collider khi trượt

---

### 8. **PlayerData.cs** - Lưu Trữ Dữ Liệu Người Chơi
**Vị trí:** `Assets/Scripts/PlayerData.cs`

**Chức năng:**
- Lưu trữ dữ liệu người chơi (coins, premium, characters, themes)
- Lưu/đọc từ file binary
- Quản lý missions, highscores
- Singleton pattern

**Dữ liệu lưu trữ:**
- Coins, premium currency
- Characters và themes đã mua
- Accessories đã mua
- Consumables inventory
- Highscores
- Missions
- Settings (volume, etc.)

---

## 🔄 Luồng Hoạt Động Tổng Quan

### 1. **Khởi Động Game**
```
GameManager.OnEnable()
  ↓
PlayerData.Create() - Load hoặc tạo save mới
  ↓
LoadoutState.Enter() - Hiển thị menu chính
  ↓
Người chơi chọn character, theme, powerup
  ↓
Click "Run!" → LoadoutState.StartGame()
```

### 2. **Bắt Đầu Gameplay**
```
GameManager.SwitchState("Game")
  ↓
GameState.Enter()
  ↓
GameState.StartGame()
  ↓
TrackManager.Begin() - Load character, setup track
  ↓
TrackManager.WaitToStart() - Countdown 5 giây
  ↓
CharacterInputController.StartRunning()
  ↓
TrackManager.StartMove() - Bắt đầu di chuyển
```

### 3. **Gameplay Loop**
```
Mỗi Frame:
  ↓
TrackManager.Update()
  ├─ Di chuyển character theo track
  ├─ Tạo segments mới
  ├─ Spawn obstacles/coins/powerups
  ├─ Tăng tốc độ
  └─ Tính điểm
  ↓
CharacterInputController.Update()
  ├─ Đọc input (keyboard/touch)
  ├─ Xử lý nhảy/trượt/đổi lane
  └─ Cập nhật vị trí character
  ↓
CharacterCollider.OnTriggerEnter()
  ├─ Thu thập coins
  ├─ Va chạm obstacles → giảm life
  └─ Nhặt powerups
  ↓
GameState.Tick()
  ├─ Cập nhật UI
  ├─ Quản lý consumables
  └─ Kiểm tra game over
```

### 4. **Game Over**
```
CharacterCollider: currentLife = 0
  ↓
GameState: WaitForGameOver()
  ├─ Dừng di chuyển
  ├─ Chờ 2 giây
  └─ Hiện popup game over
  ↓
Người chơi chọn:
  ├─ Xem quảng cáo → SecondWind() → Tiếp tục chơi
  ├─ Dùng premium → SecondWind() → Tiếp tục chơi
  └─ GameOver() → Chuyển sang GameOverState
  ↓
GameOverState.Enter()
  ├─ Lưu coins/premium vào PlayerData
  ├─ Lưu highscore
  └─ Gửi analytics
  ↓
GameOverState.Exit()
  ├─ TrackManager.End() - Cleanup
  └─ Chuyển về LoadoutState
```

## 🎮 Các Hệ Thống Con

### **Input System**
- Desktop: Keyboard input (mũi tên)
- Mobile/WebGL: Touch input (swipe gestures)
- Hỗ trợ cả hai trên WebGL

### **Collision System**
- Sử dụng Unity Trigger Colliders
- Layers: Coins (8), Obstacles (9), Powerups (10)
- Collider thay đổi kích thước khi trượt

### **State Management**
- State Pattern với stack
- Các state: Loadout, Game, GameOver, Tutorial
- Có thể push/pop để quay lại state trước

### **Object Pooling**
- Coin pool để tái sử dụng
- Addressables để load/unload assets

### **Floating Origin**
- Khi character đi quá xa (10000m), reset về gốc tọa độ
- Tránh lỗi floating point precision

### **Tutorial System**
- Hướng dẫn từng bước: đổi lane, nhảy, trượt
- Chặn chuyển zone cho đến khi hoàn thành tutorial step

## 📊 Kiến Trúc Code

```
GameManager (State Machine)
  ├─ LoadoutState (Menu chính)
  ├─ GameState (Gameplay)
  │   └─ TrackManager (Quản lý track)
  │       └─ CharacterInputController (Điều khiển)
  │           └─ CharacterCollider (Va chạm)
  └─ GameOverState (Kết thúc)
  
PlayerData (Singleton - Lưu trữ dữ liệu)
```

## 🔑 Design Patterns Sử Dụng

1. **Singleton**: GameManager, TrackManager, PlayerData
2. **State Pattern**: GameManager quản lý các state
3. **Object Pooling**: Coin pool
4. **Observer Pattern**: Events cho segment creation/change
5. **Component Pattern**: Character, CharacterCollider, CharacterInputController tách biệt

---

*Tài liệu này giải thích cấu trúc và luồng hoạt động của game CatRun. Để hiểu chi tiết hơn, xem code comments trong từng file.*

