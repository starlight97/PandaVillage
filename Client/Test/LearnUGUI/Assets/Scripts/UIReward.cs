using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIReward : MonoBehaviour
{
    public SpriteAtlas atlas;

    public Image imgIcon;

    public string iconName = "icon_itemicon_potiongreen1";


    // Start is called before the first frame update
    void Start()
    {
        Sprite sp = this.atlas.GetSprite(iconName);
        this.imgIcon.sprite = sp;
        this.imgIcon.SetNativeSize();
        float width = 80.50f;
        float height = 79.79f;
        this.imgIcon.GetComponent<RectTransform>().sizeDelta = new Vector3(width, height);
    }

}
    