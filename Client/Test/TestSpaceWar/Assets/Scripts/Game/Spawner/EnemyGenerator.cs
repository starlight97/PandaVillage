using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] arrEnemyPrefab;
    public Transform[] arrEnemySpawnPoint;

    private List<Enemy> enemies = new List<Enemy>();
    public UnityAction<Vector3> onEnemyDie;

    private void Start()
    {
        this.CreateEnemies();
    }

    public void CreateEnemies()
    {
        this.StartCoroutine(this.CreateEnemiesRoutine());
    }

    private IEnumerator CreateEnemiesRoutine()
    {
        while (true)
        {
            var enemy = this.CreateEnemy();

            enemy.onDie = (pos) => {
                Debug.Log("on die");
                this.onEnemyDie(pos);
                this.enemies.Remove(enemy);
            };
            this.enemies.Add(enemy);
            yield return new WaitForSeconds(1f);
        }
    }

    public Enemy CreateEnemy()
    {
        var randEnemyIndex = Random.Range(0, 2);
        var go = Instantiate<GameObject>(this.arrEnemyPrefab[randEnemyIndex]);

        var randX = Random.Range(this.arrEnemySpawnPoint[0].position.x, this.arrEnemySpawnPoint[1].position.x);
        var y = this.arrEnemySpawnPoint[0].position.y;
        var initPos = new Vector3(randX, y, 0);
        go.transform.position = initPos;

        return go.GetComponent<Enemy>();
    }
}
