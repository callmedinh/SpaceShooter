using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyData EnemyData;
    [SerializeField] private BulletData EnemyBullet;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerupPrefab;
    [SerializeField] private EnemyData BossData;
    public List<ISkill> BossSkill;

    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get { return instance; }
    }
    private void OnEnable()
    {
        GameplayEvent.GameStarted += StartSpawning;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }
    public void StartSpawning(bool isGameStarted)
    {
        if (isGameStarted)
        {
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerupRoutine());
        }
    }
    public void SpawnBoss()
    {
        GameObject boss = Instantiate(BossData.enemyPrefab, new Vector3(0, 5, 0), Quaternion.identity);
        Boss bossScript = boss.GetComponent<Boss>();
        if (bossScript != null)
        {
            bossScript.Initialize(BossData.health, BossData.speed);
        }
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3));
            Vector3 posToSpawn = new(Random.Range(-6, 6), 8, 0);
            GameObject newEnemy = Instantiate(EnemyData.enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.Initialize(EnemyData, EnemyBullet);
            }
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.5f);
            Vector3 posToSpawn = new(Random.Range(-6, 6), 8, 0);
            Instantiate(_powerupPrefab[Random.Range(0, _powerupPrefab.Length)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
