using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text enemyKilledText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text restartgameText;
    [SerializeField] private TMP_Text gameoverText;
    [SerializeField] private TMP_Text winText;

    private PlayerData currentPlayerData;

    private static HUDManager instance;
    public static HUDManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        ResetHUD();
    }

    private void OnEnable()
    {
        currentPlayerData = GameManager.Instance.CurrentPlayer;
        GameplayEvent.EnemyHited += OnEnemyHitted;
        GameplayEvent.PlayerHited += OnPlayerHitted;
        GameplayEvent.GameOver += DisplayGameover;
        GameplayEvent.OnBossDeath += DisplayGameWin;
        GameplayEvent.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameplayEvent.EnemyHited -= OnEnemyHitted;
        GameplayEvent.PlayerHited -= OnPlayerHitted;
        GameplayEvent.GameOver -= DisplayGameover;
        GameplayEvent.OnBossDeath -= DisplayGameWin;
        GameplayEvent.EnemyKilled -= OnEnemyKilled;

    }
    private void DisplayGameWin()
    {
        winText.gameObject.SetActive(true);
    }
    private void OnPlayerHitted(int health)
    {
        UpdateHealth(health);
    }
    private void OnEnemyKilled()
    {
        currentPlayerData.enemyKilled += 1;
        UpdateEnemyKilled(currentPlayerData.enemyKilled);
    }
    public void DisplayGameover()
    {
        gameoverText.gameObject.SetActive(true);
        restartgameText.gameObject.SetActive(true);
    }
    private void OnEnemyHitted(EnemyData enemyData)
    {
        currentPlayerData.score += enemyData.point;
        UpdateScore(currentPlayerData.score);
    }


    private void UpdateHealth(int health)
    {
        healthSlider.value = health;
    }

    private void UpdateScore(int score)
    {
        scoreText.SetText("Score: " + score);
    }

    private void UpdateEnemyKilled(int enemyKilled)
    {
        enemyKilledText.SetText("Enemies Killed: " + enemyKilled);
    }

    public void ResetHUD()
    {
        UpdateScore(0);
        UpdateEnemyKilled(0);
        healthSlider.maxValue = currentPlayerData.maxHealth;
        healthSlider.value = currentPlayerData.currentHealth;
    }

    public void ShowHUD(bool show)
    {
        gameObject.SetActive(show);
    }
    public IEnumerator TextFlicker()
    {
        while (true)
        {
            gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
