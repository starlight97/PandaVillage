using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum eItemType
    {
        None = -1,
        Hoe, 
        WateringCan,
        Fertilizer,
        Seed,
        Axe
    }

    private enum eStateType
    {
        None = -1,
        IdleFront, RunFront,
        IdleSide, RunSide,
        IdleBack, RunBack
    }

    private Movement2D movement2D;
    private Farming farming;
    private eItemType isUseTool = eItemType.None;
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>> onDecideTargetTile;

    public UnityAction<Vector3Int, TileManager.eTileType> onGetTile;
    public UnityAction<Vector3Int, eItemType> onChangeFarmTile;

    public UnityAction<GameObject> onSelectedBuilding;
    public UnityAction<Animal> onShowAnimalUI;

    public UnityAction<Vector3Int> onPlantCrop;

    private Vector3Int dir;
    private Vector3Int pos;

    private bool isShowAnimalUI= false;
    public bool isBuildingSelected = false;

    private Animator anim;
    private SpriteRenderer spriteR;

    private void Start()
    {
        this.spriteR = this.GetComponentInChildren<SpriteRenderer>();
        this.movement2D = GetComponent<Movement2D>();
        this.movement2D.onPlayAnimation = (dir) => {
            this.SetAnimation(dir);
        };
        this.movement2D.onMoveComplete = (dir) =>
        {
            this.SetIdle(dir);
        };
        this.farming = GetComponent<Farming>();
        this.anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        // 마우스 클릭시 좌표를 인게임 좌표로 변환하여 mousePos 변수에 저장
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z값은 사용을 안하므로 x, y 값만 저장후 Move
        int targetPosX = (int)Math.Truncate(mousePos.x);
        int targetPosY = (int)Math.Truncate(mousePos.y);

        int currentPosX = (int)Math.Truncate(this.transform.position.x);
        int currentPosY = (int)Math.Round(this.transform.position.y);
        Vector2Int curPos = new Vector2Int(currentPosX, currentPosY);
        Vector2Int targetPos = new Vector2Int(targetPosX, targetPosY);
        
        if (Input.GetMouseButtonDown(1))
        {
            this.pos = new Vector3Int(currentPosX, currentPosY, 0);
            Vector3Int tpos = new Vector3Int((int)mousePos.x, (int)mousePos.y, 0);



            this.movement2D.pathList.Clear();
            this.onDecideTargetTile(curPos, targetPos, this.movement2D.pathList);
        }

        // 왼쪽 마우스 클릭 시 타일 변경됨
        if (Input.GetMouseButtonDown(0)) 
        {
            if(isShowAnimalUI)
            {
                isShowAnimalUI = false;
                onShowAnimalUI(null);
                return;
            }
            this.pos = new Vector3Int(currentPosX, currentPosY, 0);
            Vector3Int tpos = new Vector3Int((int)mousePos.x, (int)mousePos.y, 0);

            GameObject findGo = FindObject(tpos);


            
            if (findGo != null && isBuildingSelected == false && findGo.tag == "Building")
            {
                findGo.transform.position = new Vector3(-10, -10, 0);
                this.onSelectedBuilding(findGo);
                isBuildingSelected = true;
                return;
            }


            this.dir = tpos - this.pos;

            var tilePos = this.dir + this.pos;

            

            if (isUseTool == eItemType.None)
            {
                //this.moveMent2D.pathList.Clear();
                //this.onDecideTargetTile(curPos, targetPos, this.moveMent2D.pathList);
            }

            if (Mathf.Abs(dir.x) <= 1 && Mathf.Abs(dir.y) <= 1)
            {
                if (findGo != null && findGo.tag == "Portal")
                {
                    findGo.GetComponent<Portal>().ClickPotal();
                    return;
                }

                findGo = FindObject(tilePos);
                if(findGo != null)
                {
                    Debug.Log(findGo.name);
                    if(findGo.tag == "Crop")
                    {
                        Crop crop = findGo.GetComponent<Crop>();
                        if (crop.isHarvest == true)
                        {
                            crop.Harvest();
                            return;
                        }
                    }
                    if (findGo.tag == "Animal")
                    {
                        Animal animal = findGo.GetComponent<Animal>();
                        if (animal.isPatted == false)
                            animal.Patted();
                        else
                        {
                            this.onShowAnimalUI(animal);
                            isShowAnimalUI = true;
                        }

                        
                        return;
                    }

                }

                switch (isUseTool)
                {
                    case eItemType.None:
                        break;
                    case eItemType.Hoe:
                    case eItemType.WateringCan:
                        GetFarmTile(tilePos, isUseTool);
                        break;
                    case eItemType.Fertilizer:
                        break;
                    case eItemType.Seed:
                        GetFarmTile(tilePos, isUseTool);
                        break;
                    case eItemType.Axe:
                        Attack(dir);
                        break;
                    default:
                        break;
                }
            }
        }
        #region 키보드로 장비 고르기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.isUseTool = eItemType.Hoe;
            Debug.Log("Hoe");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.isUseTool = eItemType.WateringCan;
            Debug.Log("WateringCan");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.isUseTool = eItemType.Seed;
            Debug.Log("Seed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.isUseTool = eItemType.Axe;
            Debug.Log("Axe");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.isUseTool = eItemType.Fertilizer;
            Debug.Log("Fertilizer");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("6");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("7");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("8");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("9");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            this.isUseTool = eItemType.None;
            Debug.Log("None");
        }
        #endregion
    }

    private void UseTool()
    {

    }

    private void GetTool()
    {

    }

    #region Animation
    public void SetAnimation(Vector3 dir)
    {
        // 상
        if (dir.x == 0 && dir.y > 0)
        {
            this.anim.SetInteger("State", (int)eStateType.RunBack);
        }

        // 하
        else if (dir.x == 0 && dir.y < 0)
        {
            this.anim.SetInteger("State", (int)eStateType.RunFront);
        }

        //좌
        else if (dir.x < 0)
        {
            this.anim.SetInteger("State", (int)eStateType.RunSide);
            spriteR.flipX = true;
        }

        // 우
        else if (dir.x > 0)
        {
            this.anim.SetInteger("State", (int)eStateType.RunSide);
            spriteR.flipX = false;
        }
    }

    private void SetIdle(Vector3 dir)
    {
        // 상
        if (dir.x == 0 && dir.y > 0)
        {
            this.anim.SetInteger("State", (int)eStateType.IdleBack);
        }

        // 하
        else if (dir.x == 0 && dir.y < 0)
        {
            this.anim.SetInteger("State", (int)eStateType.IdleFront);
        }

        //좌
        else if (dir.x < 0)
        {
            this.anim.SetInteger("State", (int)eStateType.IdleSide);
            spriteR.flipX = true;
        }

        // 우
        else if (dir.x > 0)
        {
            this.anim.SetInteger("State", (int)eStateType.IdleSide);
            spriteR.flipX = false;
        }
    }
    #endregion

    public void Move()
    {
        this.movement2D.Move();
    }

    public void GetFarmTile(Vector3Int pos, eItemType state)
    {
        TileManager.eTileType type = farming.GetFarmTile(state);
        if (type != TileManager.eTileType.None)
            this.onGetTile(pos, type);
    }

    public void FarmingAct(Vector3Int pos)
    {
        Farming.eFarmActType actType = this.farming.FarmingToolAct(this.isUseTool);

        switch (actType)
        {
            case Farming.eFarmActType.None:
                break;
            case Farming.eFarmActType.Plant:
                this.onPlantCrop(pos);
                break;
            case Farming.eFarmActType.SetTile:
                this.onChangeFarmTile(pos, this.isUseTool);
                break;
            default:
                break;
        }
    }

    public void Attack(Vector3 dir)
    {
        Vector3 startPos = this.transform.position;
        startPos.x += 0.5f;
        startPos.y += 0.5f;

        Debug.DrawRay(startPos, dir * 0.7f, new Color(0,1,0), 4f);
        //int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));  // Everything에서 Player 레이어만 제외하고 충돌 체크함
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("WallObject"));    // Object 와 WallObject 레이어만 충돌체크함


        RaycastHit2D rayHit = Physics2D.Raycast(startPos, dir, 1, layerMask);

        if(rayHit.collider != null)
        {
            OtherObject obj = rayHit.collider.gameObject.GetComponent<OtherObject>();
            obj.DestroyObject();
        }
    }

    private GameObject FindObject(Vector3Int tpos)
    {
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("WallObject"))
            + (1 << LayerMask.NameToLayer("Crop"));    // Object 와 WallObject 레이어만 충돌체크함
        GameObject findGo = null;

        var col = Physics2D.OverlapBox(new Vector2(tpos.x + 0.5f, tpos.y + 0.5f), new Vector2(0.95f, 0.95f),0, layerMask);

        if (col != null)
        {
            findGo = col.gameObject;
            //OtherObject obj = col.gameObject.GetComponent<OtherObject>();
            //obj.DestroyObject();
        }
        return findGo;
    }
}

// 작성자 : 박정식
// 마지막 수정 : 2022-08-14 
// 플레이어 관련 스크립트입니다.
