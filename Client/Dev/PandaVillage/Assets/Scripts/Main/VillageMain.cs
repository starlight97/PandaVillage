using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageMain : SceneMain
{
    private UIVillage uiVillage;
    private Player player;
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private CropManager cropManager;
    private RanchManager ranchManager;
    private ObjectPlaceManager objectPlaceManager;

    public GameObject AnimalUI;
    public Button btnNext;
    public Button btnAddAnimal;

    void Start()
    {

        Init();

    }

    public override void Init(SceneParams param = null)
    {
        this.uiVillage = GameObject.FindObjectOfType<UIVillage>();
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.ranchManager = GameObject.FindObjectOfType<RanchManager>();
        this.cropManager = GameObject.FindObjectOfType<CropManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();

        this.timeManager.Init();
        this.ranchManager.Init();
        this.cropManager.Init();
        this.uiVillage.Init();

        #region PlayerAction
        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };
        // 타일이 있냐?
        this.player.onGetFarmTile = (pos, state) =>
        {
            bool check = tileManager.GetTile(pos, state);
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
            if(hour == 1)
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
            bool check = tileManager.GetTile(pos, Farming.eFarmTileType.WateringDirt);
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

    }



}
