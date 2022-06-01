using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    const string PATH_EXT = "/player.data";

    public static void SavePlayer (Health player) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + PATH_EXT;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer() {
        string path = Application.persistentDataPath + PATH_EXT;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        } 
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
