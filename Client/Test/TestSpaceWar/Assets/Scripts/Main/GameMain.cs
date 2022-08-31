using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameMain : SceneMain
{
    public enum ePlayMode
    {
        Test, Release
    }

    public Image dim;
    public GameObject playerPrefab;
    public ePlayMode playMode;
    private ItemSpawner itemSpawner;
    private VfxSpawner vfxSpawner;
    private EnemyGenerator enemyGenerator;
    private BackGrounds backGrounds;
    private Player player;
    private Camera mainCamera;

    private int score = 0;
    private float delta = 0;

    public override void Init(SceneParams param = null)
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        this.vfxSpawner = GameObject.FindObjectOfType<VfxSpawner>();
        this.enemyGenerator = GameObject.FindObjectOfType<EnemyGenerator>();
        this.backGrounds = GameObject.Find("BackGrounds").GetComponent<BackGrounds>();
        this.mainCamera = Camera.main;
        //StartCoroutine(this.TouchToStartRoutine());

        this.enemyGenerator.onEnemyDie = (pos) =>
        {
            vfxSpawner.SpawnExplosion(pos);
        };
        this.player.onDie = (pos) =>
        {
            //vfxSpawner.SpawnExplosion(pos);

        };
        this.player.onHit = (pos) =>
        {
            this.Dispatch("onGameOver");
        };

        this.vfxSpawner.onExplosionComplet = (pos) =>
        {
            itemSpawner.SpawnItem(pos);
        };

        StartGame();
    }



    private void StartGame()
    {
        enemyGenerator.CreateEnemies();
    }


    IEnumerator WaitForSec(UnityAction callback)
    {
        yield return new WaitForSeconds(0.3f);
        callback();
    }

    private IEnumerator CameraEffectRoutine()
    {
        for(int i=0; i<2; i++)
        {
            mainCamera.transform.position = new Vector3(0.2f, 0, -10);
            yield return new WaitForSeconds(0.05f);
            mainCamera.transform.position = new Vector3(-0.2f, 0, -10);
            yield return new WaitForSeconds(0.05f);
        }
        mainCamera.transform.position = new Vector3(0, 0, -10);
    }



}
