using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int health;
    public float speed;
    public int damage;
    public int point;
}
