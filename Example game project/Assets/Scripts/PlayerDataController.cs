using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataController : MonoBehaviour
{
    public event UnityAction OnPlayerDataLoaded;
    public event UnityAction OnPlayerDataChanged;
    public event UnityAction OnPlayerDataSaved;

    [SerializeField] private int _ticketsNumber;
    [SerializeField, Range(0, 6)] private int _dailyBonusesNumber;
    private DateTime _lastDailyBonusDate;

    public int TicketsNumber => _ticketsNumber;
    public int DailyBonusesNumber => _dailyBonusesNumber;
    public DateTime LastDailyBonusDate => _lastDailyBonusDate;

    public static PlayerDataController Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadPlayerData();
            return;
        }

        Destroy(gameObject);
    }

    public void AddTickets(int value)
    {
        _ticketsNumber += value;
        SavePlayerData();
        OnPlayerDataChanged?.Invoke();
    }

    public void MarkDailyBonus()
    {
        _dailyBonusesNumber++;
        _lastDailyBonusDate = DateTime.Now;

        if (_dailyBonusesNumber >= 7)
            _dailyBonusesNumber = 0;

        SavePlayerData();
        OnPlayerDataChanged?.Invoke();
    }

    public void ResetNumberOfDays()
    {
        _dailyBonusesNumber = 0;
        _lastDailyBonusDate = DateTime.Now;
        SavePlayerData();
        OnPlayerDataChanged?.Invoke();
    }

    private void LoadPlayerData()
    {
        var data = PlayerDataIO.LoadPlayerData();
        
        // Default player will be created
        if (data == null)
            return;

        _ticketsNumber = data.TicketsNumber;
        _dailyBonusesNumber = data.DailyBonusesNumber;
        _lastDailyBonusDate = data.LastDailyBonusDate;

        OnPlayerDataLoaded?.Invoke();
        OnPlayerDataChanged?.Invoke();
    }

    [ContextMenu("Save player data")]
    private void SavePlayerData()
    {
        var data = new PlayerData(_ticketsNumber, _dailyBonusesNumber, _lastDailyBonusDate);
        PlayerDataIO.SavePlayerData(data);
        OnPlayerDataSaved?.Invoke();
    }
}
