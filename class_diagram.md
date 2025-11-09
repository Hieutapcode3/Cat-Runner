# Sơ đồ lớp Class Diagram - Game CatRun

## Tổng quan
Dự án CatRun là một game Endless Runner được phát triển bằng Unity, nơi người chơi điều khiển một chú mèo chạy không ngừng, né tránh chướng ngại vật và thu thập phần thưởng.

## Sơ đồ lớp chính

```mermaid
classDiagram
    %% Core Game Management
    class GameManager {
        +AState[] states
        +ConsumableDatabase m_ConsumableDatabase
        +AState topState
        +SwitchState(string newState)
        +PushState(string name)
        +PopState()
        +FindState(string stateName)
    }

    class AState {
        <<abstract>>
        +GameManager manager
        +Enter(AState from)*
        +Exit(AState to)*
        +Tick()*
        +GetName()* string
    }

    class GameState {
        +Canvas canvas
        +TrackManager trackManager
        +AudioClip gameTheme
        +Text coinText, premiumText, scoreText
        +StartGame()
        +Pause(bool displayMenu)
        +Resume()
        +GameOver()
    }

    class LoadoutState {
        +Enter(AState from)
        +Exit(AState to)
        +Tick()
        +GetName() string
    }

    class GameOverState {
        +Enter(AState from)
        +Exit(AState to)
        +Tick()
        +GetName() string
    }

    class TutorialState {
        +Enter(AState from)
        +Exit(AState to)
        +Tick()
        +GetName() string
    }

    %% Character System
    class Character {
        +string characterName
        +int cost, premiumCost
        +CharacterAccessories[] accessories
        +Animator animator
        +Sprite icon
        +AudioClip jumpSound, hitSound, deathSound
        +SetupAccesory(int accessory)
    }

    class CharacterInputController {
        +TrackManager trackManager
        +Character character
        +CharacterCollider characterCollider
        +int maxLife, currentLife
        +Consumable inventory
        +List~Consumable~ consumables
        +bool isJumping, isSliding
        +Jump()
        +Slide()
        +ChangeLane(int direction)
        +UseConsumable(Consumable c)
    }

    class CharacterCollider {
        +CharacterInputController controller
        +ParticleSystem koParticle
        +AudioClip coinSound, premiumSound
        +List~GameObject~ magnetCoins
        +bool tutorialHitObstacle
        +Slide(bool sliding)
        +SetInvincible(float timer)
    }

    class CharacterAccessories {
        +string accessoryName
        +int cost
        +int premiumCost
        +Sprite icon
    }

    %% Track and Level System
    class TrackManager {
        +CharacterInputController characterController
        +float minSpeed, maxSpeed
        +int speedStep
        +float laneOffset
        +bool invincible
        +ConsumableDatabase consumableDatabase
        +int score, multiplier
        +float worldDistance, speed
        +TrackSegment currentSegment
        +List~TrackSegment~ segments
        +ThemeData currentTheme
        +bool isMoving, isRerun, isTutorial
        +StartMove(bool isRestart)
        +StopMove()
        +Begin() IEnumerator
        +End()
        +SpawnNewSegment() IEnumerator
        +AddScore(int amount)
    }

    class TrackSegment {
        +float worldLength
        +Transform objectRoot
        +Transform collectibleTransform
        +AssetReference[] possibleObstacles
        +float[] obstaclePositions
        +TrackManager manager
        +GetPointAt(float t, out Vector3 pos, out Quaternion rot)
        +GetPointAtInWorldUnit(float worldUnit, out Vector3 pos, out Quaternion rot)
        +Cleanup()
    }

    %% Obstacle System
    class Obstacle {
        <<abstract>>
        +AudioClip impactedSound
        +Setup()
        +Spawn(TrackSegment segment, float t)* IEnumerator
        +Impacted()
    }

    class AllLaneObstacle {
        +Spawn(TrackSegment segment, float t) IEnumerator
    }

    class SimpleBarricade {
        +Spawn(TrackSegment segment, float t) IEnumerator
    }

    class PatrollingObstacle {
        +Spawn(TrackSegment segment, float t) IEnumerator
    }

    class Missile {
        +Spawn(TrackSegment segment, float t) IEnumerator
    }

    %% Consumable System
    class Consumable {
        <<abstract>>
        +float duration
        +enum ConsumableType
        +Sprite icon
        +AudioClip activatedSound
        +bool canBeSpawned
        +bool active
        +float timeActive
        +GetConsumableType()* ConsumableType
        +GetConsumableName()* string
        +GetPrice()* int
        +GetPremiumCost()* int
        +CanBeUsed(CharacterInputController c) bool
        +Started(CharacterInputController c) IEnumerator
        +Tick(CharacterInputController c)
        +Ended(CharacterInputController c)
    }

    class CoinMagnet {
        +GetConsumableName() string
        +GetConsumableType() ConsumableType
        +GetPrice() int
        +GetPremiumCost() int
        +Tick(CharacterInputController c)
    }

    class ExtraLife {
        +GetConsumableName() string
        +GetConsumableType() ConsumableType
        +GetPrice() int
        +GetPremiumCost() int
        +CanBeUsed(CharacterInputController c) bool
    }

    class Invincibility {
        +GetConsumableName() string
        +GetConsumableType() ConsumableType
        +GetPrice() int
        +GetPremiumCost() int
    }

    class Score2Multiplier {
        +GetConsumableName() string
        +GetConsumableType() ConsumableType
        +GetPrice() int
        +GetPremiumCost() int
    }

    %% Data Management
    class PlayerData {
        +int coins, premium
        +Dictionary~ConsumableType, int~ consumables
        +List~string~ characters
        +int usedCharacter, usedAccessory
        +List~string~ characterAccessories, themes
        +int usedTheme
        +List~HighscoreEntry~ highscores
        +List~MissionBase~ missions
        +string previousName
        +bool licenceAccepted, tutorialDone
        +float masterVolume, musicVolume, masterSFXVolume
        +int ftueLevel, rank
        +Consume(ConsumableType type)
        +Add(ConsumableType type)
        +AddCharacter(string name)
        +AddTheme(string theme)
        +AddAccessory(string name)
        +CheckMissionsCount()
        +AddMission()
        +StartRunMissions(TrackManager manager)
        +UpdateMissions(TrackManager manager)
        +AnyMissionComplete() bool
        +ClaimMission(MissionBase mission)
        +GetScorePlace(int score) int
        +InsertScore(int score, string name)
        +Create()
        +NewSave()
        +Read()
        +Save()
    }

    class HighscoreEntry {
        +string name
        +int score
        +CompareTo(HighscoreEntry other) int
    }

    %% Mission System
    class MissionBase {
        <<abstract>>
        +enum MissionType
        +float progress, max
        +int reward
        +bool isComplete
        +Serialize(BinaryWriter w)
        +Deserialize(BinaryReader r)
        +HaveProgressBar() bool
        +Created()*
        +GetMissionType()* MissionType
        +GetMissionDesc()* string
        +RunStart(TrackManager manager)*
        +Update(TrackManager manager)*
        +GetNewMissionFromType(MissionType type) MissionBase
    }

    class SingleRunMission {
        +Created()
        +HaveProgressBar() bool
        +GetMissionDesc() string
        +GetMissionType() MissionType
        +RunStart(TrackManager manager)
        +Update(TrackManager manager)
    }

    class PickupMission {
        -int previousCoinAmount
        +Created()
        +GetMissionDesc() string
        +GetMissionType() MissionType
        +RunStart(TrackManager manager)
        +Update(TrackManager manager)
    }

    class BarrierJumpMission {
        -Obstacle m_Previous
        -Collider[] m_Hits
        +Created()
        +GetMissionDesc() string
        +GetMissionType() MissionType
        +RunStart(TrackManager manager)
        +Update(TrackManager manager)
    }

    class SlidingMission {
        -float m_PreviousWorldDist
        +Created()
        +GetMissionDesc() string
        +GetMissionType() MissionType
        +RunStart(TrackManager manager)
        +Update(TrackManager manager)
    }

    class MultiplierMission {
        +HaveProgressBar() bool
        +Created()
        +GetMissionDesc() string
        +GetMissionType() MissionType
        +RunStart(TrackManager manager)
        +Update(TrackManager manager)
    }

    %% Database Systems
    class CharacterDatabase {
        +LoadDatabase() IEnumerator
        +GetCharacter(string name) Character
    }

    class ConsumableDatabase {
        +Consumable[] consumbales
        +Load()
        +GetConsumbale(ConsumableType type) Consumable
    }

    class ThemeDatabase {
        +LoadDatabase() IEnumerator
        +GetThemeData(string name) ThemeData
    }

    class ThemeData {
        +string themeName
        +Mesh skyMesh
        +Color fogColor
        +GameObject[] cloudPrefabs
        +int cloudNumber
        +Vector3 cloudMinimumDistance, cloudSpread
        +GameObject premiumCollectible
        +ThemeZone[] zones
    }

    class ThemeZone {
        +float length
        +AssetReference[] prefabList
    }

    %% UI System
    class MainMenu {
        +LoadScene(string name)
    }

    class ShopUI {
        +ShopCharacterList characterList
        +ShopThemeList themeList
        +ShopAccessoriesList accessoriesList
    }

    class ShopItemList {
        +ShopItemListItem[] items
        +ConsumableType[] s_ConsumablesTypes
    }

    class ShopItemListItem {
        +string itemName
        +int cost, premiumCost
        +Sprite icon
        +bool owned
    }

    class MissionUI {
        +MissionEntry[] missionEntries
        +UpdateMissions()
    }

    class MissionEntry {
        +MissionBase mission
        +Text descriptionText
        +Text progressText
        +Button claimButton
        +UpdateMission()
        +ClaimMission()
    }

    class HighscoreUI {
        +Text[] nameTexts
        +Text[] scoreTexts
        +UpdateHighscores()
    }

    class Leaderboard {
        +HighscoreEntry[] entries
        +UpdateLeaderboard()
    }

    %% Audio System
    class MusicPlayer {
        +AudioSource[] stems
        +SetStem(int index, AudioClip clip)
        +RestartAllStems() IEnumerator
        +UpdateVolumes(float speedRatio)
    }

    class CountdownSound {
        +AudioClip[] countdownSounds
        +PlayCountdownSound(int number)
    }

    class AssignOutputChannel {
        +AudioMixerGroup outputChannel
        +AssignToChannel(AudioSource source)
    }

    %% Utility Classes
    class Coin {
        +bool isPremium
        +Pooler coinPool
        +OnTriggerEnter(Collider other)
    }

    class Pooler {
        +GameObject prefab
        +int poolSize
        +Queue~GameObject~ pool
        +Get(Vector3 position, Quaternion rotation) GameObject
        +Free(GameObject obj)
    }

    class CoroutineHandler {
        +StartStaticCoroutine(IEnumerator coroutine) Coroutine
    }

    class Helpers {
        +static utility methods
    }

    class WorldCurver {
        +float curveStrength
        +ApplyCurve(Transform obj)
    }

    class Modifier {
        +OnRunStart(GameState state) bool
        +OnRunTick(GameState state)
        +OnRunEnd(GameState state) bool
    }

    %% Relationships
    GameManager ||--o{ AState : manages
    AState <|-- GameState : implements
    AState <|-- LoadoutState : implements
    AState <|-- GameOverState : implements
    AState <|-- TutorialState : implements

    GameState ||--|| TrackManager : uses
    TrackManager ||--o{ TrackSegment : contains
    TrackManager ||--|| CharacterInputController : controls
    TrackManager ||--|| ConsumableDatabase : uses

    CharacterInputController ||--|| Character : controls
    CharacterInputController ||--|| CharacterCollider : uses
    CharacterInputController ||--o{ Consumable : has
    Character ||--o{ CharacterAccessories : has

    Obstacle <|-- AllLaneObstacle : implements
    Obstacle <|-- SimpleBarricade : implements
    Obstacle <|-- PatrollingObstacle : implements
    Obstacle <|-- Missile : implements

    Consumable <|-- CoinMagnet : implements
    Consumable <|-- ExtraLife : implements
    Consumable <|-- Invincibility : implements
    Consumable <|-- Score2Multiplier : implements

    MissionBase <|-- SingleRunMission : implements
    MissionBase <|-- PickupMission : implements
    MissionBase <|-- BarrierJumpMission : implements
    MissionBase <|-- SlidingMission : implements
    MissionBase <|-- MultiplierMission : implements

    PlayerData ||--o{ HighscoreEntry : contains
    PlayerData ||--o{ MissionBase : contains
    PlayerData ||--|| CharacterDatabase : uses
    PlayerData ||--|| ThemeDatabase : uses

    ThemeData ||--o{ ThemeZone : contains
    CharacterDatabase ||--o{ Character : manages
    ConsumableDatabase ||--o{ Consumable : manages
    ThemeDatabase ||--o{ ThemeData : manages

    ShopUI ||--|| ShopItemList : uses
    ShopItemList ||--o{ ShopItemListItem : contains
    MissionUI ||--o{ MissionEntry : contains
    MissionEntry ||--|| MissionBase : displays

    TrackSegment ||--o{ Obstacle : spawns
    CharacterCollider ||--o{ Coin : collects
    Pooler ||--o{ Coin : manages
```

## Mô tả các thành phần chính

### 1. **Game Management System**
- **GameManager**: Quản lý trạng thái game, chuyển đổi giữa các state
- **AState**: Abstract class cho các trạng thái game
- **GameState**: Trạng thái chơi game chính
- **LoadoutState**: Trạng thái chuẩn bị trước khi chơi
- **GameOverState**: Trạng thái kết thúc game
- **TutorialState**: Trạng thái hướng dẫn

### 2. **Character System**
- **Character**: Dữ liệu nhân vật (tên, giá, phụ kiện, âm thanh)
- **CharacterInputController**: Điều khiển nhân vật (nhảy, trượt, đổi làn)
- **CharacterCollider**: Xử lý va chạm và thu thập vật phẩm
- **CharacterAccessories**: Phụ kiện nhân vật

### 3. **Track & Level System**
- **TrackManager**: Quản lý đường chạy, tốc độ, điểm số
- **TrackSegment**: Đoạn đường với chướng ngại vật và vật phẩm

### 4. **Obstacle System**
- **Obstacle**: Abstract class cho chướng ngại vật
- **AllLaneObstacle**: Chướng ngại vật toàn làn
- **SimpleBarricade**: Rào chắn đơn giản
- **PatrollingObstacle**: Chướng ngại vật tuần tra
- **Missile**: Tên lửa

### 5. **Consumable System**
- **Consumable**: Abstract class cho vật phẩm sử dụng
- **CoinMagnet**: Nam châm hút xu
- **ExtraLife**: Mạng thêm
- **Invincibility**: Bất tử
- **Score2Multiplier**: Nhân đôi điểm

### 6. **Data Management**
- **PlayerData**: Lưu trữ dữ liệu người chơi (xu, nhân vật, nhiệm vụ)
- **HighscoreEntry**: Bảng xếp hạng
- **MissionBase**: Abstract class cho nhiệm vụ
- Các loại nhiệm vụ: SingleRun, Pickup, BarrierJump, Sliding, Multiplier

### 7. **Database Systems**
- **CharacterDatabase**: Quản lý nhân vật
- **ConsumableDatabase**: Quản lý vật phẩm
- **ThemeDatabase**: Quản lý chủ đề
- **ThemeData**: Dữ liệu chủ đề (bầu trời, sương mù, mây)

### 8. **UI System**
- **MainMenu**: Menu chính
- **ShopUI**: Cửa hàng
- **MissionUI**: Giao diện nhiệm vụ
- **HighscoreUI**: Bảng xếp hạng

### 9. **Audio System**
- **MusicPlayer**: Quản lý nhạc nền
- **CountdownSound**: Âm thanh đếm ngược
- **AssignOutputChannel**: Gán kênh âm thanh

### 10. **Utility Classes**
- **Coin**: Xu vàng
- **Pooler**: Quản lý object pool
- **CoroutineHandler**: Xử lý coroutine
- **WorldCurver**: Uốn cong thế giới
- **Modifier**: Sửa đổi game

## Đặc điểm kiến trúc

1. **State Pattern**: GameManager sử dụng state pattern để quản lý các trạng thái game
2. **Abstract Factory**: MissionBase và Consumable sử dụng abstract factory pattern
3. **Object Pooling**: Sử dụng Pooler để tối ưu hiệu suất
4. **Component System**: Unity component system cho Character, CharacterCollider
5. **Data Persistence**: PlayerData lưu trữ dữ liệu người chơi
6. **Modular Design**: Tách biệt rõ ràng giữa các hệ thống (Character, Track, UI, Audio)
