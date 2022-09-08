using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager
{
    public static readonly AnimalManager instance = new AnimalManager();

    private AnimalManager() { }
    public Dictionary<int, Animal> AnimalDic = new Dictionary<int, Animal>();
    private int i =0;
    public void AddAnimal()
    {
        AnimalDic.Add(i, Resources.Load<Animal>("Prefabs/Animal"));
        i++;
    }

    public void SpawnAnimal()
    {
        
    }
}
