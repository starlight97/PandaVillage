using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FarmMain : GameSceneMain
{
    private UIFarm uiFarm;
    private CropManager cropManager;
    private RanchManager ranchManager;
    private ObjectPlaceManager objectPlaceManager;

    public override void Init(SceneParams param)
    {
        base.Init(param);
        this.uiFarm = GameObject.FindObjectOfType<UIFarm>();
        this.ranchManager = GameObject.FindObjectOfType<RanchManager>();
        this.cropManager = GameObject.FindObjectOfType<CropManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();

        this.ranchManager.Init();
        this.objectManager.Init(App.eMapType.Farm, tileManager.GetTilesPosList(TileManager.eTileType.Dirt));
        //this.uiFarm.Init();

        var info = InfoManager.instance.GetInfo();
        if (info.isNewbie == true)
        {
            info.isNewbie = false;

            this.objectManager.SpawnRuckObjects(2);
        }
        InfoManager.instance.SaveOtherObject(App.eMapType.Farm, this.objectManager.GetOtherObjectist());


        this.player.onShowAnimalUI = (animal) =>
        {
            if(animal != null)
                uiFarm.ShowAnimalUI(animal.animalName, animal.friendship, animal.age);                
            else
                uiFarm.HideAnimalUI();
        };

        // 씨앗 뿌리기
        this.player.onPlantCrop = (id, pos) =>
        {
            cropManager.CreateCrop(id, pos);
        };


        #region TimeManagerAction
        TimeManager.instance.onUpdateTime = (hour, minute) =>
        {
            var info = InfoManager.instance.GetInfo();

            uiBase.TimeUpdate(hour, minute);
            if (hour == 26)
            {
                Debug.Log("새벽 2시예요 하루가 끝났어요");
                ranchManager.NextDay();
                InfoManager.instance.SaveOtherObject(App.eMapType.Farm, this.objectManager.GetOtherObjectist());

                tileManager.ClearWateringTiles();
                TimeManager.instance.EndDay();
                InfoManager.instance.EndDay();
                Dispatch("EndDay");
            }
        };
        #endregion

        #region TileManagerAction
        this.tileManager.onFinishedSetTile = (pos) =>
        {
            InfoManager.instance.SaveHoeDirtTilePos(tileManager.hoeDirtPosList);
            InfoManager.instance.SaveWateringDirtTilePos(tileManager.wateringDirtPosList);
        };
        #endregion

        #region CropManagerAction
        this.cropManager.onGetFarmTile = (id, pos, crop) =>
        {
            bool check = tileManager.CheckTile(pos, TileManager.eTileType.WateringDirt);
            if (check == true)
            {
                crop.GrowUp();
            }
        };

        this.cropManager.onUseSeed = () =>
        {
            this.uiBase.UpdateInventory();
        };

        #endregion

        this.ranchManager.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            animal.Move();
        };

        this.objectPlaceManager.onFindWallPosList = () =>
        {
            this.objectPlaceManager.wallPosArr = mapManager.GetWallPosArr();
        };

        this.portalManager.onArrival = (sceneType, index) =>
        {
            InfoManager.instance.SaveOtherObject(App.eMapType.Farm, this.objectManager.GetOtherObjectist());
            Dispatch("onArrival" + sceneType.ToString() + "Portal"+ index);
            //foreach (var item in this.cropManager.cropList)
            //{
            //    Debug.LogFormat("{0} {1} {2}", item.wateringCount, item.state, item.name);
            //}

            this.cropManager.GrowUpCrop();
            InfoManager.instance.SaveCrop(this.cropManager.cropList);

        };

        this.cropManager.Init();
    }




}



