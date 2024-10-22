using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 8f;
    private float _damage = 5f;
    private Vector2 _direction;
    public bool _isEnemyBullet = false;


    public void Initialize(Vector2 direction, float speed, float damage, bool enemyBullet)
    {
        this._direction = direction;
        this._speed = speed;
        this._damage = damage;
        this._isEnemyBullet = enemyBullet;
    } 
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
        if (transform.position.y > 8 || transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEnemyBullet)
        {
            if (collision.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage((int)this._damage);
                    GameplayEvent.PlayerHited?.Invoke(player.GetCurrentHealth());
                    Destroy(this.gameObject);
                }
            }
        }else if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)_damage);
                GameplayEvent.EnemyHited?.Invoke(enemy.EnemyData);
            }
            Destroy(this.gameObject);
        }
    }
}
