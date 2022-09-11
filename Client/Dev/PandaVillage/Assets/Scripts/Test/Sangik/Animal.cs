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

    private Coroutine roamingRoutine;

    public UnityAction<Vector2Int, Vector2Int, List<Vector3>, Animal> onDecideTargetTile;
    public UnityAction<Vector2Int, List<Vector3>> onGoHome;
    private Movement2D movement2D;
    public Vector2Int target;
    public Vector2Int mapBottomLeft, mapTopRight;

    public Vector2Int RandomMoveRange = new Vector2Int(-3, 3);

    public void Init()
    {              
        this.movement2D = GetComponent<Movement2D>();
        Roaming();        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ComeBackHome();
            AnimalManager.instance.coopOpened = false;

        }
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

    public void GrowUp()    
    {
        Destroy(this.transform.GetChild(0).gameObject);
        Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/RabbitModel"), this.transform);

        
        //this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/Animals/Rabbit");
        //this.GetComponentInChildren<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/TestRabbit_0");       

    }
    public void ComeBackHome()
    {
        StopCoroutine(roamingRoutine);        
        var curPos = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);

        this.movement2D.pathList.Clear();
        onGoHome(curPos, this.movement2D.pathList);            
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
        product.transform.position = new Vector2(Random.Range(mapBottomLeft.x, mapTopRight.x+1), Random.Range(mapBottomLeft.y, mapTopRight.y+1));
    }

    public void Childbirth()
    {

    }



}
