using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Animal : MonoBehaviour
{
    public string animalName;           //이름
    public int friendship;              //우정, 호감도    
    //public int mood;                //기분
    public int age;                     //나이
    public int yummyDay;                 //밥먹은날

    public bool isFull = true;           //배부른가?
    public bool isPatted = false;        //쓰다듬어졌나?

    private GameObject emote;


    private Coroutine roamingRoutine;

    public UnityAction<Vector2Int, Vector2Int, List<Vector3>, Animal> onDecideTargetTile;
    public UnityAction<Vector2Int, List<Vector3>> goHome;
    private Movement2D movement2D;

    public Vector2Int mapBottomLeft, mapTopRight;

    public Vector2Int RandomMoveRange = new Vector2Int(-3, 3);

    public void Init()
    {              
        this.movement2D = GetComponent<Movement2D>();
        this.emote = this.transform.Find("emote").gameObject;
        Roaming();

    }

    // 돌아다니는 함수 입니다.
    public void Roaming()
    {
        if (this.roamingRoutine != null)
            this.StopCoroutine(this.roamingRoutine);

        this.roamingRoutine = StartCoroutine(RoamingRoutine());
    }
    #region 무브루틴
    public IEnumerator RoamingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);            
            var targetPos = new Vector2Int((int)this.transform.position.x + Random.Range(RandomMoveRange.x, RandomMoveRange.y+1), 
                (int)this.transform.position.y + Random.Range(RandomMoveRange.x, RandomMoveRange.y+1));


            #region 똥같은 코드            
            if (targetPos.x < mapBottomLeft.x)
                targetPos.x = mapBottomLeft.x;
            if (targetPos.x >= mapTopRight.x)
                targetPos.x = mapTopRight.x;
            if (targetPos.y < mapBottomLeft.y)
                targetPos.y = mapBottomLeft.y;
            if (targetPos.y >= mapTopRight.y)
                targetPos.y = mapTopRight.y;
            #endregion
            

            var curPos = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);
            this.movement2D.pathList.Clear();
            onDecideTargetTile(curPos, targetPos, this.movement2D.pathList, this);
        }       
    }
    #endregion

    // 무브먼트 2D에 무브루틴 호출
    public void Move()
    {
        this.movement2D.Move();
    }

    public virtual void GrowUp()    
    {
        //Destroy(this.transform.GetChild(0).gameObject);
        //Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/RabbitModel"), this.transform);

        //스프라이트 교체로 할경우 
        //this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/Animals/Rabbit");
        //this.GetComponentInChildren<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/TestRabbit_0");       

    }
    public void ComeBackHome()
    {
        StopCoroutine(roamingRoutine);        
        var curPos = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);

        this.movement2D.pathList.Clear();
        goHome(curPos, this.movement2D.pathList);
    }
    

    // 쓰다듬기는 하루에 한번만 가능
    public void Patted()
    {        
        StartCoroutine(PattedRoutine());
    }

    private IEnumerator PattedRoutine()
    {
        this.isPatted = true;
        this.friendship += 15;
        emote.SetActive(true);
        yield return new WaitForSeconds(1.417f);
        emote.SetActive(false);
    }


    public virtual void Produce()
    {
        var product = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Product"));
        product.transform.position = new Vector2(Random.Range(mapBottomLeft.x, mapTopRight.x+1), Random.Range(mapBottomLeft.y, mapTopRight.y+1));
    }

    public void Childbirth()
    {

    }



}