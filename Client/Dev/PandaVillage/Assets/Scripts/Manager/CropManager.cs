using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using System.Linq;

public class CropManager : MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject[] cropPrefabs;
    public GameObject cropObject;

    private Sprite[] sprites;

    private void Start()
    {
        this.sprites = Resources.LoadAll<Sprite>("Sprites/Seeds/Parsnip");
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

    // 생성
    public void CreateCrop(Vector3Int pos) 
    {
        bool check = FindCrop(pos);

        if (check == false)
        {
            int seedRand = Random.Range(0, 2);
            GameObject cropGo = Instantiate<GameObject>(this.cropPrefabs[0]);
            cropGo.transform.position = new Vector3(pos.x, pos.y, 0);
            cropGo.transform.parent = this.cropObject.transform;

            SpriteRenderer spRenderer = cropGo.GetComponent<SpriteRenderer>();
            spRenderer.sprite = sprites[seedRand];
        }
    }

    //// 매일 물을 주고 시간이 자라면 작물이 자람
    //public void GrowCrop()
    //{

    //}

    // 작물이 자람에 따라 스프라이트 변경됨
    public void ChangeCropSprite(int index)
    {

    }

    // 작물 리스트에서 삭제
    public void RemoveCrop()
    {

    }
}
