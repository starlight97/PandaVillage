using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherObject : MonoBehaviour
{
    public string objectName;

    public void DestroyObject()
    {
        Debug.LogFormat("{0} 아이템 획득", objectName);
        Destroy(this.gameObject);
    }
}
