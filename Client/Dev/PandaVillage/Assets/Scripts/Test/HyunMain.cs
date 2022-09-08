using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class HyunMain : MonoBehaviour
{
    private MapManager mapManager;
    private TileManager tileManager;
    private Player player;

    public Button hoeBtn;
    public Button seedBtn;
    public Button wateringcanBtn;

    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();

        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };

        this.hoeBtn.onClick.AddListener(() => {
            Debug.Log("click hoe button");
            this.player.SeleteItem(0);
        });

        this.seedBtn.onClick.AddListener(() => {
            Debug.Log("click seed button");
            this.player.SeleteItem(1);
        });

        this.wateringcanBtn.onClick.AddListener(() => {
            Debug.Log("click wateringcan button");
            this.player.SeleteItem(2);
        });

        // 흙타일이냐?
        this.player.onRequestDirtTile = (pos) =>
        {
            if (tileManager.GetDirtTile(pos))
            {
                this.player.RequestDirtTile();
            }
        };

        // 밭타일로 바꾸기
        this.player.onChangeDirtTile = (pos) =>
        {
            this.tileManager.DirtMapSetTile(pos);
        };

        // 밭타일이냐?
        this.player.onRequestHoeDirtTile = (pos) =>
        {
            if (tileManager.GetHoeDritTile(pos))
            {
                this.player.RequestHoeDirtTile();
            }
        };

        // 씨앗으로 바꾸기
        this.player.onChangeHoeDirtTile = (pos) =>
        {
            this.tileManager.HoeDirtMapSetTile(pos);
        };

        // 씨앗타일이냐?
        this.player.onRequestObjectTile = (pos) =>
        {
            if (tileManager.GetObjectTile(pos))
            {
                this.player.RequestObjectTile();
            }
        };

        // 물타일로 바꾸기
        this.player.onChangeObjectTile = (pos) =>
        {
            this.tileManager.ObjectMapSetTile(pos);
        };
    }
}
