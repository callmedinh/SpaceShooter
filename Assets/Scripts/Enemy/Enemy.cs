using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IShootable
{
    private BulletData _bulletData;
    public EnemyData EnemyData;


    private float _fireRate = 3f;
    private float _speed = 4f;
    private float _canFire = 0.1f;
    private Transform _firePoint;
    private float _horizontalSpeed = 2f;
    private Vector2 _direction;
    public int _health;

    public void Initialize(EnemyData enemyData, BulletData bulletData)
    {
        EnemyData = enemyData;
        _bulletData = bulletData;
        _health = enemyData.health;
    }
    private void Awake()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), -1).normalized;
    }


    void Update()
    {
        _firePoint = this.transform;
        CalculateMovement();
        Shoot(_bulletData, _firePoint);
  
    }
    public void CalculateMovement()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            transform.position = new Vector3(Random.Range(-6, 6), 8, transform.position.z);
        }
        else
        {
            // Đổi hướng khi chạm vào bờ
            if (transform.position.x <= -6 || transform.position.x >= 6)
            {
                _direction.x *= -1; // Đổi hướng ngang
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(EnemyData.damage);
                GameplayEvent.PlayerHited?.Invoke(player.GetCurrentHealth());
                Destroy(this.gameObject);
            }
        }
    }
    public void TakeDamage(int amount)
    {
        _health -= amount;
        Debug.Log(_health);
        if (_health <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
        GameplayEvent.EnemyKilled?.Invoke();
    }

    public void Shoot(BulletData bulletData, Transform firePoint)
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            GameObject bullet = Instantiate(bulletData.bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(Vector2.down, bulletData.speed, bulletData.damage, true);
            }
        }
    }
}
