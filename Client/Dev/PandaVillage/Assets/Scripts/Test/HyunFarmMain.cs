using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HyunFarmMain : SceneMain
{
    private UIFarm uiVillage;
    private Player player;
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private CropManager cropManager;
    private RanchManager ranchManager;
    private ObjectPlaceManager objectPlaceManager;
    private ObjectSpawner objectSpawner;
    private ObjectManager objectManager;
    private PortalManager portalManager;


    public Button btnNext;
    public Button btnAddAnimal;

    void Start()
    {
        //LoadInfo(1);
        Init();
    }

    public override void Init(SceneParams param = null)
    {
        this.uiVillage = GameObject.FindObjectOfType<UIFarm>();
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.ranchManager = GameObject.FindObjectOfType<RanchManager>();
        this.cropManager = GameObject.FindObjectOfType<CropManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();
        this.objectSpawner = GameObject.FindObjectOfType<ObjectSpawner>();
        this.objectManager = GameObject.FindObjectOfType<ObjectManager>();
        this.portalManager = GameObject.FindObjectOfType<PortalManager>();

        this.timeManager.Init();
        this.tileManager.Init();
        this.ranchManager.Init();
        this.cropManager.Init();
        this.uiVillage.Init();
        //this.objectManager.Init();
        this.portalManager.Init();


        #region PlayerAction
        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };
        // 타일이 있냐?
        this.player.onGetFarmTile = (pos, state) =>
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
        this.player.onPlantCrop = (pos) =>
        {
            cropManager.CreateCrop(pos);
        };

        this.player.onShowAnimalUI = (animal) =>
        {
            if (animal != null)
                uiVillage.ShowAnimalUI(animal.animalName, animal.friendship, animal.age);
            else
                uiVillage.HideAnimalUI();
        };
        this.player.onSelectedBuilding = (selectedBuildingGo) =>
        {
            objectPlaceManager.BuildingEdit(selectedBuildingGo);
        };
        #endregion

        #region TimeManagerAction
        this.timeManager.onUpdateTime = (hour, minute) =>
        {
            if (hour == 1)
            {
                Debug.Log("1시에요 하루가 끝났어요");
                timeManager.EndDay();
                ranchManager.NextDay();
                cropManager.CheckWateringDirt();
                tileManager.ClearWateringTiles();
            }
        };
        #endregion

        #region CropManagerAction
        this.cropManager.onGetFarmTile = (pos, crop) =>
        {
            bool check = tileManager.CheckTile(pos, Farming.eFarmTileType.WateringDirt);
            if (check == true)
            {
                cropManager.GrowUpCrop(crop);
            }
        };
        #endregion

        this.ranchManager.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            animal.Move();
        };

        this.btnNext.onClick.AddListener(() =>
        {
            ranchManager.NextDay();
        });
        this.btnAddAnimal.onClick.AddListener(() =>
        {
            ranchManager.coopArr[0].CreateAnimal();
        });

        this.objectPlaceManager.onEditComplete = () =>
        {
            player.isBuildingSelected = false;
        };
        this.objectPlaceManager.onFindWallPosList = () =>
        {
            this.objectPlaceManager.wallPosArr = mapManager.GetWallPosArr();
        };

        //this.objectSpawner.onGetTilePosList = (tileType) =>
        //{
        //    this.objectSpawner.GrassTileSetting(this.tileManager.GetTilesPosList(tileType));
        //    this.objectSpawner.SpawnObjects();
        //};

        this.portalManager.onArrival = (sceneType) =>
        {
            Dispatch("onArrival" + sceneType.ToString() + "Portal");
        };

        this.objectSpawner.Init();


    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        SaveGame(1);

    //    }
    //}

    //private void LoadInfo(int playerId)
    //{
    //    bool check = InfoManager.instance.LoadData();
    //    GameInfo gameInfo = new GameInfo();
    //    if (check == false)
    //    {
    //        gameInfo.playerId = playerId;
    //        InfoManager.instance.InsertInfo(gameInfo);
    //    }

    //    else
    //    {
    //        gameInfo = InfoManager.instance.GetInfo(playerId);

    //        //var datas = gameInfo.objectInfos;
    //        foreach (var data in datas)
    //        {
    //            Debug.Log(data.objectId);
    //            Debug.Log(data.sceneName);
    //            Debug.Log(data.posX);
    //            Debug.Log(data.posY);
    //        }
    //    }
    //}

    //private void SaveGame(int playerId)
    //{
    //    List<Vector3Int> objectPosList = this.objectManager.GetObjectInfoList();

    //    var info = InfoManager.instance.GetInfo(playerId);
    //    foreach (var pos in objectPosList)
    //    {
    //        ObjectInfo objectInfo = new ObjectInfo();
    //        objectInfo.objectId = 1;
    //        objectInfo.posX = pos.x;
    //        objectInfo.posY = pos.y;
    //        objectInfo.sceneName = "FarmScene";

    //        info.objectInfos.Add(objectInfo);
    //    }
    //    InfoManager.instance.SaveInfo();
    //}
}
