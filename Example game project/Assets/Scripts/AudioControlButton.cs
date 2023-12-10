using UnityEngine;
using UnityEngine.UI;

public class AudioControlButton : MonoBehaviour
{
    [SerializeField] private AudioType _audioType;
    [SerializeField] private RectTransform _disabledButtonImage;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(AudioControlClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AudioControlClick);
    }

    private void AudioControlClick()
    {
        var audioController = AudioController.Instance;

        if (audioController == null)
            return;

        switch (_audioType)
        {
            case AudioType.Sound:
                if (audioController.SoundVolume == 0)
                {
                    audioController.SetSoundVolume(1);
                    SetDisabledStyleVisible(false);
                }
                else
                {
                    audioController.SetSoundVolume(0);
                    SetDisabledStyleVisible(true);
                }
                break;

            case AudioType.Music:
                if (audioController.MusicVolume == 0)
                {
                    audioController.SetMusicVolume(1);
                    SetDisabledStyleVisible(false);
                }
                else
                {
                    audioController.SetMusicVolume(0);
                    SetDisabledStyleVisible(true);
                }
                break;
        }
    }

    private void SetDisabledStyleVisible(bool visible)
    {
        _disabledButtonImage.gameObject.SetActive(visible);
    }

    public enum AudioType
    {
        Sound,
        Music
    }
}
