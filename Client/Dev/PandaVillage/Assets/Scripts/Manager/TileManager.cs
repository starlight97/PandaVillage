using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap groundMap;           // 땅
    public Tilemap WallMap;             // 벽 
    public Tilemap hoeDirtMap;          // 밭
    public Tilemap wateringDirtMap;     // 물뿌리개밭

    public TileBase[] tileBases;        // 0: hoeDirt, 1: wateringDirt

    public bool GetTile(Vector3Int pos, Farming.eFarmTileType state)
    {
        bool check = false;
        TileBase tilebase = groundMap.GetTile(pos);

        switch (state)
        {
            case Farming.eFarmTileType.None:
                break;
            case Farming.eFarmTileType.Dirt:
                if (groundMap.GetTile(pos) != null && tilebase.name == "Dirt")
                    check = true;
                break;
            case Farming.eFarmTileType.HoeDirt:
                if (hoeDirtMap.GetTile(pos) != null)
                    check = true;
                break;
            case Farming.eFarmTileType.WateringDirt:
                if (wateringDirtMap.GetTile(pos) != null)
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
            default:
                break;
        }
    }
    // 일정 시간(하루)이 지나면 물 타일을 지워준다: 플레이어가 매일 물을 줘야하기 때문이다.
    public void ClearWateringTiles()
    {
        wateringDirtMap.ClearAllTiles();
    }


    // 타일맵에 존재하는 모든 타일 가져오기
    public void GetAllHoeDirtTilesPos()
    {
        // BoundsInt
        // 타일맵의 경계를 셀 크기로 반환
        BoundsInt bounds = hoeDirtMap.cellBounds;   
        TileBase[] allTiles = hoeDirtMap.GetTilesBlock(bounds);

        for (int y = 0; y < bounds.size.y; y++)
        {
            for (int x = 0; x < bounds.size.x; x++)
            {
                var tile = hoeDirtMap.GetTile(new Vector3Int(x, y, 0));
                //TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
            }
        }
    }



    // 씨앗이 심기지 않은 밭 타일을 랜덤으로 지움
    public void RandomClearHoeDirtTile()
    {

    }

}
