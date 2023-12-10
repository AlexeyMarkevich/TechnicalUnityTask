using UnityEngine;

public static class PlayerDataIO
{
    private static readonly string Path = Application.persistentDataPath + @"\PlayerData.dat";

    public static void SavePlayerData(PlayerData settings)
    {
        IOController.SaveData(settings, Path);
    }

    public static PlayerData LoadPlayerData()
    {
        return IOController.LoadData<PlayerData>(Path);
    }

}
