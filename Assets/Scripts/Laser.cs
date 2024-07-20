using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8f;
    private bool _isEnemyLaser = false;
    private Player _fireBy;

    // Update is called once per frame
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
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
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
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    public void AssignPlayerLaser(Player player)
    {
        _fireBy = player;
    }
    public Player GetFiringPlayer()
    {
        return _fireBy;
    }
    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();
            player.Damage();
        }
    }
}
