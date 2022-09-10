using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageMain : SceneMain
{
    private MapManager mapManager;
    private TileManager tileManager;
    public Animal animal;
    private Player player;

    void Start()
    {

        Init();
        animal.Init();

    }

    public override void Init(SceneParams param = null)
    {
        //StartCoroutine(this.TouchToStartRoutine());


        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();

        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };

        this.player.onGetFarmTile = (type, pos) =>
        {
            //this.player
        };


        this.animal.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.animal.Move();
        };
    }




}
