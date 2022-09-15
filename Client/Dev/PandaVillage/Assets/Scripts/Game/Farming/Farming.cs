using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Farming : MonoBehaviour
{
    // 타일 타입
    public enum eFarmTileType
    {
        None = -1,
        Dirt, HoeDirt, WateringDirt
    }
    public enum eFarmActType
    {
        None = -1,
        Plant, SetTile
    }

    // 플레이어가 어떤 도구를 들고 있느냐에 따라 어떤 타일을 조사해야하나?
    public eFarmTileType GetFarmTile(Player.eItemType state)
    {
        eFarmTileType tileType = eFarmTileType.None;
        switch (state)
        {
            case Player.eItemType.Hoe:
                tileType = eFarmTileType.Dirt;
                break;
            case Player.eItemType.WateringCan:
            case Player.eItemType.Seed:
                tileType = eFarmTileType.HoeDirt;
                break;
            default:
                break;
        }
        return tileType;
    }

    public eFarmActType FarmingToolAct(Vector3Int pos, Player.eItemType itemType)
    {
        eFarmActType actType = eFarmActType.None;
        switch (itemType)
        {
            case Player.eItemType.None:
                break;
            case Player.eItemType.Hoe:
            case Player.eItemType.WateringCan:
                actType = eFarmActType.SetTile;
                break;
            case Player.eItemType.Fertilizer:
                break;
            case Player.eItemType.Axe:
                break;
            case Player.eItemType.Seed:
                actType = eFarmActType.Plant;
                break;
            default:
                break;
        }
        return actType;
    }
}
