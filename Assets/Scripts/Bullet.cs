using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 5f;
    private bool _isEnemyLaser = false;
    [SerializeField] private BulletData BulletData;

    // Update is called once per frame
    private void Start()
    {
        if (BulletData != null)
        {
            speed = BulletData.speed;
            damage = BulletData.damage;
        } else
        {
            Debug.LogWarning("Bullet: BulletData is null");
        }
    }
    void Update()
    {
        if (_isEnemyLaser)
        {
            MoveDown();
        } else
        {
            MoveUp();
        }
    }
    public void MoveUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
    public void MoveDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.PlayerData.currentHealth -= this.BulletData.damage;
            GameplayEvent.PlayerHited?.Invoke(player.PlayerData);
        }
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Collision");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)damage);
            }
            GameplayEvent.EnemyHited?.Invoke(enemy.EnemyData);
        }
    }
}
