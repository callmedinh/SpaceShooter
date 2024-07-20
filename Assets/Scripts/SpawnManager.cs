using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerupPrefab;

    private bool _stopSpawning = false;
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new(Random.Range(-6, 6), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new(Random.Range(-6, 6), 8, 0);
            Instantiate(_powerupPrefab[Random.Range(0, _powerupPrefab.Length)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }
    public void PlayerDeath()
    {
        _stopSpawning = true;
    }
}
