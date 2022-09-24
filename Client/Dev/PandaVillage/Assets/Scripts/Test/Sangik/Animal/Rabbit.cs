using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : CoopAnimal
{
    public GameObject productGo;

    public override void GrowUp()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public override void Produce()
    {
        Debug.Log("농장 내부에 생산품을 뿌려줘야함");
        var product = Instantiate<GameObject>(productGo);
        product.transform.position = new Vector2(Random.Range(mapBottomLeft.x, mapTopRight.x + 1), Random.Range(mapBottomLeft.y, mapTopRight.y + 1));
    }
}
