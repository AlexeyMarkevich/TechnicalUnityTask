using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelsDataController : MonoBehaviour
{
    public event UnityAction OnLevelsDataLoaded;
    public event UnityAction OnLevelsDataChanged;
    public event UnityAction OnLevelsDataSaved;

    [SerializeField] private LevelsData _levelsData;

    public static LevelsDataController Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadLevelsData();
            return;
        }

        Destroy(gameObject);
    }

    public bool IsLevelCompleted(int levelNumber)
    {
        if (!_levelsData.Levels.TryGetValue(levelNumber, out bool isCompleted))
            throw new ArgumentException(nameof(levelNumber));

        return isCompleted;
    }

    public void SetLevelStatus(int levelNumber, bool isCompleted)
    {
        if (!_levelsData.Levels.ContainsKey(levelNumber))
            throw new ArgumentException(nameof(levelNumber));

        _levelsData.Levels[levelNumber] = isCompleted;
        OnLevelsDataChanged?.Invoke();
        SaveLevelsData();
    }

    private void LoadLevelsData()
    {
        var data = LevelsDataIO.LoadLevelsData();

        // Default player will be created
        if (data == null)
            data = new LevelsData();

        if (data.Levels == null)
        {
            data.Levels = new System.Collections.Generic.Dictionary<int, bool>();
            for (int i = 1; i <= 20; i++)
                data.Levels.Add(i, false);
        }

        _levelsData = data;

        OnLevelsDataLoaded?.Invoke();
        OnLevelsDataChanged?.Invoke();
    }

    private void SaveLevelsData()
    {
        LevelsDataIO.SaveLevelsData(_levelsData);
        OnLevelsDataSaved?.Invoke();
    }
}
