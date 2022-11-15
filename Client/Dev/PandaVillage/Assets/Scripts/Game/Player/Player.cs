using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public enum eItemType
    {
        None = -1,
        Hoe = 1, 
        WateringCan,
        Axe,
        Pick,
        Sickle,
        Seed,
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

    public int useItemId = -1;
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>> onDecideTargetTile;
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>> onDecideTargetObject;
    
    public UnityAction<Vector3Int, TileManager.eTileType> onGetTile;
    public UnityAction<Vector3Int, TileManager.eTileType> onCheckTile;
    public UnityAction<Vector3Int, eItemType> onChangeFarmTile;

    public UnityAction<Animal> onShowAnimalUI;
    public UnityAction<Silo> onShowStateUI;
    public UnityAction onGetItem;
    public UnityAction onCutGrassComplete;

    public UnityAction<int, Vector3Int> onPlantCrop;
    public UnityAction<int> onSelectedItem;
    public UnityAction<int> onFillHay;

    private Vector3Int dir;
    private Vector3Int pos;

    private bool isShowAnimalUI= false;
    private bool isFarmAction = false;


    private Animator anim;
    private SpriteRenderer spriteR;
    [SerializeField] private SpriteAtlas atlas;


    private void Start()
    {
        this.spriteR = this.GetComponentInChildren<SpriteRenderer>();
        this.movement2D = GetComponent<Movement2D>();
        this.movement2D.onPlayAnimation = (dir) => {
            this.SetAnimation(dir);
        };
        this.movement2D.onMoveComplete = (dir) => { 
            SetIdle(dir);
        };
        this.movement2D.onMoveActionComplete = (dir) => { 
            SetIdle(dir);
        };
        this.farming = GetComponent<Farming>();
        this.anim = GetComponentInChildren<Animator>();

        SoundManager.instance.Init();

    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true || IsPointerOverUIObject() == true)
        {
            return;
        }

        // 마우스 클릭시 좌표를 인게임 좌표로 변환하여 mousePos 변수에 저장
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z값은 사용을 안하므로 x, y 값만 저장후 Move
        int targetPosX = (int)Math.Truncate(mousePos.x);
        int targetPosY = (int)Math.Truncate(mousePos.y);

        int currentPosX = (int)Math.Truncate(this.transform.position.x);
        int currentPosY = (int)Math.Round(this.transform.position.y);
        Vector2Int curPos = new Vector2Int(currentPosX, currentPosY);
        Vector2Int targetPos = new Vector2Int(targetPosX, targetPosY);

        //if (Input.GetMouseButton(1))
        //{
        //    this.pos = new Vector3Int(currentPosX, currentPosY, 0);
        //    Vector3Int tpos = new Vector3Int((int)mousePos.x, (int)mousePos.y, 0);

        //    this.movement2D.pathList.Clear();
        //    this.onDecideTargetTile(curPos, targetPos, this.movement2D.pathList);
        //}
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

            this.dir = tpos - this.pos;

            var tilePos = this.dir + this.pos;

            findGo = FindObject(tilePos);

            if (findGo == null || (findGo.CompareTag("Portal") && findGo.GetComponent<Portal>().isClickPortal == false))
            {
                CheckFarmTile(tilePos, isUseTool);
                if (isUseTool != eItemType.None)
                {
                    if (isFarmAction)
                    {
                        this.movement2D.onMoveActionComplete = (dir) =>
                        {
                            GetFarmTile(tilePos, isUseTool);
                            PlayToolSound();
                            if (isUseTool != eItemType.Seed && (useItemId == 6000 || useItemId == 6001))
                                ShowUseToolSprite();
                            SetIdle(dir);
                        };
                        this.movement2D.pathList.Clear();
                        this.onDecideTargetObject(curPos, targetPos, this.movement2D.pathList);
                        return;
                    }
                    else
                    {
                        this.movement2D.pathList.Clear();
                        this.onDecideTargetTile(curPos, targetPos, this.movement2D.pathList);
                        return;
                    }
                }
                this.movement2D.pathList.Clear();
                this.onDecideTargetTile(curPos, targetPos, this.movement2D.pathList);
                return;
            }
            
            else 
            {
                this.movement2D.onMoveActionComplete = (dir) => 
                {
                    if (findGo != null)
                    {
                        if (findGo.tag == "Portal")
                        {
                            var portal = findGo.GetComponent<Portal>();
                            if (portal.isClickPortal)
                            {
                                SoundManager.instance.PlaySound(SoundManager.eButtonSound.Portal);
                                portal.ClickPotal();
                                SetIdle(dir);
                                return;
                            }
                            else
                                return;
                        }

                        if (findGo.tag == "Crop")
                        {
                            Crop crop = findGo.GetComponent<Crop>();
                            if (crop.isHarvest == true)
                            {
                                Harvest(findGo);
                                SetIdle(dir);
                                return;
                            }
                            else if (isUseTool == eItemType.WateringCan && useItemId == 6001 && crop.isHarvest == false)
                            {
                                isFarmAction = true;
                                PlayToolSound();
                                GetFarmTile(tilePos, isUseTool);
                                ShowUseToolSprite();
                                isFarmAction = false;
                                SetIdle(dir);
                                return;
                            }
                            
                            else
                            {
                                this.movement2D.pathList.Clear();
                                this.onDecideTargetTile(curPos, targetPos, this.movement2D.pathList);
                                return;
                            }
                        }

                        else if (findGo.tag == "Building")
                        {
                            Silo silo = findGo.GetComponent<Silo>();
                            this.onShowStateUI(silo);
                            if (useItemId == 4004)
                            {
                                FillHay();
                                PlaySound(ePlayerSound.DropItem);
                                PlaySound(ePlayerSound.RanchUI);
                                useItemId = -1;
                            }
                            SetIdle(dir);
                            PlaySound(ePlayerSound.RanchUI);
                        }

                        else if (findGo.tag == "Animal")
                        {
                            if (findGo.GetComponent<BarnAnimal>() != null)
                            {
                                if (findGo.GetComponent<BarnAnimal>().Produce())
                                    return;
                            }

                            Animal animal = findGo.GetComponent<Animal>();
                            if (animal.isPatted == false)
                            {
                                animal.Patted();
                                animal.PlaySound(animal.id);
                                SetIdle(dir);
                            }
                            else
                            {
                                animal.PlaySound(animal.id);
                                this.onShowAnimalUI(animal);
                                isShowAnimalUI = true;
                            }
                            return;
                        }
                        else if (findGo.tag == "Gathering")
                        {
                            Gather(findGo);
                            SetIdle(dir);
                            return;
                        }
                        else if (findGo.tag == "Ruck")
                        {
                            useItemId = GetTooltype(findGo);
                            Break(findGo, useItemId);
                            SetIdle(dir);
                            return;
                        }
                        else if (findGo.tag == "Shop")
                        {
                            SoundManager.instance.PlaySound(SoundManager.eButtonSound.Shop);
                            var shop = findGo.GetComponent<ShopObject>();
                            shop.ShowShop();
                        }
                        else if (findGo.tag == "ShippingBin")
                        {
                            PlaySound(ePlayerSound.OpenShippingBox);
                            var uiFarm = GameObject.FindObjectOfType<UIFarm>();
                            uiFarm.ShowShippingUI();
                        }
                        else if (findGo.tag == "AnimalDoor")
                        {
                            SoundManager.instance.PlaySound(SoundManager.eButtonSound.Portal);
                            var coop = findGo.GetComponentInParent<Coop>();
                            coop.ClickedDoor();
                        }
                    }
                    SetIdle(dir);
                };

                this.movement2D.pathList.Clear();
                this.onDecideTargetObject(curPos, targetPos, this.movement2D.pathList);
                return;
            }
        }
    }

    public void ItemSelected(int id)
    {
        this.useItemId = id;
        var toolList = DataManager.instance.GetDataList<ToolData>().ToList();
        var seedList = DataManager.instance.GetDataList<SeedData>().ToList();

        var tool = toolList.Find(x => x.id == id);
        var seed = seedList.Find(x => x.id == id);

        if (tool != null)
        {
            isUseTool = (eItemType)tool.type;
            return;
        }

        if(seed != null)
        {
            isUseTool = eItemType.Seed;
            return;
        }

        isUseTool = eItemType.None;
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

    public void MoveAction()
    {
        this.movement2D.MoveAction();
    }

    public void GetFarmTile(Vector3Int pos, eItemType state)
    {
        TileManager.eTileType type = farming.GetFarmTile(state);
        if (isFarmAction && type != TileManager.eTileType.None)
        {
            this.onGetTile(pos, type);
        }
    }

    public void CheckFarmTile(Vector3Int pos, eItemType state)
    {
        TileManager.eTileType type = farming.GetFarmTile(state);
        onCheckTile(pos, type);
    }

    public void CheckFarmingAct(bool check)
    {
        if (check)
            isFarmAction = true;
        else
            isFarmAction = false;
    }

    public void FarmingAct(Vector3Int pos)
    {
        Farming.eFarmActType actType = this.farming.FarmingToolAct(this.isUseTool);

        switch (actType)
        {
            case Farming.eFarmActType.None:
                break;
            case Farming.eFarmActType.Plant:
                var info = InfoManager.instance.GetInfo();
                this.onPlantCrop(this.useItemId, pos);
                if (info.playerInfo.inventory.GetItemCount(this.useItemId) == 0)
                {
                    this.useItemId = -1;
                    this.isUseTool = eItemType.None;
                }
                break;
            case Farming.eFarmActType.SetTile:
                this.onChangeFarmTile(pos, isUseTool);
                break;
            default:
                break;
        }
    }

    public void FillHay()
    {
        var inventory = InfoManager.instance.GetInfo().playerInfo.inventory;
        var hayAmount = inventory.GetItemCount(useItemId);
        onFillHay(hayAmount);
    }

    public void Harvest(GameObject findGo)
    {
        Crop crop = findGo.GetComponent<Crop>();
        var info = InfoManager.instance.GetInfo();
        var data = DataManager.instance.GetData<CropData>(crop.id);

        bool invenCheck = info.playerInfo.inventory.AddItem(data.gain_gathering_id, 1);
        if (invenCheck)
        {
            PlaySound(ePlayerSound.GetItem);
            crop.DestroyObject();
            onGetItem();
        }
        else
        {
            PlaySound(ePlayerSound.FullInventory);
            Debug.Log("자리가 없어요");
            return;
        }

    }

    // 찾은 오브젝트가 채집 오브젝트라면
    public void Gather(GameObject findGo)
    {
        OtherObject otherObject = findGo.GetComponent<OtherObject>();
        var data = DataManager.instance.GetData<GatheringData>(otherObject.id);
        var info = InfoManager.instance.GetInfo();

        bool check = info.playerInfo.inventory.AddItem(data.id, 1);
        if(check == true)
        {
            PlaySound(ePlayerSound.GetItem);
            otherObject.DestroyObject();
            onGetItem();
        }
        else
        {
            PlaySound(ePlayerSound.FullInventory);
            Debug.Log("인벤 꽉참");
        }
    }

    private void ShowUseToolSprite()
    {
        var toolImage = this.transform.Find("ToolImg");
        toolImage.gameObject.SetActive(true);
        toolImage.transform.localPosition = new Vector3(0.5f, 2.2f, 0);
        var spRenderer = toolImage.GetComponent<SpriteRenderer>();
        var tool = DataManager.instance.GetData<ToolData>(useItemId);
        var toolSpriteName = tool.sprite_name;

        var sprite = atlas.GetSprite(toolSpriteName);
        spRenderer.sprite = sprite;
        toolImage.DOLocalMoveY(2.8f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => {
            toolImage.gameObject.SetActive(false);
        });
    }

    public int GetTooltype(GameObject findGo)
    {
        OtherObject obj = findGo.GetComponent<OtherObject>();
        var ruckData = DataManager.instance.GetData<RuckData>(obj.id);
        var autoToolType = ruckData.auto_tool_type;

        int toolId = -1;
        var toolList = DataManager.instance.GetDataList<ToolData>().ToList();
        

        foreach (var tool in toolList)
        {
            if (tool.type == autoToolType)
            {
                toolId = tool.id;
                isUseTool = (eItemType)tool.type;
            }
        }
        
        return toolId;
    }

    // Ruck 인벤토리에 추가
    public void Break(GameObject findGo, int useItemId)
    {
        if(isUseTool == eItemType.Hoe || isUseTool == eItemType.Pick 
            || isUseTool == eItemType.Axe || isUseTool == eItemType.Sickle )
        {
            var useToolType = DataManager.instance.GetData<ToolData>(useItemId).type;
            OtherObject otherObject = findGo.GetComponent<OtherObject>();
            var data = DataManager.instance.GetData<RuckData>(otherObject.id);

            var info = InfoManager.instance.GetInfo();

            List<UsetoolgroupData> toolgroupdataList = DataManager.instance.GetDataList<UsetoolgroupData>().ToList();
            var toolList = toolgroupdataList.FindAll(x => x.usetool_type_group_id == data.usetool_type_group_id);

            foreach (var tool in toolList)
            {
                if (tool.tool_type == useToolType)
                {
                    if (data.ruck_name == "잔디")
                    {
                        PlayToolSound();
                        otherObject.DestroyObject();
                        onCutGrassComplete();
                        return;
                    }

                    bool invencheck = info.playerInfo.inventory.AddItem(data.gain_material_id, data.gain_material_amount);
                    if (invencheck == true)
                    {
                        ShowUseToolSprite();
                        PlayToolSound();
                        otherObject.DestroyObject();
                        onGetItem();
                    }
                    else
                    {
                        PlaySound(ePlayerSound.FullInventory);
                        Debug.Log("인벤 꽉참");
                    }
                }
            }
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
        }
        return findGo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Roof")
        {
            collision.transform.parent.GetComponent<TransparentObject>().HideObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Roof")
        {
            collision.transform.parent.GetComponent<TransparentObject>().ShowObject();
        }
    }

    public enum ePlayerSound
    {
        Hoe,        // 호미 사용
        watringCan, // 물뿌리개 사용
        Pick,      // 곡괭이 사용
        Sickle,       // 낫 사용
        Axe,       // 도끼 사용
        Walk,
        Plant,
        GetItem,
        SelectItem,
        FullInventory,
        OpenShippingBox,
        DropItem,
        RanchUI,
        ExitUI
    }

    public AudioClip[] arrPlayerSound;

    public void PlaySound(ePlayerSound soundType)
    {
        SoundManager.instance.PlaySound(arrPlayerSound[(int)soundType]);
    }

    public void PlayToolSound()
    {
        if (useItemId == 6000)
            PlaySound(ePlayerSound.Hoe);
        else if (useItemId == 6001)
            PlaySound(ePlayerSound.watringCan);
        else if (useItemId == 6002)
            PlaySound(ePlayerSound.Axe);
        else if (useItemId == 6003)
            PlaySound(ePlayerSound.Pick);
        else if (useItemId == 6004)
            PlaySound(ePlayerSound.Sickle);
    }
}

// 작성자 : 박정식
// 마지막 수정 : 2022-08-14 
// 플레이어 관련 스크립트입니다.
