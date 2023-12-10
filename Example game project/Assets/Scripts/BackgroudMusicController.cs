using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroudMusicController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AudioController.Instance.OnMusicVolumeChanged += OnMusicVolumeChanged;

        _audioSource.clip = AudioController.Instance.BackgroundMusic;
        _audioSource.volume = AudioController.Instance.MusicVolume;
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        AudioController.Instance.OnMusicVolumeChanged -= OnMusicVolumeChanged;
    }

    private void OnMusicVolumeChanged(float value)
    {
        _audioSource.volume = value;
    }
}
