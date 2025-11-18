using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ShopUIPanel : MonoSingleton<ShopUIPanel>
{
    [Header("Shop Lists (Panels)")]
    [SerializeField] private ShopItemList itemList;
    [SerializeField] private ShopCharacterList characterList;
    [SerializeField] private ShopAccessoriesList accessoriesList;
    [SerializeField] private ShopThemeList themeList;

    [Header("Buttons")]
    [SerializeField] private Button itemListBtn;
    [SerializeField] private Button characterListBtn;
    [SerializeField] private Button accessoriesListBtn;
    [SerializeField] private Button themeListBtn;

    [Header("Button Sprites")]
    [SerializeField] private Sprite selectedBtnSprite;
    [SerializeField] private Sprite normalBtnSprite;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinCounter;
    [SerializeField] private TextMeshProUGUI premiumCounter;
    [SerializeField] private Button cheatButton;
    [SerializeField] private Image backgroundImage;

    private Dictionary<Button, MonoBehaviour> _tabs;
    private Button _currentTabBtn;

    protected const int k_CheatCoins = 1000000;
    protected const int k_CheatPremium = 1000;

    protected override void Awake()
    {
        base.Awake();
        _tabs = new Dictionary<Button, MonoBehaviour>();
        TryAddTab(itemListBtn, itemList);
        TryAddTab(characterListBtn, characterList);
        TryAddTab(accessoriesListBtn, accessoriesList);
        TryAddTab(themeListBtn, themeList);

        foreach (var kvp in _tabs)
        {
            var btn = kvp.Key;
            btn.onClick.AddListener(() => OnTabSelected(btn));
        }
    }
    private void Start()
    {
        PlayerData.Create();

        CoroutineHandler.StartStaticCoroutine(CharacterDatabase.LoadDatabase());
        CoroutineHandler.StartStaticCoroutine(ThemeDatabase.LoadDatabase());


    }
    void OnEnable()
    {
        if(itemList != null)
            OnTabSelected(itemListBtn);
        UpdateDataTxt();
    }

    public void UpdateDataTxt()
    {
        coinCounter.text = PlayerData.instance.coins.ToString();
        premiumCounter.text = PlayerData.instance.premium.ToString();
    }

    private void TryAddTab(Button btn, MonoBehaviour panel)
    {
        if (btn != null && panel != null)
            _tabs.Add(btn, panel);
        else
            Debug.LogWarning($"[ShopUIPanel] Missing reference for {btn?.name ?? "Button"} or {panel?.name ?? "Panel"}");
    }

    private void OnTabSelected(Button clickedBtn)
    {
        if (_currentTabBtn == clickedBtn)
            return;

        _currentTabBtn = clickedBtn;

        foreach (var kvp in _tabs)
        {
            var btn = kvp.Key;
            var panel = kvp.Value;

            if (btn == null || panel == null) continue;

            bool isActive = btn == clickedBtn;
            panel.gameObject.SetActive(isActive);
            btn.image.sprite = isActive ? selectedBtnSprite : normalBtnSprite;

            if (isActive)
            {
                switch (panel)
                {
                    case ShopItemList il: il.Open(); break;
                    case ShopCharacterList cl: cl.Open(); break;
                    case ShopAccessoriesList al: al.Open(); break;
                    case ShopThemeList tl: tl.Open(); break;
                }
            }
            else
            {
                switch (panel)
                {
                    case ShopItemList il: il.Close(); break;
                    case ShopCharacterList cl: cl.Close(); break;
                    case ShopAccessoriesList al: al.Close(); break;
                    case ShopThemeList tl: tl.Close(); break;
                }
            }
        }
    }
    public void OnBackgroundClick(BaseEventData data)
    {
        PointerEventData p = (PointerEventData)data;
        if (p.pointerCurrentRaycast.gameObject == backgroundImage.gameObject)
            CloseScene();
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void CloseScene()
    {
        SceneManager.UnloadSceneAsync("shop");
        LoadoutState loadoutState = GameManager.instance.topState as LoadoutState;
        if (loadoutState != null)
            loadoutState.Refresh();
    }

    public void CheatCoin()
    {
        PlayerData.instance.coins += k_CheatCoins;
        PlayerData.instance.premium += k_CheatPremium;
        PlayerData.instance.Save();
        UpdateDataTxt();
    }
}
