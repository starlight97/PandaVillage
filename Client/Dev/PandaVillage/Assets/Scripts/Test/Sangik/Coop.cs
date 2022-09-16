using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coop : Building
{

    private Transform door;
    private bool isOpen =false;

    public List<Animal> animalList= new List<Animal>();
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>, Animal> onDecideTargetTile;
    public UnityAction<string, int ,int> showAnimalUI;  //이름 우정 나이

    public GameObject animalPrefab;


    public void Init()
    {        
        this.door = this.transform.GetChild(1);        
    }

    
    //문열고닫기
    public bool SetDoor()
    {
        if (isOpen)
        {
            this.door.gameObject.SetActive(true);
            this.isOpen = false;
            return isOpen;
        }
        else
        {          
            this.door.gameObject.SetActive(false);
            this.isOpen = true;
            return isOpen;
        }
    }


    public void FindAnimals()
    {
        var animals = GameObject.FindObjectsOfType<Animal>();
        foreach (var animal in animals)
        {
            animal.Init();
            this.animalList.Add(animal);
            animal.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
            {
                this.DecideTargetTile(startPos, targetPos, pathList, animal);
            };
        }
    }

    // 닭장안에 있는 모든 동물 성장
    public void GrowUp()
    {
        foreach (var animal in animalList)
        {
            animal.GrowUp();
        }
    }

    public void DecideTargetTile(Vector2Int startPos, Vector2Int targetPos, List<Vector3>pathList, Animal animal)
    {
        this.onDecideTargetTile(startPos, targetPos, pathList, animal);
    }

    public void AnimalsGoHome()
    {
        foreach (var animal in animalList)
        {
            animal.ComeBackHome();
            AnimalManager.instance.coopOpened = false;
        }
    }

    public void CreateAnimal()
    {
        GameObject animalGo = Instantiate<GameObject>(animalPrefab);
        animalGo.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 4, 0);
        //animalGo.transform.parent = this.transform;

        var animal = animalGo.GetComponent<Animal>();
        animal.Init();
        animal.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.DecideTargetTile(startPos, targetPos, pathList, animal);
        };
        this.animalList.Add(animal);
    }

    public void DoorOpen()
    {
        if (SetDoor() && !AnimalManager.instance.coopOpened) //문이 열렸을경우에만 모든 동물들이 나온다.
        {
            foreach (var data in AnimalManager.instance.AnimalDic.Values)
            {
                var go = Instantiate(data);
                //coop의 문의 포지션을 가져옴
                Vector3 DoorPos = transform.GetChild(1).position;
                go.transform.position = DoorPos;
            }
            AnimalManager.instance.coopOpened = true;
        }

        FindAnimals();
    }
  

}
