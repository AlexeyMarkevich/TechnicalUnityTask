using UnityEngine;

internal class PurchasesDataIO
{
    private static readonly string Path = Application.persistentDataPath + @"\Purchases.dat";

    public static void SavePurchasesData(PurchasesData data)
    {
        IOController.SaveData(data, Path);
    }

    public static PurchasesData LoadPurchasesData()
    {
        return IOController.LoadData<PurchasesData>(Path);
    }
}
