using UnityEngine;

public class LoadAudioButtonsStates : MonoBehaviour
{
    [SerializeField] private RectTransform _disabledSoundImage;
    [SerializeField] private RectTransform _disabledMusicImage;

    private void OnEnable()
    {
        LoadStates();
    }

    private void LoadStates()
    {
        var audioController = AudioController.Instance;
        if (audioController == null)
            return;

        if (_disabledSoundImage != null)
        {
            var isMuted = audioController.SoundVolume == 0;
            _disabledSoundImage.gameObject.SetActive(isMuted);
        }

        if (_disabledMusicImage != null)
        {
            var isMuted = audioController.MusicVolume == 0;
            _disabledMusicImage.gameObject.SetActive(isMuted);
        }
    }
}
