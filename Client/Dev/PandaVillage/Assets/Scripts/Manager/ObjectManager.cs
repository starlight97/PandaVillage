using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ObjectSpawner objectSpawner;

    private List<Vector3Int> objectInfoList = new List<Vector3Int>();

    public void Init()
    {
        this.objectSpawner = GameObject.FindObjectOfType<ObjectSpawner>();
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
