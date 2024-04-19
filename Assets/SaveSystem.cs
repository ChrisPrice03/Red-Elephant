using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (Player player, terrainGeneration terrain) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.thing";
        FileStream fs = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, terrain);

        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static SaveData loadPlayer() {
        string path = Application.persistentDataPath + "/playerData.thing";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            
            FileStream fs = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(fs) as SaveData;
            fs.Close();

            return data;
        }
        else {
            return null;
        }
    }

    public static void DelData() {
        string path = Application.persistentDataPath + "/playerData.thing";
        if (File.Exists(path)) {
            File.Delete(path);
        }
    }

}
