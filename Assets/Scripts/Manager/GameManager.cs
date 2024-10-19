using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _pauseMenuPanel;



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
    }
    private void OnEnable()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.BackToMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
        GameplayEvent.PlayerHited += IsGameOver;
    }
    public void IsGameOver(PlayerData playerData)
    {
        if (playerData.currentHealth <= 0)
        {
            Debug.Log(playerData.currentHealth);
            GameplayEvent.GameOver?.Invoke();
            Time.timeScale = 0;
        }
    }
    public void HidePausePanel()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
