using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IDamageable, IShootable
{
    private BulletData _bulletData;
    private PlayerData _playerData;
    private int _currentHealth;

    private float _speed = 3.5f;
    private float _fireRate = 0.2f;
    private float _canFire = 0.2f;

    public SpriteRenderer spriteRenderer;

    private float minX, maxX, minY, maxY;


    private Transform firePoint;

    public void Initialize(BulletData bulletData, PlayerData playerData)
    {
        this._bulletData = bulletData;
        this._playerData = playerData;
        if (_playerData != null )
        {
            _speed = _playerData.speed;
            _currentHealth = _playerData.currentHealth;
            _fireRate = _playerData.fireRate;
        }
    }
    private void Start()
    {
       
    }
    void Update()
    {
        firePoint = this.transform;
        if (Input.anyKey)
        {
            HandleMovement();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (_bulletData != null)
            {
                Shoot(_bulletData, firePoint);
            }
        }
    }
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movement.normalized * _speed * Time.deltaTime);
    }
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
        StartCoroutine(FlickerEffect());
    }
    public void Die()
    {
        Destroy(gameObject);
        GameplayEvent.GameOver?.Invoke();
    }
    public void ActivateBulletPowerup(BulletData bulletData)
    {
        _bulletData = bulletData;
    }
    public int GetCurrentHealth()
    {
        return _currentHealth;
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
                bulletScript.Initialize(Vector2.up, bulletData.speed, bulletData.damage, false);
            }
        }
    }
    private IEnumerator FlickerEffect()
    {
        float flickerDuration = 0.1f;
        int flickerCount = 5;

        for (int i = 0; i < flickerCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flickerDuration);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flickerDuration);
        }
    }
}
