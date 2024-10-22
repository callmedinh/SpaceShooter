using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int currentHealth;
    public int maxHealth;
    public int score;
    public int enemyKilled;
    public float fireRate;
    public Sprite playerImage;
    public GameObject playerPrefab;
    public string namePlayer;
    public float speed;
}
