using UnityEngine;

public static class LevelsDataIO
{
    private static readonly string Path = Application.persistentDataPath + @"\LevelsData.dat";

    public static void SaveLevelsData(LevelsData data)
    {
        IOController.SaveData(data, Path);
    }

    public static LevelsData LoadLevelsData()
    {
        return IOController.LoadData<LevelsData>(Path);
    }
}
