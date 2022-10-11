using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    public SpriteAtlas atlas;

    public List<OtherObject> OtherObjectList
    {
        private set;
        get;
    }

    private List<Vector3Int> spawnTilePosList;

    public void Init(List<Vector3Int> spawnTilePosList)
    {
        this.OtherObjectList = new List<OtherObject>();
        this.spawnTilePosList = spawnTilePosList;
    }

    public void SpawnObject(string prefab_name, string sprite_name, Vector3Int pos)
    {
        if (WallCheck(pos) == true)
            return;

        GameObject objGo = Instantiate(Resources.Load<GameObject>(prefab_name),
    pos, Quaternion.identity);
        objGo.transform.parent = this.transform;

        var otherObj = objGo.GetComponent<OtherObject>();
        otherObj.Init(atlas.GetSprite(sprite_name));
        this.OtherObjectList.Add(otherObj);
    }

    public void SpawnObject(string prefab_name, string sprite_name)
    {
        if (spawnTilePosList.Count == 0)
        {
            Debug.Log("빈공간 없음");
            return;
        }

        var randPosIdx = Random.Range(0, spawnTilePosList.Count);

        Vector3Int spawnPos = spawnTilePosList[randPosIdx];
        if (WallCheck(spawnPos) == true)
        {
            spawnTilePosList.RemoveAt(randPosIdx);
            return;
        }
        GameObject objGo = Instantiate(Resources.Load<GameObject>(prefab_name), spawnPos, Quaternion.identity);
        objGo.transform.parent = this.transform;

        var otherObj = objGo.GetComponent<OtherObject>();
        otherObj.Init(atlas.GetSprite(sprite_name));
        this.OtherObjectList.Add(otherObj);
        spawnTilePosList.RemoveAt(randPosIdx);
    }

    public void DestroyObject(OtherObject obj)
    {
        this.OtherObjectList.Remove(obj);
    }

    private bool WallCheck(Vector3Int pos)
    {
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("WallObject"))
                        + (1 << LayerMask.NameToLayer("Wall"));    // Object 와 WallObject 레이어만 충돌체크함
        var col = Physics2D.OverlapBox(new Vector2(pos.x + 0.5f, pos.y + 0.5f), new Vector2(0.95f, 0.95f), 0, layerMask);
        if (col != null)
        {
            Debug.Log("징애물 있음");
            return true;
        }
        return false;
    }

}
