using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public BulletData BulletData;
    public EnemyData EnemyData;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    private float _speed = 4f;

    public int health;

    private void Start()
    {
        if (EnemyData != null && BulletData != null)
        {
            _speed = EnemyData.speed;
            _fireRate = BulletData.fireRate;
        }
        else
        {
            Debug.LogWarning("Enemy: EnemyData || BulletData are null");
        }
    }

    void Update()
    {
        CalculateMovement();
    }
    public void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 5f);
            _canFire = Time.time + _fireRate;
            //laser prefab have 2 children
            GameObject enemyLaser = Instantiate(BulletData.bulletPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
        }
    }
    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            transform.position = new(Random.Range(-6, 6), 8, transform.position.z);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.PlayerData.currentHealth -= this.EnemyData.damage;
            Destroy(this.gameObject);
            GameplayEvent.PlayerHited?.Invoke(player.PlayerData);
        }
    }
}
