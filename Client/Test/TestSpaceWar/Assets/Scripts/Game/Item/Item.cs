using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 2;
    public int score;
    public string itemName;

    void Update()
    {
        this.transform.Translate(Vector3.down * this.speed * Time.deltaTime);
        if (this.transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }

    }
}
