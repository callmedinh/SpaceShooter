using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 1)]
public class BulletData : ScriptableObject
{
    public int damage;
    public float speed;
    public float fireRate;
    public GameObject bulletPrefab;
}
