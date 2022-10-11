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


    private void Start()
    {
        this.Init();
    }

    public override void Init(SceneParams param = null)
    {
        InfoManager.instance.Init(10);
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);

        this.uiVillage = GameObject.FindObjectOfType<UIFarm>();
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.ranchManager = GameObject.FindObjectOfType<RanchManager>();
        this.cropManager = GameObject.FindObjectOfType<CropManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();
        this.objectManager = GameObject.FindObjectOfType<ObjectManager>();
        this.portalManager = GameObject.FindObjectOfType<PortalManager>();

        this.timeManager.Init();
        this.tileManager.Init();
        this.ranchManager.Init();
        this.cropManager.Init();
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

        // 씨앗 뿌리기
        this.player.onPlantCrop = (pos) =>
        {
            cropManager.CreateCrop(1001, pos);
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
            if (hour == 6 && minute == 10)
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
        this.cropManager.onGetFarmTile = (id, pos, crop) =>
        {
            bool check = tileManager.CheckTile(pos, TileManager.eTileType.WateringDirt);
            if (check == true)
            {
                cropManager.GrowUpCrop(id, crop);
            }
        };
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
            Dispatch("onArrival" + sceneType.ToString() + "Portal");
        };
        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            // check 불러온 인포가 있다면 true
            // 없다면 false반환 하므로 뉴비처리
            //bool check = InfoManager.instance.LoadData();
            //this.objectManager.Init("HyunCopyFarm", tileManager.GetTilesPosList(TileManager.eTileType.Dirt));
            //this.objectManager.SpawnGatheringObjects(0, Random.Range(0,4));
            //if (check == false)
            //{
            //    this.objectManager.SpawnGatheringObjects(0, 4);
            //}
            //SaveGame();
        });
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        SaveGame(1);

    //    }
    //}

  
    private void SaveGame()
    {
        List<OtherObject> otherObjList = this.objectManager.GetOtherObjectist();

        var info = InfoManager.instance.GetInfo();
        // 10분당 1로 저장
        // ex 하루 = 1320 분
        // 하루마다 132 씩 ++
        info.playerInfo.playMinute += 132;
        //if(info.playerInfo.dicInventoryInfo.ContainsKey(1000))
        //{
        //    //info.playerInfo.dicInventoryInfo.Add(1000, new InventoryInfo(10, 1000, ));
        //}
        foreach (var obj in otherObjList)
        {
            ObjectInfo objectInfo = new ObjectInfo();
            objectInfo.objectId = obj.id;
            objectInfo.posX = (int)obj.gameObject.transform.position.x;
            objectInfo.posY = (int)obj.gameObject.transform.position.y;
            objectInfo.sceneName = "HyunCopyFarm";

            info.objectInfoList.Add(objectInfo);
        }

        InfoManager.instance.SaveInfo();
    }
}
