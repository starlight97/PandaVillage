using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Farming : MonoBehaviour
{
    // 농사 타일 타입
    public enum eFarmTileType
    {
        None = -1,
        Dirt, HoeDirt, WateringDirt
    }
    // 플레이어 농사 행위 타입
    // Plant: 씨앗 심기, SetTile: 행위에 따른 타일 변경
    public enum eFarmActType
    {
        None = -1,
        Plant, SetTile
    }

    // 플레이어가 들고 있는 도구에 따른 타일 타입 반환
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

    // 플레이어가 들고 있는 도구에 따른 행위 반환
    public eFarmActType FarmingToolAct(Player.eItemType itemType)
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
