using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    public Vector2Int mapBottomLeft, mapTopRight;
    public SpriteAtlas atlas;

    public List<OtherObject> OtherObjectList
    {
        private set;
        get;
    }

    public void Init()
    {
        this.OtherObjectList = new List<OtherObject>();
    }

    public void SpawnObject(int objectId, Vector2Int pos)
    {
        var objData = DataManager.instance.GetData<RuckData>(objectId);

        GameObject objGo = Instantiate(Resources.Load<GameObject>(objData.prefab_name),
    new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        objGo.transform.parent = this.transform;

        var otherObj = objGo.GetComponent<OtherObject>();
        this.OtherObjectList.Add(otherObj);
    }

    public void SpawnObject(int objectId)
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

        var objData = DataManager.instance.GetData<RuckData>(objectId);
        //GameObject objGo = Instantiate<GameObject>(objGos[randObj]);
        GameObject objGo = Instantiate(Resources.Load<GameObject>(objData.prefab_name),
            new Vector3(emptyPosList[randPosIdx].x, emptyPosList[randPosIdx].y, 0), Quaternion.identity);
        objGo.transform.parent = this.transform;

        var otherObj = objGo.GetComponent<OtherObject>();
        otherObj.Init(atlas.GetSprite(objData.sprite_name));
        this.OtherObjectList.Add(otherObj);
    }

    public void DestroyObject(OtherObject obj)
    {
        this.OtherObjectList.Remove(obj);
    }
}
