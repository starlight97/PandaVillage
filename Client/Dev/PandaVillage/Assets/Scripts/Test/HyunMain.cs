using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class HyunMain : MonoBehaviour
{
    private Player player;
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private CropManager cropManager;

    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.cropManager = GameObject.FindObjectOfType<CropManager>();

        this.timeManager.Init();
        this.cropManager.Init();

        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };

        // 타일이 있냐?
        this.player.onGetFarmTile = (pos, state) =>
        {
            bool check = tileManager.GetTile(pos, state);
            if (check)
                player.FarmingAct(pos); 
        };

        // 타일 변경
        this.player.onChangeFarmTile = (pos, state) =>
        {
            tileManager.SetTile(pos, state);
        };

        // 씨앗 뿌리기
        this.player.onPlantCrop = (pos) => 
        {
            cropManager.CreateCrop(pos);   
        };

        this.timeManager.onUpdateTime = () =>
        {
            cropManager.CheckWateringDirt();
            tileManager.ClearWateringTiles();
        };

        this.cropManager.onGetFarmTile = (pos, crop) =>
        {
            bool check = tileManager.GetTile(pos, Farming.eFarmTileType.WateringDirt);
            if (check == true)
            {
                cropManager.CropGrowUp(crop);
            }
        };
    }
}
