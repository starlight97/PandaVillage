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

    }


}
