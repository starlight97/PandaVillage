using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMain : SceneMain
{
    private Player player;
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private ObjectManager objectManager;
    private PortalManager portalManager;

    private void Start()
    {
        SceneParams param = new SceneParams();
        param.SpawnPos = new Vector3(39, 32, 0);
        this.Init(param);
    }

    public override void Init(SceneParams param)
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.objectManager = GameObject.FindObjectOfType<ObjectManager>();
        this.portalManager = GameObject.FindObjectOfType<PortalManager>();

        this.player.transform.position = param.SpawnPos;
        this.timeManager.Init();
        this.tileManager.Init();
        //this.objectManager.Init(App.eMapType.BusStop, tileManager.GetTilesPosList(TileManager.eTileType.Grass));

        this.portalManager.Init();

        #region PlayerAction
        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };
        // 타일이 있냐?
        this.player.onGetTile = (pos, state) =>
        {
            bool check = tileManager.CheckTile(pos, state);
            if (check)
                player.FarmingAct(pos);
        };

        // 타일 변경
        this.player.onChangeFarmTile = (pos, state) =>
        {
            tileManager.SetTile(pos, state);
        };


        #endregion

        #region TimeManagerAction
        this.timeManager.onUpdateTime = (hour, minute) =>
        {
            if (hour == 1)
            {
                Debug.Log("1시에요 하루가 끝났어요");
                timeManager.EndDay();
                //ranchManager.NextDay();
                //cropManager.CheckWateringDirt();
                tileManager.ClearWateringTiles();
            }
        };
        #endregion

        this.portalManager.onArrival = (sceneType) =>
        {
            //InfoManager.instance.SaveGame(App.eMapType.BusStop, this.objectManager.GetOtherObjectist());
            Dispatch("onArrival" + sceneType.ToString() + "Portal");
        };
    }
}
