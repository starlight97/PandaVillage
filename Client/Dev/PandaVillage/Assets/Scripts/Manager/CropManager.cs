using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class CropManager : MonoBehaviour
{
    public GameObject cropObject;

    public List<Crop> cropList;
    public UnityAction<int, Vector3Int, Crop> onGetFarmTile;

    public void Init()
    {
        this.cropList = new List<Crop>();
        //var info = InfoManager.instance.GetInfo();
    }

    // 해당 좌표에 레이를 쏴서 감지되는 오브젝트가 있으면 true 반환
    // 없으면 false 반환
    private bool FindCrop(Vector3Int pos)
    {
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("WallObject"))
             + (1 << LayerMask.NameToLayer("Crop"));    // Object 와 WallObject, Crop 레이어만 충돌체크함
        var col = Physics2D.OverlapCircle(new Vector2(pos.x + 0.5f, pos.y + 0.5f), 0.4f, layerMask);
        if (col != null)
        {
            return true;
        }
        return false;
    }
    
    public void CreateCrop(Vector3Int pos)
    {

    }


    // 물뿌린 밭에 작물을 심고 리스트에 저장
    // 해당 타일에 작물 오브젝트가 존재하면 심지 않음
    public void CreateCrop(int seedId, Vector3Int pos)
    {
        var seedData = DataManager.instance.GetData<SeedData>(seedId);
        var cropDatas = DataManager.instance.GetDataList<CropData>();

        List<CropData> cropDataList = new List<CropData>();

        foreach (var data in cropDatas)
        {
            cropDataList.Add(data);
        }

        bool check = FindCrop(pos);

        if (check == false)
        {
            var cropData = DataManager.instance.GetData<CropData>(seedData.plant_item_id);
            GameObject cropGo = Instantiate(Resources.Load<GameObject>(cropData.prefab_path));
            cropGo.transform.position = new Vector3(pos.x, pos.y, 0);
            cropGo.transform.parent = this.cropObject.transform;

            Crop crop = cropGo.GetComponent<Crop>();
            crop.Init(seedData.plant_item_id);

            crop.onGetWateringDirtTile = (pos, crop) =>
            {
                this.onGetFarmTile(cropData.id, pos, crop);
            };
            crop.onDestroy = (cropGo) =>
            {
                this.cropList.Remove(cropGo.GetComponent<Crop>());
                Destroy(cropGo);
            };
            this.cropList.Add(crop);
        }
    }

    // 작물이 심긴 위치에 물 타일이 있는지 조사 
    public void CheckWateringDirt()
    {
        foreach (var crop in cropList)
        {
            crop.CheckWateringDirt();
        }
    }

    // 작물이 자라남
    public void GrowUpCrop(int id, Crop crop)
    {
        var data = DataManager.instance.GetData<CropData>(id);
        crop.GrowUp(data.id);
    }

    #region 미완성 코드
    // 리스트에서 작물 지우기
    public void RemoveCrop()
    {

    }
    #endregion
}
