using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

public class InfoManager 
{
    public static readonly InfoManager instance = new InfoManager();

    public UnityEvent onDataLoadFinished = new UnityEvent();

    private Dictionary<int ,RawData> dicInfos = new Dictionary<int, RawData>();


    // path에 데이터 파일이 있을경우 기존유저 처리해야함 true 반환
    // 없을경우 신규유저 처리해야함 false 반환
    public bool LoadData<T>(string fileName) where T : RawData
    {
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            Debug.Log("기존 유저");
            var json = File.ReadAllText(path);
            var datas = JsonConvert.DeserializeObject<T[]>(json);
            datas.ToDictionary(x => x.id).ToList().ForEach(x => dicInfos.Add(x.Key, x.Value));
            onDataLoadFinished.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    // id값이랑 class type 일치하는 info데이터 반환
    public T GetInfo<T>(int id) where T : RawData
    {
        var collection = this.dicInfos.Values.Where(x => x.GetType().Equals(typeof(T)));
        return (T)collection.ToList().Find(x => x.id == id);
    }

    // 특정 데이터 그룹을 검색하고싶을때 해당 데이터 타입을 호출하면 해당 데이터타입 객체만 반환한다.
    // ex(GetDatas<Student>()) = 딕셔너리에 저장된 데이터들중 Student타입을 가진 객체들만 반환
    public IEnumerable<T> GetInfoList<T>() where T : RawData
    {
        IEnumerable<RawData> col = this.dicInfos.Values.Where(x => x.GetType().Equals(typeof(T)));
        return col.Select(x => (T)Convert.ChangeType(x, typeof(T)));
    }

    public void UpdateInfo(RawData info)
    {
        this.dicInfos[info.id] = info;
    }
    public void InsertInfo(RawData info)
    {
        this.dicInfos.Add(info.id, info);
    }

    // info데이터 저장
    public void SaveInfo<T>(string fileName)
    {
        IEnumerable<RawData> col = this.dicInfos.Values.Where(x => x.GetType().Equals(typeof(T)));
        var json = JsonConvert.SerializeObject(col.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList());
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
    }

}
