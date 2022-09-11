using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coop : Building
{

    private Transform door;
    private List<Animal> animalList= new List<Animal>();
    private bool isOpen =false;
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>, Animal> onDecideTargetTile;


    public void Init()
    {        
        this.door = this.transform.GetChild(1);

        //test
        base.FindCollider();
        StartCoroutine(base.ColliderRoutine());

        FindAnimals();
    }

    //private void Update()
    //{
    //    // Test
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //    HideObject(this.transform.GetChild(0).gameObject);
    //    HideObject(this.transform.GetChild(1).gameObject);
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        ShowObject(this.transform.GetChild(0).gameObject);
    //        ShowObject(this.transform.GetChild(1).gameObject);
    //    }
    //}


    //private void AddList()
    //{
    //    //현재 씬에 있는 Animal들을 찾아서 리스트에 넣는다.
    //    var animalArr = GameObject.FindObjectsOfType<Animal>();

    //    foreach (var animal in animalArr)
    //    {
    //        animalList.Add(animal);
    //    }        
    //}

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


    private void FindAnimals()
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


}
