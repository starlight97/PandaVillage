using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageMain : SceneMain
{
    private MapManager mapManager;
    private Player player;

    void Start()
    {

        Init();

    }

    public override void Init(SceneParams param = null)
    {
        //StartCoroutine(this.TouchToStartRoutine());


        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();

        this.player.onDecideTargetTile = (startPos, targetPos) =>
        {
            this.mapManager.PathFinding(startPos, targetPos);
            this.player.Move(this.mapManager.PathList);
        };

        mapManager.Init();

        this.player.onRequestDirtTile = (pos) =>
        {
            if (mapManager.GetDirtTile(pos))
            {
                this.player.RequestDirtTile();
            }
        };

        this.player.onChangeDirtTile = (pos) => {
            this.mapManager.DirtMapSetTile(pos);
        };

        //this.player.onChangeHoeDirtTile = (pos) => { 
        //    this.mapManager
        //};
    }


}
