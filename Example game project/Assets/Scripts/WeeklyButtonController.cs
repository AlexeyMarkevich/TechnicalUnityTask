using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeeklyButtonController : MonoBehaviour
{
    [SerializeField] private int _numberOfTickets = 50;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _activeRewardImage;
    [SerializeField] private RectTransform _disabledButtonImage;
    [SerializeField] private WeeklyButtonStyles _buttonStyle;

    public UnityEvent OnButtonStyleChanged;

    private void Start()
    {
        ApplyButtonStyle();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }
    public void ChangeButtonStyle(WeeklyButtonStyles style)
    {
        _buttonStyle = style;
        ApplyButtonStyle();
        OnButtonStyleChanged?.Invoke();
    }

    private void ButtonClick()
    {
        PlayerDataController.Instance.MarkDailyBonus();
        PlayerDataController.Instance.AddTickets(_numberOfTickets);
    }


    [ContextMenu("Apply button style")]
    private void ApplyButtonStyle()
    {
        switch (_buttonStyle)
        {
            case WeeklyButtonStyles.Active:
                _button.enabled = true;
                _activeRewardImage.gameObject.SetActive(true);
                _disabledButtonImage.gameObject.SetActive(false);
                break;

            case WeeklyButtonStyles.Disabled:
                _button.enabled = false;
                _activeRewardImage.gameObject.SetActive(false);
                _disabledButtonImage.gameObject.SetActive(true);
                break;
        }
    }

    public enum WeeklyButtonStyles
    {
        Disabled,
        Active
    }
}
