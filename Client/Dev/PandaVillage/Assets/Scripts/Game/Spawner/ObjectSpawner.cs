using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objGos;

    public Vector2Int mapBottomLeft, mapTopRight;


    public void Init()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("WallObject"));

        for (int y = mapBottomLeft.y; y <= mapTopRight.y; y++)
        {
            for (int x = mapBottomLeft.x; x <= mapTopRight.x; x++)
            {
                var cols = Physics2D.OverlapCircleAll(new Vector2(x + 0.5f, y + 0.5f), 0.4f, layerMask);
                //Debug.Log(cols.Length);
                if (cols.Length == 0)
                {
                    var rand = Random.Range(0, 5);

                    if (rand == 4)
                    {
                        rand = Random.Range(0, 5);
                        GameObject objGo = Instantiate<GameObject>(objGos[rand]);
                        objGo.transform.position = new Vector3(x, y, 0);
                        objGo.transform.parent = this.transform;
                    }
                }




                //if(cols.Length == 0)
                //{
                //    var rand = Random.Range(0, 3);

                //    if (rand == 2)
                //    {
                //        rand = Random.Range(0, 5);
                //        GameObject objGo = Instantiate<GameObject>(objGos[rand]);
                //        objGo.transform.position = new Vector3(x, y, 0);
                //        objGo.transform.parent = this.transform;
                //    }
                //}


                //foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(x + bottomLeft.x + 0.5f, y + bottomLeft.y + 0.5f), 0.4f))
                //{
                //    if (col.gameObject.layer == LayerMask.NameToLayer("Wall") || col.gameObject.layer == LayerMask.NameToLayer("WallObject"))
                //        isWall = true;

                //}


            }
        }


    }
}
