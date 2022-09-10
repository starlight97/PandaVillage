using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Farming : MonoBehaviour
{   
    public enum eFarmType
    {
        None = -1,
        Dirt, HoeDirt, Watering, Object
    }

    // 플레이어가 어떤 도구를 들고 있느냐에 따라 어떤 타일을 조사해야하나?
    public eFarmType GetFarmTile(Player.eItemType state)
    {
        eFarmType type = eFarmType.None;
        switch (state)
        {
            case Player.eItemType.Hoe:
                type = eFarmType.Dirt;
                break;
            case Player.eItemType.WateringCan:
            case Player.eItemType.Seed:
                type = eFarmType.HoeDirt;
                break;
            default:
                break;
        }
        return type;
    }


}
