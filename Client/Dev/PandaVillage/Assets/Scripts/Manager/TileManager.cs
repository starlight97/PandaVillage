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

    public TileBase[] tileBases;

    public bool GetTile(Vector3Int pos, Farming.eFarmType state)
    {
        bool check = false;
        
        switch (state)
        {
            case Farming.eFarmType.None:
                break;
            case Farming.eFarmType.Dirt:
                if (dirtMap.GetTile(pos) != null)
                    check = true;
                break;
            case Farming.eFarmType.HoeDirt:
                if (hoeDirtMap.GetTile(pos) != null)
                    check = true;
                break;
            case Farming.eFarmType.Watering:
                if (wateringDirtMap.GetTile(pos) != null)
                    check = true;
                break;
            case Farming.eFarmType.Object:
                if (objectMap.GetTile(pos) != null)
                    check = true;
                break;
            default:
                break;
        }
        return check;
    }

    public void SetTile(Vector3Int pos, Player.eItemType state)
    {
        switch (state)
        {
            case Player.eItemType.None:
                break;
            case Player.eItemType.Hoe:
                hoeDirtMap.SetTile(pos, tileBases[0]);
                break;
            case Player.eItemType.WateringCan:
                wateringDirtMap.SetTile(pos, tileBases[1]);
                break;
            case Player.eItemType.Seed:
                objectMap.SetTile(pos, tileBases[2]);
                break;
            default:
                break;
        }
    }
}
