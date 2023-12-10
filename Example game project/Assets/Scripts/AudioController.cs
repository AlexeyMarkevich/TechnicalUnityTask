using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    public event UnityAction<float> OnSoundVolumeChanged;
    public event UnityAction<float> OnMusicVolumeChanged;

    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField, Range(0, 1)] private float _soundVolume = 1f;
    [SerializeField, Range(0, 1)] private float _musicVolume = 1f;

    public AudioClip ButtonClickSound => _buttonClickSound;
    public AudioClip BackgroundMusic => _backgroundMusic;
    public float SoundVolume => _soundVolume;
    public float MusicVolume => _musicVolume;

    public static AudioController Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            LoadVolumeSettings();
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public void SetSoundVolume(float value)
    {
        _soundVolume = Mathf.Clamp01(value);
        OnSoundVolumeChanged?.Invoke(_soundVolume);
        SaveVolumeSettings();
    }

    public void SetMusicVolume(float value)
    {
        _musicVolume = Mathf.Clamp01(value);
        OnMusicVolumeChanged?.Invoke(_musicVolume);
        SaveVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        var settings = VolumeSettingsIO.LoadVolumeSettings();

        // Default values
        if (settings == null)
            return;

        _soundVolume = settings.SoundVolume;
        _musicVolume = settings.MusicVolume;

        OnSoundVolumeChanged?.Invoke(_soundVolume);
        OnMusicVolumeChanged?.Invoke(_musicVolume);
    }

    private void SaveVolumeSettings()
    {
        var settings = new VolumeSettings(_soundVolume, _musicVolume);
        VolumeSettingsIO.SaveVolumeSettings(settings);
    }
}
