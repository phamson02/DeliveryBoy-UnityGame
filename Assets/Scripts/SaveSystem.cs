using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(GameData data){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load(){
        if(!File.Exists(GetPath())){
            GameData emptyData = new GameData();
            Save(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetPath(), FileMode.Open);
        GameData data = formatter.Deserialize(stream) as GameData;
        stream.Close();

        return data;
    }


    private static string GetPath(){
        return Application.persistentDataPath + "/data.dvb";
    }
}
