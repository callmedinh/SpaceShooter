using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private float _speed = 3.5f;
    [SerializeField] private BulletData BulletData;
    public PlayerData PlayerData;

    private float _offset = 1.05f;
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private int _lives;
    private int _score;



    void Start()
    {
        if (BulletData == null || PlayerData == null)
        {
            Debug.LogWarning("Player: BulletData || PlayerData are null");
        }
        _lives = PlayerData.maxHealth;
        _score = PlayerData.score;
    }
    void Update()
    {
        HandleMovement(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    private void HandleMovement(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        if (Input.GetKey(left))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }else if (Input.GetKey(right))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }else if (Input.GetKey(up))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }else if (Input.GetKey(down))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        } else
        {
            
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        GameObject bulletObject = Instantiate(BulletData.bulletPrefab, transform.position + new Vector3(0, _offset, 0), Quaternion.identity);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.speed = BulletData.speed;
        bulletScript.damage = BulletData.damage;
    }
    public void ActivateBulletPowerup(BulletData bulletData)
    {
        BulletData = bulletData;
        _fireRate = bulletData.fireRate;
    }
    public void AddScore(int points)
    {
        _score += points;
    }
    public int GetLive()
    {
        return _lives;
    }
}
