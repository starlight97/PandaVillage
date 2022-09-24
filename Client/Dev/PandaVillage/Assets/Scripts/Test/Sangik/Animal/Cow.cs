using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : BarnAnimal
{
    public override void GrowUp()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }


    public override void Produce()
    {
        Debug.Log("우유획득");
    }
}
