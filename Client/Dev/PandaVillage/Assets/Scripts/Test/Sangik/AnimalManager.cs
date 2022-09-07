using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager
{
    public static readonly AnimalManager instance = new AnimalManager();

    private AnimalManager() { }
    private Dictionary<bool, Animal> AnimalDic = new Dictionary<bool, Animal>();
    
    public void Init()
    {
        AnimalDic.Add(true, Resources.Load<Animal>("Prefabs/Animal"));
    }

}
