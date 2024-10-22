using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    protected float _health;
    protected float _speed;
    public virtual void Initialize(float health, float speed)
    {
        _health = health;
        _speed = speed;
    }
    public abstract void Move();

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if ( _health <= 0 )
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}
