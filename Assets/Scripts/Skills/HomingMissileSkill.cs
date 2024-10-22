using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileSkill : ISkill
{
    private BulletData bulletData;
    private Transform target;
    private float homingSensitivity;

    public HomingMissileSkill(BulletData m_bulletData, Transform m_firePoint, Transform m_target, float m_homingSensitivity)
    {
        bulletData = m_bulletData;
        target = m_target;
        homingSensitivity = m_homingSensitivity;
    }

    public void Execute(Transform firePoint)
    {
        GameObject missile = Object.Instantiate(bulletData.bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
