using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DailyButtonController : MonoBehaviour
{
    private const string DayNumberText = "DAY";
    private const string NumberOfTicketsText = "X";

    [SerializeField] private int _dayNumber = 1;
    [SerializeField] private int _numberOfTickets = 5;

    [SerializeField] private TextMeshProUGUI _dayNumberText;
    [SerializeField] private TextMeshProUGUI _numberOfTicketsText;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _foreground;
    [SerializeField] private RectTransform _foregroundArrow;
    [SerializeField] private DailyButtonStyles _buttonStyle;

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

    public void ChangeButtonStyle(DailyButtonStyles style)
    {
        _buttonStyle = style;
        ApplyButtonStyle();
        OnButtonStyleChanged?.Invoke();
    }

    [ContextMenu("Apply button style")]
    private void ApplyButtonStyle()
    {
        _dayNumberText.text = DayNumberText + _dayNumber.ToString();
        _numberOfTicketsText.text = NumberOfTicketsText + _numberOfTickets.ToString();

        switch (_buttonStyle)
        {
            case DailyButtonStyles.ActiveToCollect:
                _button.enabled = true;
                _foreground.gameObject.SetActive(false);
                _foregroundArrow.gameObject.SetActive(false);
                break;

            case DailyButtonStyles.Collected:
                _button.enabled = false;
                _foreground.gameObject.SetActive(true);
                _foregroundArrow.gameObject.SetActive(true);
                break;

            case DailyButtonStyles.Blocked:
                _button.enabled = false;
                _foreground.gameObject.SetActive(true);
                _foregroundArrow.gameObject.SetActive(false);
                break;
        }
    }

    private void ButtonClick()
    {
        PlayerDataController.Instance.MarkDailyBonus();
        PlayerDataController.Instance.AddTickets(_numberOfTickets);
    }

    public enum DailyButtonStyles
    {
        ActiveToCollect,
        Collected,
        Blocked
    }
}
