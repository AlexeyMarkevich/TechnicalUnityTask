using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class WeeklyBonusProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        PlayerDataController.Instance.OnPlayerDataChanged += UpdateWeeklyBonusProgress;
        UpdateWeeklyBonusProgress();
    }

    private void OnDisable()
    {
        PlayerDataController.Instance.OnPlayerDataChanged -= UpdateWeeklyBonusProgress;
    }

    private void UpdateWeeklyBonusProgress()
    {
        var currentProgress = PlayerDataController.Instance.DailyBonusesNumber;

        _slider.value = currentProgress;
    }
}
