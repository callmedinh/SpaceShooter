using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    private Animator _anim;
    private AudioSource _audioSource;

    private float _fireRate = 3f;
    private float _canFire = -1f;
    private float _speed = 4f;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

        if (_anim == null )
        {
            Debug.LogError("Animation is NULL");
        }
        if (_audioSource == null )
        {
            Debug.LogError("Audio Source is NULL");
        }
        if (_laserPrefab == null )
        {
            Debug.LogError("Enemy: Laser prefab is NULL");
        }
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }
    public void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 5f);
            _canFire = Time.time + _fireRate;
            //laser prefab have 2 children
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            foreach (Laser laser in lasers)
            {
                laser.AssignEnemyLaser();
            }
        }
    }
    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            transform.position = new(Random.Range(-8, 8), 8, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            Laser laser = collision.GetComponent<Laser>();
            if (!laser.IsEnemyLaser() && laser != null)
            {
                Player player = laser.GetFiringPlayer();
                if (player != null)
                {
                    player.AddScore(10);
                }
                Destroy(collision.gameObject);
                _anim.SetTrigger("OnEnemyDeath");
                _speed = 0;
                _audioSource.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.8f);
            }
        }
        if (collision.CompareTag("Player"))
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _speed = 0;
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
}
