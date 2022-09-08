using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap dirtMap;             // 흙
    public Tilemap hoeDirtMap;          // 밭
    public Tilemap objectMap;           // 씨앗
    public Tilemap wateringDirtMap;     // 물

    //public Tilemap[] tilemaps;
    public TileBase[] tileBases;

    // 호미질
    public void DirtMapSetTile(Vector3Int pos)
    {
        if (this.hoeDirtMap.GetTile(pos) != null)
            Debug.Log("hoeDirt: " + this.hoeDirtMap.GetTile(pos).name);

        if (this.dirtMap.GetTile(pos) != null)
            this.hoeDirtMap.SetTile(pos, tileBases[0]);
    }

    public bool GetDirtTile(Vector3Int pos)
    {
        if (dirtMap.GetTile(pos) != null)
            return true;
        return false;
    }

    //private int index = 0;

    // 씨앗 뿌리기
    public void HoeDirtMapSetTile(Vector3Int pos)
    {
        if (this.objectMap.GetTile(pos) != null)
            Debug.Log("object: " + this.objectMap.GetTile(pos).name);

        if (this.hoeDirtMap.GetTile(pos) == null || this.objectMap.GetTile(pos) != null)
            return;
        else if (this.hoeDirtMap.GetTile(pos) != null)
        {
            this.objectMap.SetTile(pos, tileBases[1]);
        }
    }

    public bool GetHoeDritTile(Vector3Int pos)
    {
        if (this.hoeDirtMap.GetTile(pos) != null && this.objectMap.GetTile(pos) == null)
            return true;
        return false;
    }

    // 물 뿌리기
    public void ObjectMapSetTile(Vector3Int pos)
    {
        if (this.wateringDirtMap.GetTile(pos) != null)
            Debug.Log("wateringDirt: " + this.wateringDirtMap.GetTile(pos).name);

        if(this.hoeDirtMap.GetTile(pos) != null)
        {
            this.wateringDirtMap.SetTile(pos, tileBases[2]);
        }
    }

    public bool GetObjectTile(Vector3Int pos)
    {
        if (hoeDirtMap.GetTile(pos) != null)
            return true;
        return false;
    }
}
