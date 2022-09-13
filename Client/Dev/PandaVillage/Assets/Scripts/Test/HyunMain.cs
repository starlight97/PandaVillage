using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class HyunMain : MonoBehaviour
{
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private Player player;

    public Button hoeBtn;
    public Button seedBtn;
    public Button wateringcanBtn;

    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();

        this.timeManager.Init();

        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };
        #region test
        //this.hoeBtn.onClick.AddListener(() =>
        //{
        //    Debug.Log("click hoe button");
        //    this.player.SelectItem(Player.eItemType.Hoe);
        //});

        //this.seedBtn.onClick.AddListener(() =>
        //{
        //    Debug.Log("click seed button");
        //    this.player.SelectItem(Player.eItemType.Seed);
        //});

        //this.wateringcanBtn.onClick.AddListener(() =>
        //{
        //    Debug.Log("click wateringcan button");
        //    this.player.SelectItem(Player.eItemType.WateringCan);
        //});
        #endregion

        // 타일이 있냐?
        this.player.onGetFarmTile = (pos, state) =>
        {
            bool check = tileManager.GetTile(pos, state);
            if (check)
                player.ChangeFarmTile(pos);
        };

        // 타일 변경
        this.player.onChangeFarmTile = (pos, state) =>
        {
            tileManager.SetTile(pos, state);
        };
    }
}
