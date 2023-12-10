using UnityEngine;

public class ShopController : MonoBehaviour
{
    public ShopItemController[] _shopItems;

    private PurchasesData _puchasesData;

    public static ShopController Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        LoadPurchases();

        for (int i = 0; i < _shopItems.Length; i++)
        {
            _shopItems[i].OnBuyButtonClick.AddListener(OnBuyButtonClick);
            if (IsContainsPurchase(_shopItems[i].ItemTag))
            {
                _shopItems[i].SetSoldStyle();
            }
        }
    }

    public void SavePurchases()
    {
        PurchasesDataIO.SavePurchasesData(_puchasesData);
    }

    private void LoadPurchases()
    {
        _puchasesData = PurchasesDataIO.LoadPurchasesData();

        if (_puchasesData == null)
            _puchasesData = new PurchasesData(new System.Collections.Generic.List<string>());

        if (_puchasesData.Purchases == null)
            _puchasesData.Purchases = new System.Collections.Generic.List<string>();
    }

    private void AddPurchase(string tag)
    {
        if (IsContainsPurchase(tag))
            throw new System.ArgumentException("This tag is already exists", nameof(tag));

        _puchasesData.Purchases.Add(tag);
        SavePurchases();
    }

    public bool IsContainsPurchase(string tag)
    {
        if (_puchasesData == null || _puchasesData.Purchases == null)
            return false;

        return _puchasesData.Purchases.Contains(tag);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _shopItems.Length; i++)
            _shopItems[i].OnBuyButtonClick.RemoveListener(OnBuyButtonClick);
    }

    private void OnBuyButtonClick(ShopItemController controller)
    {
        var priceType = controller.PriceType;
        var vmp = controller.VirutalMoneyPrice;
        var rmp = controller.RealMoneyPrice;
        var additionalTickets = controller.TicketContents;
        var itemTag = controller.ItemTag;
        var infinitePurchases = controller.InfinitePurchases;

        if (priceType != ShopItemController.ShopItemPriceType.VirtualMoney)
            return;

        if (PlayerDataController.Instance.TicketsNumber < vmp)
            return;

        var ticketsToAdd = -vmp + additionalTickets;
        PlayerDataController.Instance.AddTickets(ticketsToAdd);

        if (!infinitePurchases)
        {
            controller.SetSoldStyle();
            AddPurchase(itemTag);
        }
    }

    public void OnIAPButtonClick(int tickets)
    {
        PlayerDataController.Instance.AddTickets(tickets);
    }
}
