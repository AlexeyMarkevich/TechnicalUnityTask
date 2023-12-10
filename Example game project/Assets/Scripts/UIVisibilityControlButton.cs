using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIVisibilityControlButton : MonoBehaviour
{
    [SerializeField] private RectTransform[] _panels;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeActivePanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeActivePanel);
    }

    private void ChangeActivePanel()
    {
        if (_panels == null)
            return;

        foreach (var panel in _panels)
        {
            if (panel == null)
                continue;

            var isActive = panel.gameObject.activeSelf;
            panel.gameObject.SetActive(!isActive);
        }
    }
}
