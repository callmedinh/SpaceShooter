using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyBase
{
    private Vector2 _direction;
    public override void Initialize(float health, float speed)
    {
        base.Initialize(health, speed);
        _direction = new Vector2(Random.Range(-1f, 1f), - 1).normalized;
    }
    public override void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            transform.position = new Vector3(Random.Range(-6, 6), 8, transform.position.z);
        }
        else if (transform.position.x <= -6 || transform.position.x >= 6)
        {
            _direction.x *= -1;
        }
    }
}
