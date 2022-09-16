using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objGos;
    private int objCount;

    public Vector2Int mapBottomLeft, mapTopRight;


    public void Init()
    {
        this.objCount = objGos.Length;
        //SpawnObjects();
    }

    public void SpawnObjects()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("WallObject"));

        for (int y = mapBottomLeft.y; y <= mapTopRight.y; y++)
        {
            for (int x = mapBottomLeft.x; x <= mapTopRight.x; x++)
            {
                var cols = Physics2D.OverlapBoxAll(new Vector2(x + 0.5f, y + 0.5f), new Vector2(0.95f, 0.95f), 0, layerMask);
                //Debug.Log(cols.Length);
                if (cols.Length == 0)
                {
                    var rand = Random.Range(0, 5);

                    if (rand == 4)
                    {
                        rand = Random.Range(0, objCount);
                        GameObject objGo = Instantiate<GameObject>(objGos[rand]);
                        objGo.transform.position = new Vector3(x, y, 0);
                        objGo.transform.parent = this.transform;
                    }
                }
            }
        }
    }

    public void SpawnObject()
    {
        List<Vector2> emptyPosList = new List<Vector2>();
        int layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("WallObject"));

        for (int y = mapBottomLeft.y; y <= mapTopRight.y; y++)
        {
            for (int x = mapBottomLeft.x; x <= mapTopRight.x; x++)
            {
                var cols = Physics2D.OverlapBoxAll(new Vector2(x + 0.5f, y + 0.5f), new Vector2(0.95f, 0.95f), 0, layerMask);
                //Debug.Log(cols.Length);
                if (cols.Length == 0)
                {
                    emptyPosList.Add(new Vector2(x, y));
                }
            }
        }

        var randPosIdx = Random.Range(0, emptyPosList.Count);
        var randObj = Random.Range(0, objCount);

        GameObject objGo = Instantiate<GameObject>(objGos[randObj]);
        objGo.transform.position = new Vector3(emptyPosList[randPosIdx].x, emptyPosList[randPosIdx].y, 0);
        objGo.transform.parent = this.transform;
    }
}
