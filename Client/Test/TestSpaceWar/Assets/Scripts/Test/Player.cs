using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    private Animator anim;
    private int Hp;

    public InputController inputController;
    public float moveSpeed =3f;
    public Transform pivot;
    public GameObject bulletPrefab;


    public UnityAction<Vector3> onHit;
    public UnityAction<Vector3> onDie;

    public bool isTest;
    private Vector2 dirJoy;
    void Start()
    {
        Hp = 3;
        anim = this.GetComponent<Animator>();
        StartCoroutine(ShootRoutine());
        this.Move();
        

            this.inputController.onUpdate = (dir) =>
            {
                this.dirJoy = dir;
            };       
        this.inputController.Run();      
        
    }


   

    public void Shoot()
    {
        var bul = Instantiate(bulletPrefab);
        bul.transform.position = pivot.position;



        //2¹ß
        if (false)
        {
            bul.transform.position += new Vector3(0.15f, 0,0);
            var bul2 = Instantiate(bulletPrefab);
            bul2.transform.position = pivot.position+ new Vector3(-0.15f, 0, 0); 
        }
        //3¹ß
        if (false)
        {
            var bul3 = Instantiate(bulletPrefab);
            bul3.transform.position = pivot.position+ new Vector3(0, 0.15f, 0); 
        }
        
    }
    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
            

            yield return null;
        }
        
    }

    private void Hit()
    {
        this.transform.position = new Vector3(0,-4,0);
        this.Hp -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            onHit(this.transform.position);
            Debug.Log(Hp);
            this.Hit();
            if (this.Hp <= 0)
            {
                onDie(this.transform.position);
                Debug.Log("die");
            }


        }
    }
    
    private void Move()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
    
            while (true)
            {

               
                    float getX = Input.GetAxisRaw("Horizontal");
                    this.anim.SetInteger("DirX", (int)getX);
                    float getY = Input.GetAxisRaw("Vertical");
                    var movement = new Vector3(getX, getY, 0);
                    movement = movement.normalized * Time.deltaTime * this.moveSpeed;

                    this.transform.Translate(movement);
                


                if(!isTest)
                {
                    var nor = dirJoy.normalized;
                    this.anim.SetInteger("DirX", (int)dirJoy.x);
                    this.transform.Translate(nor * this.moveSpeed * Time.deltaTime, Space.World);
                }

            yield return null;
            }


    }
}
