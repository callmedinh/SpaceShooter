using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotSkill : ISkill
{
    private BulletData _bulletData;
    private int _bulletCount = 10;
    private float _spreadAngle = 90f;

    public SpreadShotSkill(BulletData bulletData)
    {
        this._bulletData = bulletData;
    }
    public void Execute(Transform firePoint)
    {
        for (int i = 0; i< _bulletCount; i++)
        {
            float angle = -_spreadAngle/2 + (_spreadAngle / (_bulletCount - 1)) * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Object.Instantiate(_bulletData.bulletPrefab, firePoint.position, rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null )
            {
                bulletScript.Initialize(Vector2.down, _bulletData.speed, _bulletData.damage, true);
            }
        }
    }
}
