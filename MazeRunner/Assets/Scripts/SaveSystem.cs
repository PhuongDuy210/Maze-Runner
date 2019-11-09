using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
    public static void SavePlayer(int[] highScores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData playerData = new PlayerData(highScores);

        formatter.Serialize(stream, playerData);
        stream.Close();

    }

    public static PlayerData LoadPLayer()
    {
        string path = Application.dataPath + "/player.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        }
        else
        {
            Debug.Log("No save file");
            return null;
        }
    }
}
