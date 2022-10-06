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


    public Button btnNext;
    public Button btnAddAnimal;

    public override void Init(SceneParams param)
    {
        LoadInfo(1);
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
        this.objectManager.Init("Farm", tileManager.GetTilesPosList(Farming.eFarmTileType.Grass));
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
            if(hour == 1)
            {
                Debug.Log("22시에요 하루가 끝났어요");
                timeManager.EndDay();
                ranchManager.NextDay();
                SaveGame(1);
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

        this.portalManager.onArrival = (sceneType) =>
        {
            Dispatch("onArrival" + sceneType.ToString() + "Portal");            
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveGame(1);
            
        }
    }

    private void LoadInfo(int playerId)
    {
        bool check = InfoManager.instance.LoadData();
        GameInfo gameInfo = new GameInfo();
        if (check == false)
        {
            gameInfo.playerId = playerId;
            gameInfo.objectInfoList = new List<ObjectInfo>();
            gameInfo.playerInfo = new PlayerInfo("길동이", "강아지");

            gameInfo.playerInfo.inventory.dicItem.Add(1000,10);
            gameInfo.playerInfo.inventory.dicItem.Add(2000,20);
            gameInfo.playerInfo.inventory.dicItem.Add(3000,30);
            InfoManager.instance.InsertInfo(gameInfo);

            InfoManager.instance.SaveInfo();
            Debug.Log("신규 유저 입니다.");
        }

        else
        {
            Debug.Log("기존 유저 입니다.");
            gameInfo = InfoManager.instance.GetInfo(playerId);

            var datas = gameInfo.objectInfoList;
            foreach (var data in datas)
            {
                Debug.Log(data.objectId);
                Debug.Log(data.sceneName);
                Debug.Log(data.posX);
                Debug.Log(data.posY);
            }
        }
    }

    private void SaveGame(int playerId)
    {        
        //List<Vector3Int> objectPosList = this.objectManager.GetObjectInfoList();

        //var info = InfoManager.instance.GetInfo(playerId);
        //// 10분당 1로 저장
        //// ex 하루 = 1320 분
        //// 하루마다 132 씩 ++
        //info.playerInfo.playMinute += 132;
        ////if(info.playerInfo.dicInventoryInfo.ContainsKey(1000))
        ////{
        ////    //info.playerInfo.dicInventoryInfo.Add(1000, new InventoryInfo(10, 1000, ));
        ////}
        //foreach (var pos in objectPosList)
        //{
        //    ObjectInfo objectInfo = new ObjectInfo();
        //    objectInfo.objectId = 1;
        //    objectInfo.posX = pos.x;
        //    objectInfo.posY = pos.y;
        //    objectInfo.sceneName = "FarmScene";

        //    info.objectInfoList.Add(objectInfo);
        //}

        InfoManager.instance.SaveInfo();
    }


}



