using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class JeongSikTestMain : MonoBehaviour
{
    private MapManager mapManager;
    private TimeManager timeManager;
    private TileManager tileManager;
    private ObjectSpawner objectSpawner;
    private ObjectPlaceManager objectPlaceManager;
    private Coop coop;

    private Player player;

    public Vector2 pos;
    public Vector3 size;

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
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.coop = GameObject.FindObjectOfType<Coop>();
        this.objectSpawner = GameObject.FindObjectOfType<ObjectSpawner>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();


        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };

        this.player.onGetFarmTile = (pos, state) =>
        {
            bool check = tileManager.GetTile(pos, state);
            //if (check)
                //player.ChangeFarmTile(pos);
        };
        // 타일 변경
        this.player.onChangeFarmTile = (pos, state) =>
        {
            tileManager.SetTile(pos, state);
        };
        this.player.onSelectedBuilding = (selectedBuildingGo) =>
        {
            objectPlaceManager.BuildingEdit(selectedBuildingGo);
        };

        this.objectPlaceManager.onEditComplete = () =>
        {
            player.isBuildingSelected = false;
        };
        this.objectPlaceManager.onFindWallPosList = () =>
        {
            this.objectPlaceManager.wallPosArr = mapManager.GetWallPosArr();
        };


        this.coop.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            animal.Move();
        };

        this.timeManager.onUpdateTime = (hour, minute) =>
        {
            this.objectSpawner.SpawnObject();
        };

        this.timeManager.Init();
        this.coop.Init();
        this.objectSpawner.Init();
        //this.objectSpawner.SpawnObjects();
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            var col = Physics2D.OverlapBox(pos, size, 0);

            if(col != null)
            {
                Debug.Log(col.gameObject);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(pos, size);
    }

}
