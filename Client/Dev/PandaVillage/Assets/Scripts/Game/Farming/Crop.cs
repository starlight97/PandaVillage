using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class Crop : OtherObject
{
    private int state;
    private int maxState;
    public bool isHarvest = false;

    private SpriteRenderer spRenderer;
    public SpriteAtlas atlas;
    private Sprite sprite;
    public UnityAction<Vector3Int, Crop> onGetWateringDirtTile;
    public UnityAction<GameObject> onHarvest;

    public void Init(int id)
    {
        this.spRenderer = this.GetComponent<SpriteRenderer>();
        var data = DataManager.instance.GetData<CropData>(id);
        data.id = id;
        this.maxState = data.max_state;
        this.state = 1;
        int rand = Random.Range(1, 2);
        Debug.Log(string.Format(data.sprite_name, rand));
        var sprite = this.atlas.GetSprite(string.Format(data.sprite_name, rand));

        this.spRenderer.sprite = sprite;
    }

    // 물타일 있냐?
    public void CheckWateringDirt()
    {
        int posX = (int)this.transform.position.x;
        int posY = (int)this.transform.position.y;
        Vector3Int pos = new Vector3Int(posX, posY, 0);
        this.onGetWateringDirtTile(pos, this);
    }

    // 작물이 자람에 따라 스프라이트 변경됨
    public void GrowUp(int id)
    {
        var data = DataManager.instance.GetData<CropData>(id);
        if (state <= maxState)
        {
            state++;
            Debug.LogFormat("{0} / {1}", state, maxState);
            Debug.Log("tnghkr: "+ isHarvest);
            var sprite = this.atlas.GetSprite(string.Format(data.sprite_name, state + 1));
            this.spRenderer.sprite = sprite;
        }

        if (state == maxState)
            isHarvest = true;
    }

    // 수확하면 오브젝트 지워달라는 액션 보냄
    public void Harvest()
    {
        this.onHarvest(this.gameObject);
    }
}
