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

        //coopscene에 있는 Animal들을 찾아서 리스트에 넣는다.
        var animalArr = GameObject.FindObjectsOfType<Animal>();     

        foreach (var animal in animalArr)
        {            
            animalList.Add(animal);            
        }

        foreach (var animal in animalList)
        {
            animal.goHome = () => {
                animal.target = new Vector2Int((int)door.position.x, (int)door.position.y -1);
            };
        }

        Instantiate<Animal>(animalList[0]);
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
