using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coop : MonoBehaviour
{

    private Transform door; //(7.12, 2.02, 0.00)
    private List<Animal> animalList= new List<Animal>();
    private bool isOpen =false;
    
    public void Init()
    {
        this.door = this.transform.GetChild(1);

        //현재 씬에 있는 Animal들을 찾아서 리스트에 넣는다.
        var animalArr = GameObject.FindObjectsOfType<Animal>();     

        foreach (var animal in animalArr)
        {            
            animalList.Add(animal);            
        }

        //집위치를 동물들에게 알려줌
        foreach (var animal in animalList)
        {
            animal.goHome = () => {
                animal.target = new Vector2Int((int)door.position.x, (int)door.position.y -1);
            };
        }        
    }


    public void SetDoor()
    {
        if (isOpen)
        {
            this.door.gameObject.SetActive(true);
            this.isOpen = false;
        }
        else
        {          
            this.door.gameObject.SetActive(false);
            this.isOpen = true;
        }
    }


}
