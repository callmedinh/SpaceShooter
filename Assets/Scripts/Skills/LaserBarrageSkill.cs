using UnityEngine;
using System.Collections;
public class LaserBarrageSkill : MonoBehaviour, ISkill
{
    private BulletData _bulletData;
    private float _chargeTime;
    private float _laserDuration;

    public LaserBarrageSkill(BulletData bulletData, float chargeTime, float laserDuration)
    {
        this._chargeTime = chargeTime;
        this._laserDuration = laserDuration;
        _bulletData = bulletData;
    }

    public void Execute(Transform firePoint)
    {
        CoroutineManager.Instance.StartCoroutine(LaserSequence(firePoint));
    }

    IEnumerator LaserSequence(Transform firePoint)
    {
        yield return new WaitForSeconds(_chargeTime);
        GameObject laser = Object.Instantiate(_bulletData.bulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(_laserDuration);
        Object.Destroy(laser);
    }
}
