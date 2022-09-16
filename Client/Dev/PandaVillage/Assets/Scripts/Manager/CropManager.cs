using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using System.Linq;

public class CropManager : MonoBehaviour
{
    public GameObject[] cropPrefabs;
    public GameObject cropObject;

    public List<Crop> cropList;
    public UnityAction<Vector3Int, Crop> onGetFarmTile;

    public void Init()
    {
        this.cropList = new List<Crop>();
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

    // 물뿌린 밭에 작물을 심고 리스트에 저장
    // 해당 타일에 작물 오브젝트가 존재하면 심지 않음
    public void CreateCrop(Vector3Int pos)
    {
        bool check = FindCrop(pos);

        if (check == false)
        {
            GameObject cropGo = Instantiate<GameObject>(this.cropPrefabs[0]);
            cropGo.transform.position = new Vector3(pos.x, pos.y, 0);
            cropGo.transform.parent = this.cropObject.transform;

            Crop crop = cropGo.GetComponent<Crop>();
            crop.Init();
            crop.onGetWateringDirtTile = (pos, crop) =>
            {
                this.onGetFarmTile(pos, crop);
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
    public void GrowUpCrop(Crop crop)
    {
        crop.GrowUp();
    }

    #region 미완성 코드
    // 리스트에서 작물 지우기
    public void RemoveCrop()
    {

    }
    #endregion
}
