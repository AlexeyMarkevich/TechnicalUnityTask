using System;
using UnityEngine;

public class DailyRewardController : MonoBehaviour
{
    [SerializeField] private DailyButtonController[] _dayButtons;
    [SerializeField] private WeeklyButtonController _weeklyButton;

    private void OnEnable()
    {
        PlayerDataController.Instance.OnPlayerDataChanged += LoadPanel;

        LoadPanel();
    }

    private void OnDisable()
    {
        PlayerDataController.Instance.OnPlayerDataChanged -= LoadPanel;
    }

    private void LoadPanel()
    {
        var playerDataInstance = PlayerDataController.Instance;
        if (playerDataInstance == null)
            return;

        if ((DateTime.Now - playerDataInstance.LastDailyBonusDate).Days > 1)
            playerDataInstance.ResetNumberOfDays();

        foreach (var dayButton in _dayButtons)
            dayButton.ChangeButtonStyle(DailyButtonController.DailyButtonStyles.Blocked);
        _weeklyButton.ChangeButtonStyle(WeeklyButtonController.WeeklyButtonStyles.Disabled);

        int day = 0;
        for (; day < playerDataInstance.DailyBonusesNumber; day++)
            _dayButtons[day].ChangeButtonStyle(DailyButtonController.DailyButtonStyles.Collected);

        if (playerDataInstance.LastDailyBonusDate.Date != DateTime.Now.Date)
        {
            if (day >= 6)
            {
                _weeklyButton.ChangeButtonStyle(WeeklyButtonController.WeeklyButtonStyles.Active);
            }
            else
            {
                _dayButtons[day].ChangeButtonStyle(DailyButtonController.DailyButtonStyles.ActiveToCollect);
            }
        }
    }
}
