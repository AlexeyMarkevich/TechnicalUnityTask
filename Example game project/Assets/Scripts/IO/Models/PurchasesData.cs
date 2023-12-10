using System;
using System.Collections.Generic;

[Serializable]
public class PurchasesData
{
    public PurchasesData(List<string> purchases)
    {
        Purchases = purchases;
    }

    public List<string> Purchases { get; set; }
}
