using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float hp = 10f;
    [SerializeField]
    private int score = 10;
    private float maxHp;

    public GameObject[] enemySprites;
    private Coroutine hitCoroutine;
    public UnityAction<Vector3> onDie;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        this.maxHp = this.hp;
    }

    private void Update()
    {
        this.transform.Translate(Vector3.down * this.speed * Time.deltaTime);
        if (this.transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            Destroy(collision.gameObject);
            this.Die();
        }
        else if (collision.CompareTag("Bullet"))
        {
            var bullet = collision.GetComponent<Bullet>();
            this.Hit(bullet.damage);
            Destroy(collision.gameObject);
        }
    }

    public void Hit(float damage)
    {
        Debug.Log("hit");
        if (this.hitCoroutine != null)
            this.StopCoroutine(this.hitCoroutine);

        this.hitCoroutine = this.StartCoroutine(this.HitRoutine(damage));
    }

    IEnumerator HitRoutine(float damage)
    {
        this.hp -= damage;

        this.enemySprites[0].SetActive(false);
        this.enemySprites[1].SetActive(true);
        yield return null;

        this.enemySprites[0].SetActive(true);
        this.enemySprites[1].SetActive(false);

        if (this.hp <= 0)
        {
            this.hp = 0;
            this.Die();
        }
    }

    private void Die()
    {
        this.onDie(this.transform.position);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
}
