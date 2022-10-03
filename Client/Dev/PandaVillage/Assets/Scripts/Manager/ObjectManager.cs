using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManager : MonoBehaviour
{
    public UnityAction<Farming.eFarmTileType> onGetTilePosList;
    private ObjectSpawner objectSpawner;

    private List<Vector3Int> objectInfoList = new List<Vector3Int>();
    private List<Vector3Int> grassPosList = new List<Vector3Int>();


    public void Init(string sceneName, List<Vector3Int> grassPosList)
    {
        this.objectSpawner = GameObject.FindObjectOfType<ObjectSpawner>();
        this.grassPosList = grassPosList;
        var objectInfoList = InfoManager.instance.GetInfo(1).objectInfoList.FindAll(x => x.sceneName == sceneName);

        foreach (var info in objectInfoList)
        {
            objectSpawner.SpawnObject(info.objectId, new Vector2Int(info.posX, info.posY));
        }
        if(objectInfoList.Count == 0)
        {

        }
    }

    private void FindObjectPosList()
    {
        this.objectInfoList.Clear();
        int objectCount = this.objectSpawner.transform.childCount;
        for (int index = 0; index < objectCount; index++)
        {
            this.objectInfoList.Add(new Vector3Int((int)this.objectSpawner.transform.GetChild(index).transform.position.x, (int)this.objectSpawner.transform.GetChild(index).transform.position.y, 0));
        }
    }

    public List<Vector3Int> GetObjectInfoList()
    {
        this.FindObjectPosList();
        return objectInfoList;
    }

    public void SpawnObjects()
    {
        //this.objectSpawner.GrassTileSetting();
        //this.objectSpawner.SpawnObjects();
    }



}
