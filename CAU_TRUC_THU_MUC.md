# Sơ Đồ Cấu Trúc Thư Mục Dự Án CatRun

```
CatRun/
│
├── 📁 Assets/                          # Thư mục chính chứa tất cả assets
│   │
│   ├── 📁 AddressableAssetsData/       # Cấu hình Addressables (load assets động)
│   │   ├── AssetGroups/                # Nhóm assets
│   │   ├── DataBuilders/               # Build configurations
│   │   └── [Platforms]/                # Android, WebGL, Windows, OSX
│   │
│   ├── 📁 Animation/                   # Animations và controllers
│   │   ├── *.fbx                       # Animation files
│   │   └── *.controller                # Animator controllers
│   │
│   ├── 📁 Bundles/                     # Asset bundles
│   │   ├── Characters/                 # Character prefabs
│   │   └── Themes/                     # Theme prefabs
│   │
│   ├── 📁 Editor/                      # Editor scripts
│   │   └── BundleAndBuild.cs          # Build automation
│   │
│   ├── 📁 Font/                        # Fonts
│   │   ├── LuckiestGuy.ttf
│   │   └── ReadexPro-VariableFont...
│   │
│   ├── 📁 Materials/                   # Materials (122 files)
│   │
│   ├── 📁 Models/                      # 3D Models
│   │   ├── Cat.fbx, Dog.FBX, Rat.fbx
│   │   ├── Daytime/                     # Day theme models
│   │   ├── NightTime/                   # Night theme models
│   │   └── Materials/                  # Model materials
│   │
│   ├── 📁 Plugins/                     # Third-party plugins
│   │   └── Android/                    # Android-specific
│   │
│   ├── 📁 Prefabs/                     # Unity Prefabs
│   │   ├── Explosion.prefab
│   │   ├── MusicPlayer.prefab
│   │   ├── Particles/                  # Particle effects
│   │   ├── Powerup/                     # Powerup prefabs
│   │   └── UI/                          # UI prefabs
│   │
│   ├── 📁 Resources/                   # Resources folder
│   │   ├── BillingMode.json
│   │   └── IAPProductCatalog.json
│   │
│   ├── 📁 Scenes/                      # Unity Scenes
│   │   └── *.unity                     # Main, Shop, Tutorial scenes
│   │
│   ├── 📁 Scripts/                     # ⭐ CODE CHÍNH
│   │   │
│   │   ├── 📁 Characters/              # Character system
│   │   │   ├── Character.cs            # Character data
│   │   │   ├── CharacterAccessories.cs # Accessories system
│   │   │   ├── CharacterCollider.cs   # Collision detection
│   │   │   ├── CharacterDatabase.cs   # Character database
│   │   │   ├── CharacterInputController.cs # Input handling
│   │   │   ├── RandomAnimation.cs
│   │   │   └── RestartRunning.cs
│   │   │
│   │   ├── 📁 Consumable/              # Powerups/Consumables
│   │   │   ├── Consumable.cs           # Base consumable class
│   │   │   ├── ConsumableDatabase.cs
│   │   │   ├── ConsumableIcon.cs
│   │   │   └── 📁 Types/               # Consumable types
│   │   │       ├── CoinMagnet.cs       # Magnet powerup
│   │   │       ├── ExtraLife.cs        # Extra life
│   │   │       ├── Invincibility.cs    # Invincibility
│   │   │       └── Score2Multiplier.cs # Score multiplier
│   │   │
│   │   ├── 📁 GameManager/              # ⭐ Core game management
│   │   │   ├── GameManager.cs          # State machine manager
│   │   │   ├── GameState.cs            # In-game state
│   │   │   ├── GameOverState.cs        # Game over state
│   │   │   ├── LoadoutState.cs         # Menu/loadout state
│   │   │   ├── TutorialState.cs        # Tutorial state
│   │   │   ├── Modifier.cs             # Game modifiers
│   │   │   └── MonoSingleton.cs        # Singleton base
│   │   │
│   │   ├── 📁 Missions/                 # Mission system
│   │   │   └── Missions.cs             # Mission types & logic
│   │   │
│   │   ├── 📁 Obstacles/                # Obstacle system
│   │   │   ├── Obstacle.cs             # Base obstacle class
│   │   │   ├── AllLaneObstacle.cs      # Blocks all lanes
│   │   │   ├── SimpleBarricade.cs      # Simple barricade
│   │   │   ├── PatrollingObstacle.cs   # Moving obstacle
│   │   │   └── Missile.cs              # Missile obstacle
│   │   │
│   │   ├── 📁 Sounds/                   # Audio system
│   │   │   ├── MusicPlayer.cs
│   │   │   ├── AssignOutputChannel.cs
│   │   │   └── CountdownSound.cs
│   │   │
│   │   ├── 📁 Themes/                   # Theme system
│   │   │   ├── ThemeData.cs
│   │   │   └── ThemeDatabase.cs
│   │   │
│   │   ├── 📁 Tracks/                   # ⭐ Track/Map system
│   │   │   ├── TrackManager.cs         # Manages track segments
│   │   │   └── TrackSegment.cs         # Individual track segment
│   │   │
│   │   ├── 📁 UI/                       # User Interface
│   │   │   ├── MainMenu.cs
│   │   │   ├── Leaderboard.cs
│   │   │   ├── MissionUI.cs
│   │   │   ├── HighscoreUI.cs
│   │   │   ├── PowerupIcon.cs
│   │   │   ├── 📁 Settings/            # Settings UI
│   │   │   │   ├── SettingPopup.cs
│   │   │   │   └── DataDeleteConfirmation.cs
│   │   │   └── 📁 Shop/                # Shop system
│   │   │       ├── ShopUIPanel.cs
│   │   │       ├── ShopCharacterList.cs
│   │   │       ├── ShopThemeList.cs
│   │   │       ├── ShopItemList.cs
│   │   │       ├── ShopAccessoriesList.cs
│   │   │       └── IAPHandler.cs        # In-app purchase
│   │   │
│   │   ├── Coin.cs                      # Coin class
│   │   ├── PlayerData.cs                # ⭐ Save data system
│   │   ├── Pooler.cs                   # Object pooling
│   │   ├── WorldCurver.cs               # World curve effect
│   │   ├── CoroutineHandler.cs
│   │   ├── Helpers.cs
│   │   └── LevelLoader.cs
│   │
│   ├── 📁 Settings/                     # Unity settings
│   │
│   ├── 📁 Shaders/                      # Shaders
│   │   ├── CurvedCode.cginc            # Curve shader code
│   │   └── *.shader                    # Various shaders
│   │
│   ├── 📁 Sounds/                       # Audio files
│   │   ├── *.ogg                        # Sound effects
│   │   └── *.mixer                      # Audio mixer
│   │
│   ├── 📁 TextMesh Pro/                 # TextMesh Pro assets
│   │
│   ├── 📁 Textures/                     # Textures
│   │   ├── *.png
│   │   └── *.tif
│   │
│   ├── 📁 Tutorial/                     # Tutorial assets
│   │
│   ├── 📁 UI/                           # UI sprites/images
│   │   └── *.png                        # UI textures
│   │
│   └── 📁 TutorialInfo/                 # Tutorial info
│
├── 📁 Library/                          # Unity library (auto-generated)
│
├── 📁 Packages/                         # Unity packages
│   ├── manifest.json
│   └── packages-lock.json
│
├── 📁 ProjectSettings/                  # Project settings
│
├── 📁 UserSettings/                     # User-specific settings
│
├── 📁 GeneratedAssets/                  # Generated assets
│
├── 📄 CatRun.sln                        # Visual Studio solution
├── 📄 README.md
├── 📄 LICENSE
├── 📄 LUONG_HOAT_DONG.md               # Tài liệu luồng hoạt động
└── 📄 class_diagram.md                  # Class diagram
```

## 📊 Tổng Quan Cấu Trúc

### 🎯 Thư Mục Quan Trọng Nhất

1. **`Assets/Scripts/`** - Tất cả code C#
   - `GameManager/` - Quản lý state machine
   - `Tracks/` - Hệ thống track/segment
   - `Characters/` - Hệ thống nhân vật
   - `UI/` - Giao diện người dùng

2. **`Assets/Prefabs/`** - Unity Prefabs
   - Characters, Obstacles, Powerups, UI

3. **`Assets/Models/`** - 3D Models
   - Characters, Obstacles, Collectibles

4. **`Assets/Scenes/`** - Unity Scenes
   - Main scene, Shop scene, Tutorial

5. **`Assets/AddressableAssetsData/`** - Addressables
   - Quản lý load assets động

### 📁 Cấu Trúc Scripts Chi Tiết

```
Scripts/
├── GameManager/          # ⭐ Core - State machine
├── Tracks/              # ⭐ Core - Track system
├── Characters/          # ⭐ Core - Character system
├── UI/                  # UI components
├── Obstacles/           # Obstacle types
├── Consumable/          # Powerup system
├── Missions/            # Mission system
├── Themes/              # Theme system
├── Sounds/              # Audio system
└── [Root scripts]       # Coin, PlayerData, Pooler, etc.
```

### 🔑 File Code Chính

| File | Chức Năng |
|------|-----------|
| `GameManager.cs` | Quản lý state machine |
| `TrackManager.cs` | Quản lý track segments, spawn obstacles/coins |
| `CharacterInputController.cs` | Xử lý input (keyboard/touch) |
| `CharacterCollider.cs` | Xử lý collision (coins/obstacles) |
| `PlayerData.cs` | Lưu trữ dữ liệu người chơi |
| `GameState.cs` | State khi đang chơi |
| `LoadoutState.cs` | Menu chọn character/theme |
| `GameOverState.cs` | Màn hình game over |

### 📦 Asset Organization

- **Bundles/** - Addressable bundles
- **Prefabs/** - Reusable game objects
- **Models/** - 3D models organized by theme
- **Textures/** - 2D textures
- **Sounds/** - Audio files
- **Shaders/** - Custom shaders

---

*Sơ đồ này mô tả cấu trúc thư mục của dự án Unity CatRun. Các thư mục quan trọng nhất được đánh dấu ⭐.*

