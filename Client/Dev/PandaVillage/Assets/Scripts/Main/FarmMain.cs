using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FarmMain : SceneMain
{
    private UIFarm uiFarm;
    private Player player;
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private CropManager cropManager;
    private RanchManager ranchManager;
    private ObjectPlaceManager objectPlaceManager;
    private ObjectManager objectManager;
    private PortalManager portalManager;

    public override void Init(SceneParams param)
    {
        this.uiFarm = GameObject.FindObjectOfType<UIFarm>();
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.ranchManager = GameObject.FindObjectOfType<RanchManager>();
        this.cropManager = GameObject.FindObjectOfType<CropManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();
        this.objectManager = GameObject.FindObjectOfType<ObjectManager>();
        this.portalManager = GameObject.FindObjectOfType<PortalManager>();

        this.player.transform.position = param.SpawnPos;
        this.tileManager.Init();
        this.timeManager.Init();
        this.ranchManager.Init();
        this.cropManager.Init();
        this.uiFarm.Init();
        this.portalManager.Init();


        var info = InfoManager.instance.GetInfo();
        this.objectManager.Init(App.eMapType.Farm, tileManager.GetTilesPosList(TileManager.eTileType.Dirt));
        if (info.isNewbie == true)
        {
            Debug.Log("isNEWBIE");
            info.isNewbie = false;
            this.objectManager.SpawnRuckObjects(400);
        }
        InfoManager.instance.SaveGame(App.eMapType.Farm, this.objectManager.GetOtherObjectist());


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

        // 씨앗 뿌리기
        //this.player.onPlantCrop = (pos) =>
        //{
        //    cropManager.CreateCrop(pos);
        //};

        this.player.onShowAnimalUI = (animal) =>
        {
            if(animal != null)
                uiFarm.ShowAnimalUI(animal.animalName, animal.friendship, animal.age);                
            else
                uiFarm.HideAnimalUI();
        };
        this.player.onSelectedBuilding = (selectedBuildingGo) =>
        {
            objectPlaceManager.BuildingEdit(selectedBuildingGo);
        };
        #endregion

        #region TimeManagerAction
        this.timeManager.onUpdateTime = (hour, minute) =>
        {
            var info = InfoManager.instance.GetInfo();
            // 10분당 1로 저장
            // ex 하루 = 1320 분
            // 하루마다 132 씩 ++
            info.playerInfo.playMinute += 132;

            if (hour == 26)
            {
                Debug.Log("새벽 2시예요 하루가 끝났어요");
                timeManager.EndDay();
                ranchManager.NextDay();
                InfoManager.instance.SaveGame(App.eMapType.Farm, this.objectManager.GetOtherObjectist());
                InfoManager.instance.EndDay();
                cropManager.CheckWateringDirt();
                tileManager.ClearWateringTiles();
            }
        };
        #endregion

        #region CropManagerAction
        //this.cropManager.onGetFarmTile = (pos, crop) =>
        //{
        //    bool check = tileManager.CheckTile(pos, TileManager.eTileType.WateringDirt);
        //    if (check == true)
        //    {
        //        cropManager.GrowUpCrop(crop);
        //    }
        //};
        #endregion

        this.ranchManager.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            animal.Move();
        };

        this.objectPlaceManager.onEditComplete = () =>
        {
            player.isBuildingSelected = false;
        };
        this.objectPlaceManager.onFindWallPosList = () =>
        {
            this.objectPlaceManager.wallPosArr = mapManager.GetWallPosArr();
        };

        this.portalManager.onArrival = (sceneType) =>
        {
            InfoManager.instance.SaveGame(App.eMapType.Farm, this.objectManager.GetOtherObjectist());
            Dispatch("onArrival" + sceneType.ToString() + "Portal");            
        };

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            InfoManager.instance.SaveGame(App.eMapType.Farm, this.objectManager.GetOtherObjectist());
            
        }
    }



}



