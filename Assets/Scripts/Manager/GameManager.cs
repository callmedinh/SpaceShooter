using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerData CurrentPlayer;
    public BulletData BulletData;
    public PlayerData[] playerList;
    private bool bossSpawned = false;
    private int _enemyKilled = 0;
    private bool _gameStarted = false;


    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }else
        {
            instance = this;
        }
        CurrentPlayer = playerList[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //activeSelf: The local active state of the GameObject. True if active, false if inactive. (Read Only)
            UIManager.Instance.TooglePauseMenu();
        }
        if (!bossSpawned && CurrentPlayer.enemyKilled > 5 && _gameStarted)
        {
            SpawnManager.Instance.SpawnBoss();
            bossSpawned = true; // Đánh dấu là boss đã được spawn
        }
    }
    private void OnEnable()
    {
        GameplayEvent.PlayerHited += EndGame;
        GameplayEvent.OnBossDeath += WinGame;
        
    }
    private void OnDisable()
    {
        GameplayEvent.PlayerHited -= EndGame;
        GameplayEvent.OnBossDeath -= WinGame;
    }
    public void EndGame(int health)
    {
        if (health <= 0)
        {
            GameplayEvent.GameOver?.Invoke();
            Time.timeScale = 0;
        }
    }
    public void WinGame()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        //reset player
        CurrentPlayer.currentHealth = CurrentPlayer.maxHealth;
        CurrentPlayer.score = 0;
        CurrentPlayer.enemyKilled = 0;
        //initialize Layer and pass data into it
        GameObject playerObject =  Instantiate(CurrentPlayer.playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Player player = playerObject.GetComponent<Player>();

        if (player != null)
        {
            player.Initialize(BulletData, CurrentPlayer);
        }
        Debug.Log("Start game Player: " + CurrentPlayer);
        UIManager.Instance.DisplayHUD(true);
        GameplayEvent.GameStarted?.Invoke(true);
        bossSpawned = false;
        _gameStarted = true;
    }
}
