using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FarmEditMain : GameSceneMain
{
    public enum eEditType
    {
        Build=1,            // 건설
        Move,               // 이동
        Demolition,         // 철거
        CoopPurchase,      // 닭장 동물 구매
        BarnPurchase,      // 외양간 동물 구매
    }
    private UIFarmEdit uiFarmEdit;
    private ObjectDetector objectDetector;
    private MapManager mapManager;
    private ObjectPlaceManager objectPlaceManager;
    private eEditType editType;
    private GameObject selectedBuildingGo = null;

    private void Start()
    {
        //var param = new FarmEditParam();
        //param.objectId = 9004;

        //DataManager.instance.Init();
        //DataManager.instance.LoadAllData(this);

        //DataManager.instance.onDataLoadFinished.AddListener(() =>
        //{
        //    this.Init(param);
        //});


    }
    public override void Init(SceneParams param = null)
    {
        //base.Init(param);
        var mainParam = (FarmEditParam)param;
        this.editType = (eEditType)mainParam.editType;
        TimeManager.instance.StopAllCoroutines();
        this.objectDetector = GameObject.FindObjectOfType<ObjectDetector>();
        this.objectManager = GameObject.FindObjectOfType<ObjectManager>();
        this.objectPlaceManager = GameObject.FindObjectOfType<ObjectPlaceManager>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.uiFarmEdit = GameObject.FindObjectOfType<UIFarmEdit>();
        this.tileManager = GameObject.FindObjectOfType<TileManager>();        
        this.tileManager.Init();
        this.objectManager.Init(App.eMapType.Farm, tileManager.GetTilesPosList(TileManager.eTileType.Dirt));
        this.uiFarmEdit.Init();

        var info = InfoManager.instance.GetInfo();
        if (editType == eEditType.Move || editType == eEditType.Demolition)
        {
            Debug.Log("디텍터");
            this.objectDetector.Init();
        }
        else
        {            
            objectPlaceManager.Init(mainParam.objectId);
            this.uiFarmEdit.ShowBtnOk();
        }
        
        

        this.objectDetector.onClickBuilding = (buildingGo) =>
        {
            if(this.editType == eEditType.Move)
            {
                objectPlaceManager.Init(buildingGo);
                this.objectDetector.StopDetecting();
            }
            else if(this.editType == eEditType.Demolition)
            {
                if (this.selectedBuildingGo != null)
                {
                    var oldSpriteRenderer = this.selectedBuildingGo.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    oldSpriteRenderer.color = Color.white;
                }
            }
            else if (this.editType == eEditType.CoopPurchase)
            {
                int objid = buildingGo.GetComponent<OtherObject>().id;
                if (objid != 9004 && objid != 9005 && objid != 9006)
                {
                    Debug.Log("COOP 아님");
                    return;
                }
            }
            else if (this.editType == eEditType.BarnPurchase)
            {
                int objid = buildingGo.GetComponent<OtherObject>().id;
                if (objid != 9007 && objid != 9008 && objid != 9009)
                {
                    Debug.Log("COOP 아님");
                    return;
                }
            }
            this.uiFarmEdit.ShowBtnOk();
            selectedBuildingGo = buildingGo;

            var spriteRenderer = this.selectedBuildingGo.transform.GetChild(0).GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.green;

        };

        this.objectPlaceManager.onMoveComplete = (oldPos, newPos) =>
        {
            this.objectDetector.Detecting();
            this.uiFarmEdit.HideBtnOk();

            var oldSpriteRenderer = this.selectedBuildingGo.transform.GetChild(0).GetComponent<SpriteRenderer>();
            oldSpriteRenderer.color = Color.white;

            selectedBuildingGo = null;

            var obj = info.objectInfoList.Find(x => x.sceneName == "Farm" && x.posX == oldPos.x && x.posY == oldPos.y);
            obj.posX = (int)newPos.x;
            obj.posY = (int)newPos.y;
        };

        this.objectPlaceManager.onBuildComplete = (pos) =>
        {
            var data = DataManager.instance.GetData<BuildingData>(mainParam.objectId);
            info.playerInfo.gold -= data.require_gold;
            ObjectInfo objectInfo = new ObjectInfo();
            objectInfo.objectId = mainParam.objectId;
            objectInfo.objectType = 0;
            objectInfo.sceneName = "Farm";
            objectInfo.posX = (int)pos.x;
            objectInfo.posY = (int)pos.y;
            info.objectInfoList.Add(objectInfo);

            if(objectInfo.objectType == 0)
            {
                // 사일로가 아닐때 (외양간, 닭장)
                if(objectInfo.objectId != 9003)
                {
                    var coopInfo = new CoopInfo(objectInfo.objectId, (int)pos.x, (int)pos.y);
                    info.ranchInfo.coopInfoList.Add(coopInfo);
                }
            }
            Dispatch("onEditComplete");
            
        };
        this.objectPlaceManager.onFindWallPosList = () =>
        {
            this.objectPlaceManager.wallPosArr = mapManager.GetWallPosArr();
            this.objectPlaceManager.BuildingEdit();
        };
        this.uiFarmEdit.onCLickBtnOkay = () =>
        {
            if (this.editType == eEditType.Build)
            {
                this.objectPlaceManager.BuildingBuildExecute();
            }
            else if (this.editType == eEditType.Move)
            {
                this.objectPlaceManager.BuildingMove();
            }
            else if (this.editType == eEditType.Demolition)
            {
                var pos = selectedBuildingGo.transform.position;
                var objInfo = info.objectInfoList.Find(x => x.sceneName == "Farm" && x.posX == pos.x && x.posY == pos.y);
                info.objectInfoList.Remove(objInfo);
                Dispatch("onEditComplete");
            }
            else if (this.editType == eEditType.CoopPurchase)
            {
                this.PurchaseAnimal(mainParam.objectId);

            }
            else if (this.editType == eEditType.BarnPurchase)
            {
                //info.ranchInfo.coopInfoList.Add();
            }

        };

        this.uiFarmEdit.onCLickBtnCancel = () =>
        {
            // 현재 건물 선택중이 아니라면
            if (selectedBuildingGo == null)
            {
                Dispatch("onEditComplete");
            }
            // 현재 건물 선택중이 라면
            else
            {
                if (this.editType == eEditType.Build)
                {
                    this.objectPlaceManager.BuildingBuildExecute();
                }
                else if (this.editType == eEditType.Move)
                {
                    // 현재 선택한 건물 취소
                    var oldSpriteRenderer = this.selectedBuildingGo.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    oldSpriteRenderer.color = Color.white;

                    selectedBuildingGo = null;
                    this.objectPlaceManager.BuildingEditCancel();
                    objectDetector.Detecting();


                }
                else if (this.editType == eEditType.Demolition)
                {
                    // 현재 선택한 건물 취소
                    selectedBuildingGo = null;
                }
            }

        };

    }

    private void PurchaseAnimal(int animalId)
    {
        var info = InfoManager.instance.GetInfo();
        var pos = selectedBuildingGo.transform.position;

        var coopinfo = info.ranchInfo.coopInfoList.Find(x => x.posX == pos.x && x.posY == pos.y);
        AnimalInfo animalInfo = new AnimalInfo("dd", 11);
        coopinfo.animalinfoList.Add(animalInfo);
        //info.ranchInfo.coopInfoList.Add();
    }


}
