using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnItem(Vector3 pos)
    {
        var itemPrefabsLength = itemPrefabs.Length;
        var randItemIndex = Random.Range(0, itemPrefabsLength);

        var go = Instantiate<GameObject>(this.itemPrefabs[randItemIndex]);
        go.transform.position = pos;
    }
}
