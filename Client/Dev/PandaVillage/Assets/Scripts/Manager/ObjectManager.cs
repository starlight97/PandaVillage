using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManager : MonoBehaviour
{
    public enum eSpawnPlace
    {
        All,
        Alley,
        MountainRange,
        Farm,
        BusStop,
        PelicanVillage,
        CindersapForest,
        SecretForest,
    }

    public UnityAction<Farming.eFarmTileType> onGetTilePosList;
    private ObjectSpawner objectSpawner;

    private List<Vector3Int> grassPosList = new List<Vector3Int>();


    public void Init(string sceneName, List<Vector3Int> grassPosList)
    {
        this.objectSpawner = GameObject.FindObjectOfType<ObjectSpawner>();
        this.objectSpawner.Init();
        this.grassPosList = grassPosList;
        //var objectInfoList = InfoManager.instance.GetInfo(1).objectInfoList.FindAll(x => x.sceneName == sceneName);

        //foreach (var info in objectInfoList)
        //{
        //    objectSpawner.SpawnObject(info.objectId, new Vector2Int(info.posX, info.posY));
        //}

    }

    public List<OtherObject> GetOtherObjectist()
    {
        return objectSpawner.OtherObjectList;
    }

    public void SpawnObjects()
    {
        //this.objectSpawner.GrassTileSetting();
        this.objectSpawner.SpawnObject(5000);
        this.objectSpawner.SpawnObject(5001);
        this.objectSpawner.SpawnObject(5002);
        this.objectSpawner.SpawnObject(5003);
        this.objectSpawner.SpawnObject(5004);
        this.objectSpawner.SpawnObject(5005);

    }



}
