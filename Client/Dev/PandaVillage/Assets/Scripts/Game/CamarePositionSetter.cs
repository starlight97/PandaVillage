using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamarePositionSetter : MonoBehaviour
{
    public GameObject playerGo;
    public void Init()
    {

    }

    private void LateUpdate()
    {
        Vector3 newPos = playerGo.transform.position;
        
        newPos.y += 1;
        newPos.z = -5;

        if (newPos.x <= 14)
        {
            newPos.x = 14;
        }
        if(newPos.y <= 8)
        {
            newPos.y = 8;
        }

        if (newPos.x >= 66)
        {
            newPos.x = 66;
        }
        if (newPos.y >= 57)
        {
            newPos.y = 57;
        }

        this.transform.position = newPos;
    }


}
