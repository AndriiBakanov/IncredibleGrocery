using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    static string playerPath = Application.persistentDataPath + "/player.bin";
    static string settingsPath = Application.persistentDataPath + "/settings.bin";
    public static void SavePlayer(PlayerSave save) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(playerPath, FileMode.Create)) 
        {
            formatter.Serialize(fileStream, save);
        }
    }
    public static void SaveSettings(Settings settings) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(settingsPath, FileMode.Create))
        {
            formatter.Serialize(fileStream, settings);
        }
    }
    public static PlayerSave LoadPlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        PlayerSave playerSave = new PlayerSave();
        if (File.Exists(playerPath)) 
        {
            using (FileStream fileStream = new FileStream(playerPath, FileMode.Open))
            {
                playerSave = (PlayerSave)formatter.Deserialize(fileStream);
            }
        }
        return playerSave;
    }
    public static Settings LoadSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Settings settings = new Settings();
        if (File.Exists(settingsPath))
        {
            using (FileStream fileStream = new FileStream(settingsPath, FileMode.Open))
            {
                settings = (Settings)formatter.Deserialize(fileStream);
            }
        }
        return settings;
    }
}
