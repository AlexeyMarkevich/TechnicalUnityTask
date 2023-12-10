using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private bool _isBlocked;

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private RectTransform _blockLevelButton;

    public int LevelNumber => _levelNumber;

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

    public void ApplyButtonStyle(bool isBlocked)
    {
        _isBlocked = isBlocked;

        _levelNumberText.text = _levelNumber.ToString();
        _blockLevelButton.gameObject.SetActive(isBlocked);
        _button.enabled = !isBlocked;
    }

    [ContextMenu("Apply button style")]
    private void ApplyButtonStyle() => ApplyButtonStyle(_isBlocked);

    private void ButtonClick()
    {
        LevelsDataController.Instance.SetLevelStatus(_levelNumber, true);
    }
}
