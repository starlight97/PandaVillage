using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private Collider2D RoofCollider;
    private Collider2D player;
    public void FindCollider()
    {
        this.player = GameObject.FindObjectOfType<Player>().gameObject.GetComponent<Collider2D>();
        this.RoofCollider = this.transform.Find("Roof").GetComponent<Collider2D>();
        Debug.Log(RoofCollider.name);        
    }
    public IEnumerator ColliderRoutine()
    {
        while (true)
        {
            if (RoofCollider.IsTouching(player))
            {
                //Debug.Log("HideObject");
                HideObject(this.gameObject);
            }
            else
            {
                //Debug.Log("ShowObject");
                ShowObject(this.gameObject);
            }
            yield return null;
            //yield return new WaitForSeconds(.5f);
        }
        
    }
    public void HideObject(GameObject go)       //go의 SpriteRenderer와 go의 자식들의 SpriteRenderer의 알파값을 변경
    {
        if (go.GetComponent<SpriteRenderer>() != null)
        {
            var color = go.GetComponent<SpriteRenderer>().color;
            color = new Color(1f, 1f, 1f, .5f);
        }

        foreach (var sprite in go.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = new Color(1f, 1f, 1f, .5f);
        }
        

    }

    public void ShowObject(GameObject go)
    {
        if (go.GetComponent<SpriteRenderer>() != null)
        {
            var color = go.GetComponent<SpriteRenderer>().color;
            color = new Color(1f, 1f, 1f, 1f);
        }

        foreach (var sprite in go.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }

    }

}
