using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TicketsNumberTextOutput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        PlayerDataController.Instance.OnPlayerDataChanged += UpdateTicketsInfo;
        UpdateTicketsInfo();
    }

    private void OnEnable()
    {
        if (PlayerDataController.Instance == null)
            return;

        PlayerDataController.Instance.OnPlayerDataChanged += UpdateTicketsInfo;
        UpdateTicketsInfo();
    }

    private void OnDisable()
    {
        PlayerDataController.Instance.OnPlayerDataChanged -= UpdateTicketsInfo;
    }

    private void UpdateTicketsInfo()
    {
        if (PlayerDataController.Instance == null)
            return;

        _textMeshProUGUI.text = PlayerDataController.Instance.TicketsNumber.ToString();
    }
}
