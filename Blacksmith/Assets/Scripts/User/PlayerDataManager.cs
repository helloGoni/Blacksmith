using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance = null;

    string path;
    string filename = "user";
    UserData userdata = new UserData();
    

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if ( instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);             
        path = Application.persistentDataPath;
    }

    void SaveData() {
        string data = JsonUtility.ToJson(userdata);
        File.WriteAllText(path+filename, data);
    }
    
    void LoadData() {
        string data = File.ReadAllText(path+filename);
        userdata = JsonUtility.FromJson<UserData>(data);
    }


}
