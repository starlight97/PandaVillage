using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform targetPoint;
    public Transform damageTextPoint;

    public Transform hpGuagePoint;
    public float speed = 1f;

    public System.Action<Vector3> onUpdateMove;
    public System.Action<int> onHit;
    private Coroutine moveRoutine;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.moveRoutine != null)
                this.StopCoroutine(moveRoutine);
            moveRoutine = this.StartCoroutine(this.MoveRoutine());

            this.Hit(Random.Range(10, 1000));
        }
    }

    private void Hit(int damage)
    {
        this.onHit(damage);
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            var dir = targetPoint.position - this.transform.position;
            this.transform.Translate(dir.normalized * this.speed * Time.deltaTime);

            this.onUpdateMove(this.transform.position);

            var distance = dir.magnitude;
            if (distance <= 0.1f)
                break;
            yield return null;
        }
    }
}
