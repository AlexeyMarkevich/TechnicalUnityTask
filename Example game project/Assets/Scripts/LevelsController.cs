using UnityEngine;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private LevelButtonController[] _levels;

    private void OnEnable()
    {
        LevelsDataController.Instance.OnLevelsDataChanged += LoadLevelsProgress;
        LoadLevelsProgress();
    }

    private void OnDisable()
    {
        LevelsDataController.Instance.OnLevelsDataChanged -= LoadLevelsProgress;
    }

    private void LoadLevelsProgress()
    {
        var levelsDataController = LevelsDataController.Instance;

        foreach (var level in _levels)
            level.ApplyButtonStyle(true);

        for (int i = 1; i <= _levels.Length; i++)
        {
            if (levelsDataController.IsLevelCompleted(i))
            {
                _levels[i - 1].ApplyButtonStyle(false);
            }
            else
            {
                _levels[i - 1].ApplyButtonStyle(false);
                break;
            }
        }
    }
}
