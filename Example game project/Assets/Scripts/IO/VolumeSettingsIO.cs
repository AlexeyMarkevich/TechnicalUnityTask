using UnityEngine;

public static class VolumeSettingsIO
{
    private static readonly string Path = Application.persistentDataPath + @"\VolumeSettings.dat";

    public static void SaveVolumeSettings(VolumeSettings settings)
    {
        IOController.SaveData(settings, Path);
    }

    public static VolumeSettings LoadVolumeSettings()
    {
        return IOController.LoadData<VolumeSettings>(Path);
    }

}
