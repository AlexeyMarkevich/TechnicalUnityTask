using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class ButtonClickSound : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlaySound);
    }

    private void PlaySound()
    {
        var audioController = AudioController.Instance;

        if (audioController == null)
            return;

        var sound = audioController.ButtonClickSound;
        if (sound == null)
            return;

        _audioSource.PlayOneShot(sound, audioController.SoundVolume);
    }
}
