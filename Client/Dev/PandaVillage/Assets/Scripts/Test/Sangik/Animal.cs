using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Animal : MonoBehaviour
{
    private int animalName;         //이름
    private int friendship;         //우정    
    private int feelings;           //기분
    public int age;                 //나이

    private Coroutine moveRoutine;

    public UnityAction<Vector2Int, Vector2Int> onDecideTargetTile;
    public UnityAction goHome;    
    private PlayerMove playerMove;
    public Vector2Int target;

    public int MapTopRightX =16;
    public int MapTopRightY =8;

    public void Init()
    {        
        this.playerMove = GetComponent<PlayerMove>();
        Move();        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ComeBackHome();
        }
    }

    public void Move()
    {
        this.moveRoutine=StartCoroutine(MoveRoutine());
    }    
    #region 무브루틴
    public IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);            
            var targetPos = new Vector2Int((int)this.transform.position.x + Random.Range(-3, 4), (int)this.transform.position.y + Random.Range(-3, 4));

            #region 똥같은 코드
            if (targetPos.x < 0)
                targetPos.x = -targetPos.x;
            if (targetPos.x >= MapTopRightX)
                targetPos.x = (int)this.transform.position.x + ((int)this.transform.position.x - targetPos.x);
            if (targetPos.y < 0)
                targetPos.y = -targetPos.y;
            if (targetPos.y >= MapTopRightY)
                targetPos.y = (int)this.transform.position.y + ((int)this.transform.position.y - targetPos.y);
            #endregion


            var curPos = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);
            onDecideTargetTile(curPos, targetPos);
        }       
    }
    #endregion
    public void MovePlayer(List<Vector3> pathList)
    {
        this.playerMove.Move(pathList);
    }

    public void GrowUp()    
    {

        Destroy(this.transform.GetChild(0).gameObject);
        Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/RabbitModel"), this.transform);


        //this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/Animals/Rabbit");
        //this.GetComponentInChildren<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/TestRabbit_0");       

    }
    public void ComeBackHome()
    {
        goHome();
        StopCoroutine(moveRoutine);        
        var curPos = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);
        onDecideTargetTile(curPos, target);      
    }
    
    public void Patted()
    {

    }

    public void ShowStateUI()
    {

    }


    public void Produce()
    {
        var product = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Product"));
        product.transform.position = new Vector2(Random.Range(0,17), Random.Range(0, 9));
    }

    public void Childbirth()
    {

    }



}
