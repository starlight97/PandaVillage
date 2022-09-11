using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class JeongSikTestMain : MonoBehaviour
{
    private MapManager mapManager;
    private TimeManager timeManager;
    private ObjectSpawner objectSpawner;
    private Coop coop;

    private Player player;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        //StartCoroutine(this.TouchToStartRoutine());


        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.coop = GameObject.FindObjectOfType<Coop>();
        this.objectSpawner = GameObject.FindObjectOfType<ObjectSpawner>();


        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };

        this.player.onGetFarmTile = (type, pos) =>
        {
            //this.player
        };


        this.coop.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            animal.Move();
        };

        this.timeManager.Init();
        this.coop.Init();
        this.objectSpawner.Init();
    }

}
