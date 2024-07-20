using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 10.0f;
    [SerializeField] GameObject _explosionPrefabs;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Asteriod: Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            _spawnManager.StartSpawning();
            Destroy(collision.gameObject);
            Instantiate(_explosionPrefabs, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
