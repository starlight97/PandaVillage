using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RanchManager : MonoBehaviour
{
    public Silo[] siloArr;
    public Coop[] coopArr;

    public UnityAction<Vector2Int, Vector2Int, List<Vector3>, Animal> onDecideTargetTile;

    public int hay;
    public int maxHay;

    public void Init()
    {
        this.siloArr = GameObject.FindObjectsOfType<Silo>();
        this.coopArr = GameObject.FindObjectsOfType<Coop>();

        this.maxHay = siloArr.Length * 240;

        foreach (var coop in coopArr)
            coop.Init();

        AnimalsInit();
    }   

    //건초추가
    public void AddHay()
    {
        if (hay < maxHay)
        {
            hay++;
        }
    }

    //사일로 UI보이게하기
    public void ShowSiloUI(Vector3 mousePos)
    {
        foreach (var silo in siloArr)
        {
            if (mousePos == silo.transform.position)
            {
                Debug.LogFormat("건초의양 : {0} / {1}", hay, maxHay);
            }
        }        
    }


    public void NextDay()
    {
        foreach(var coop in coopArr)
        foreach (var animal in coop.animalList)
        {
            //나이먹기
            animal.age++;
            //쓰다듬초기화
            animal.isPatted = false;


            //동물들이 배부른경우 성장과 생산을 할수 있음
            if (animal.isFull)
            {
                animal.yummyDay++;

                if (animal.yummyDay > 6)
                    animal.Produce();

                if (animal.yummyDay == 6)
                {
                    animal.GrowUp();
                }
            }


            // 사일로의 건초가 있으면 먹이주기
            if (hay > 0)
            {
                hay--;
                animal.isFull = true;
            }
            else
            {
                animal.isFull = false;
            }
        }
    }

    public void DoorOpen()
    {
        foreach (var coop in this.coopArr)
        {
            coop.DoorOpen();
        }
    }

    public void AnimalsGoHome()
    {
        foreach (var coop in this.coopArr)
            coop.AnimalsGoHome();
    }

    public void AnimalsInit()
    {
        //animal들 초기화          

        foreach (var coop in this.coopArr)
        {
            //동물들 움직이기
            coop.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
            {
                this.onDecideTargetTile(startPos, targetPos, pathList, animal);
            };



            foreach (var animal in coop.animalList)
            {
                //동물들이 집으로 가게하기
                animal.goHome = (startPos, pathList) =>
                {
                    //coop의 문의 포지션을 가져옴
                    var DoorPos = coop.transform.GetChild(1).position;
                    Vector2Int targetPos = new Vector2Int((int)DoorPos.x, (int)DoorPos.y - 1);
                    this.onDecideTargetTile(startPos, targetPos, pathList, animal);
                };
            }
        }
    }
    
}
