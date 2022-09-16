using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crop : OtherObject
{
    private int state;
    private int maxState = 5;
    public bool isHarvest = false;

    private SpriteRenderer spRenderer;
    private Sprite[] sprites;
    public UnityAction<Vector3Int, Crop> onGetWateringDirtTile;
    public UnityAction<GameObject> onDestroy;

    public void Init()
    {
        this.state = 1;
        this.spRenderer = this.GetComponent<SpriteRenderer>();
        this.sprites = Resources.LoadAll<Sprite>("Sprites/Seeds/Parsnip");
        GetSeedSprite();
    }

    // 씨앗 스프라이트 2개 랜덤으로 지정
    public void GetSeedSprite()
    {
        int rand = Random.Range(0, 2);
        this.spRenderer.sprite = this.sprites[rand];
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
    public void GrowUp()
    {
        if (state <= maxState)
        {
            state++;
            this.spRenderer.sprite = sprites[state];
        }

        if (state == maxState)
            isHarvest = true;
    }

    // 수확하면 오브젝트 지워달라는 액션 보냄
    public void Harvest()
    {
        this.onDestroy(this.gameObject);
    }
}
