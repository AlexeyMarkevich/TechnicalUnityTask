using System;

[Serializable]
public class PlayerData
{
    public PlayerData(int ticketsNumber, int dailyBonusesNumber, DateTime lastDailyBonusDate)
    {
        TicketsNumber = ticketsNumber;
        DailyBonusesNumber = dailyBonusesNumber;
        LastDailyBonusDate = lastDailyBonusDate;
    }

    public int TicketsNumber { get; set; }
    public int DailyBonusesNumber { get; set; }
    public DateTime LastDailyBonusDate { get; set; }
}
