using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class JeongSikTestMain : MonoBehaviour
{
    private Player player;
    private MapManager mapManager;
    private TileManager tileManager;
    private TimeManager timeManager;
    private ObjectPlaceManager objectPlaceManager;
    private ObjectManager objectManager;

    private void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();
        this.timeManager = GameObject.FindObjectOfType<TimeManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();
        this.objectManager = GameObject.FindObjectOfType<ObjectManager>();

        this.tileManager.Init();
        this.timeManager.Init();
        this.objectManager.Init("JeongTest", tileManager.GetTilesPosList(Farming.eFarmTileType.Grass));


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
            //cropManager.CreateCrop(pos);
        };

        this.player.onShowAnimalUI = (animal) =>
        {
            //if (animal != null)
            //    uiFarm.ShowAnimalUI(animal.animalName, animal.friendship, animal.age);
            //else
            //    uiFarm.HideAnimalUI();
        };
        this.player.onSelectedBuilding = (selectedBuildingGo) =>
        {
            objectPlaceManager.BuildingEdit(selectedBuildingGo);
        };
        #endregion

        this.timeManager.onUpdateTime = (hour, minute) =>
        {
        };
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

            gameInfo.playerInfo.inventory.dicItem.Add(1000, 10);
            gameInfo.playerInfo.inventory.dicItem.Add(2000, 20);
            gameInfo.playerInfo.inventory.dicItem.Add(3000, 30);
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
        List<Vector3Int> objectPosList = this.objectManager.GetObjectInfoList();

        var info = InfoManager.instance.GetInfo(playerId);
        // 10분당 1로 저장
        // ex 하루 = 1320 분
        // 하루마다 132 씩 ++
        info.playerInfo.playMinute += 132;
        //if(info.playerInfo.dicInventoryInfo.ContainsKey(1000))
        //{
        //    //info.playerInfo.dicInventoryInfo.Add(1000, new InventoryInfo(10, 1000, ));
        //}
        foreach (var pos in objectPosList)
        {
            ObjectInfo objectInfo = new ObjectInfo();
            objectInfo.objectId = 1;
            objectInfo.posX = pos.x;
            objectInfo.posY = pos.y;
            objectInfo.sceneName = "FarmScene";

            info.objectInfoList.Add(objectInfo);
        }

        InfoManager.instance.SaveInfo();
    }
}
