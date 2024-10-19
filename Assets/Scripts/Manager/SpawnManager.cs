using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyData EnemyData;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerupPrefab;

    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get { return instance; }
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
        if (EnemyData == null)
        {
            Debug.LogError("SpawnManager: EnemyData is null");
        }
        StartSpawning();
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3));
            Vector3 posToSpawn = new(Random.Range(-6, 6), 8, 0);
            GameObject newEnemy = Instantiate(EnemyData.enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
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
