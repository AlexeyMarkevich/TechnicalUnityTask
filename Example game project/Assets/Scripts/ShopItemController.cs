using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    [Header("Prices and etc.")]
    [SerializeField] private ShopItemPriceType _priceType;
    [SerializeField] private float _realMoneyPrice = 1.99f;
    [SerializeField] private int _virtualMoneyPrice = 500;
    [SerializeField] private int _minLevelBlock;
    [SerializeField] private int _ticketContents = 500;
    [SerializeField] private string _itemText = "[Epic chest]";
    [SerializeField] private TextMeshProUGUI _itemTextField;
    [SerializeField] private string _itemTag;

    [Header("Sell button")]
    [SerializeField] private bool _infinitePurchases;
    [SerializeField] private Button _buyButton;
    [SerializeField] private RectTransform _realMoneyButtonStyle;
    [SerializeField] private RectTransform _virtualMoneyButtonStyle;
    [SerializeField] private RectTransform _soldButtonStyle;
    [SerializeField] private TextMeshProUGUI _realMoneyPriceText;
    [SerializeField] private TextMeshProUGUI _virtualMoneyPriceText;
    [SerializeField] private CodelessIAPButton _iAPButton;

    [Header("Icon")]
    [SerializeField] private Sprite _itemSprite;
    [SerializeField] private Color _itemSpriteColor = Color.white;
    [SerializeField] private Image _itemImageUI;
    [SerializeField] private RectTransform _unlockedIcon;
    [SerializeField] private RectTransform _lockedIcon;
    [SerializeField] private RectTransform _ticketContentsButtonPanel;
    [SerializeField] private TextMeshProUGUI _lockedIconMinLevelText;
    [SerializeField] private TextMeshProUGUI _ticketContentsText;

    [Header("Events")]
    public UnityEvent<ShopItemController> OnBuyButtonClick;

    public string ItemTag => _itemTag;
    public ShopItemPriceType PriceType => _priceType;
    public int TicketContents => _ticketContents;
    public bool InfinitePurchases => _infinitePurchases;
    public float RealMoneyPrice => _realMoneyPrice;
    public int VirutalMoneyPrice => _virtualMoneyPrice;
    public CodelessIAPButton IAPButton => _iAPButton;

    private void Start()
    {
        ApplySettings();
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyButtonClick);
    }

    private void BuyButtonClick()
    {
        OnBuyButtonClick?.Invoke(this);
    }

    public void ApplySettings()
    {
        ApplyText();
        ApplyStyles();
        ApplyBlock();
    }

    private void ApplyStyles()
    {
        switch(_priceType)
        {
            case ShopItemPriceType.RealMoney:
                _realMoneyButtonStyle.gameObject.SetActive(true);
                _virtualMoneyButtonStyle.gameObject.SetActive(false);
                _iAPButton.enabled = true;
                break;

            case ShopItemPriceType.VirtualMoney:
                _realMoneyButtonStyle.gameObject.SetActive(false);
                _virtualMoneyButtonStyle.gameObject.SetActive(true);
                _iAPButton.enabled = false;
                break;
        }

        _soldButtonStyle.gameObject.SetActive(false);

        _ticketContentsButtonPanel.gameObject.SetActive(_ticketContents > 0);

        _itemImageUI.sprite = _itemSprite;
        _itemImageUI.color = _itemSpriteColor;
    }

    private void ApplyBlock()
    {
        bool isLocked = _minLevelBlock > 0 && !LevelsDataController.Instance.IsLevelCompleted(_minLevelBlock);

        _unlockedIcon.gameObject.SetActive(!isLocked);
        _lockedIcon.gameObject.SetActive(isLocked);
        _buyButton.enabled = !isLocked;
    }

    private void ApplyText()
    {
        _itemTextField.text = _itemText;

        _virtualMoneyPriceText.text = _virtualMoneyPrice.ToString();
        _realMoneyPriceText.text = _realMoneyPrice.ToString() + '$';
        _lockedIconMinLevelText.text = "LV. " + _minLevelBlock.ToString();
        _ticketContentsText.text = 'x' + _ticketContents.ToString();
    }

    public void SetSoldStyle()
    {
        _buyButton.enabled = false;
        _virtualMoneyButtonStyle.gameObject.SetActive(false);
        _realMoneyButtonStyle.gameObject.SetActive(false);
        _soldButtonStyle.gameObject.SetActive(true);
    }

    public enum ShopItemPriceType
    {
        VirtualMoney,
        RealMoney
    }
}

[CustomEditor(typeof(ShopItemController))]
public class ShoItemControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10f);
        if (GUILayout.Button("Apply settings"))
            ((ShopItemController)target).ApplySettings();
    }
}
