using System;

[Serializable]
public class VolumeSettings
{
    public VolumeSettings(float soundVolume, float musicVolume)
    {
        SoundVolume = soundVolume;
        MusicVolume = musicVolume;
    }

    public float SoundVolume { get; set; }
    public float MusicVolume { get; set; }
}
