using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _coopPlayer;
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private GameObject _pauseMenuPanel;

    private bool _isGameOver = true;
    public static bool isDouPlayerMode = false;

    private int numberOfPlayers;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    public List<Player> playerList = new List<Player>();

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (isDouPlayerMode)
        {
            numberOfPlayers = 2;
        }
        else
        {
            numberOfPlayers = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame(isDouPlayerMode);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiManager.BackToMainMenu();
        }
        if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isDouPlayerMode == false)
                {
                    GameObject soloPlayerPrefab = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
                    playerList.Add(soloPlayerPrefab.GetComponent<Player>());
                } else
                {
                    GameObject duoPlayerPrefab = Instantiate(_coopPlayer, Vector3.zero, Quaternion.identity);
                    Player[] players = duoPlayerPrefab.GetComponentsInChildren<Player>();
                    playerList.Add(players[0]);
                    playerList.Add(players[1]);
                }
                _isGameOver = false;
                Instantiate(_asteroidPrefab, _asteroidPrefab.transform.position, Quaternion.identity);
                 
            } else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main_Menu");
            }
        }
        //Display Game Over
        if (numberOfPlayers == 0)
        {
            _uiManager.DisplayGameover();
            _spawnManager.PlayerDeath();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void RestartGame(bool isDuoMode)
    {
        if (isDuoMode)
        {
            SceneManager.LoadScene(2);
        } else
        {
            SceneManager.LoadScene(1);
        }
    }
    public void HidePausePanel()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    public void PlayerDeath()
    {
        numberOfPlayers--;
    }
    public List<Player> GetPlayerList()
    {
        return playerList;
    }
    public void ClearPlayerList()
    {
        playerList.Clear();
    }
}
