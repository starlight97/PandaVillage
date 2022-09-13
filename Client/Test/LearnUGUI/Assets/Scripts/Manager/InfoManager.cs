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


    // path�� ������ ������ ������� �������� ó���ؾ��� true ��ȯ
    // ������� �ű����� ó���ؾ��� false ��ȯ
    public bool LoadData<T>(string fileName) where T : RawData
    {
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            Debug.Log("���� ����");
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

    // id���̶� class type ��ġ�ϴ� info������ ��ȯ
    public T GetInfo<T>(int id) where T : RawData
    {
        var collection = this.dicInfos.Values.Where(x => x.GetType().Equals(typeof(T)));
        return (T)collection.ToList().Find(x => x.id == id);
    }

    // Ư�� ������ �׷��� �˻��ϰ������ �ش� ������ Ÿ���� ȣ���ϸ� �ش� ������Ÿ�� ��ü�� ��ȯ�Ѵ�.
    // ex(GetDatas<Student>()) = ��ųʸ��� ����� �����͵��� StudentŸ���� ���� ��ü�鸸 ��ȯ
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

    // info������ ����
    public void SaveInfo<T>(string fileName)
    {
        IEnumerable<RawData> col = this.dicInfos.Values.Where(x => x.GetType().Equals(typeof(T)));
        var json = JsonConvert.SerializeObject(col.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList());
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
    }

}
