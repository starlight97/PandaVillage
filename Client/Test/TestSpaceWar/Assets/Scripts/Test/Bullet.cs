using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float damage = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mov = Vector3.up*moveSpeed* Time.deltaTime;
        this.transform.Translate(mov);

        if (this.transform.position.y > 6)
        {
            Destroy(this.gameObject);
        }
    }
}
