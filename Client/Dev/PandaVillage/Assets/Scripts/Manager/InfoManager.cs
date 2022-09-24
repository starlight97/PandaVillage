using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();

    private Dictionary<int, GameInfo> dicInfos = new Dictionary<int, GameInfo>(); 


    // path에 데이터 파일이 있을경우 기존유저 처리해야함 true 반환
    // 없을경우 신규유저 처리해야함 false 반환
    public bool LoadData()
    {
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);

        if (File.Exists(path))
        {
            Debug.Log("기존 유저");
            var json = File.ReadAllText(path);
            var datas = JsonConvert.DeserializeObject<GameInfo[]>(json);
            datas.ToDictionary(x => x.playerId).ToList().ForEach(x => dicInfos.Add(x.Key, x.Value));
            return true;

        }
        else
        {
            return false;
        }
    }

    // id값이랑 class type 일치하는 info데이터 반환
    public GameInfo GetInfo(int playerId)
    {
        return this.dicInfos[playerId];
    }

    public void UpdateInfo(GameInfo info)
    {
        this.dicInfos[info.playerId] = info;
    }
    public void InsertInfo(GameInfo info)
    {
        this.dicInfos.Add(info.playerId, info);
    }

    public void SaveInfo()
    {
        //IEnumerable<RawData> col = this.dicInfos.Values.Where(x => x.GetType().Equals(typeof(T)));
        //var json = JsonConvert.SerializeObject(col.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList());

        var json = JsonConvert.SerializeObject(this.dicInfos.Values);
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        File.WriteAllText(path, json);
    }

    [MenuItem("PandaVillage/game_info/delete")]
    public static void DeleteGameInfo()
    {
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("game_info.json deleted");
        }
        else
        {
            Debug.Log("game_info.json not found.");
        }

        // https://answers.unity.com/questions/43422/how-to-implement-show-in-explorer.html
        Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
    }

    [MenuItem("PandaVillage/game_info/show in explorer")]
    public static void ShowInExplorer()
    {
        Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
    }

}
