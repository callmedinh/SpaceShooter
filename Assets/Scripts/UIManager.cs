using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;


    [SerializeField] private PlayerData PlayerData;

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance );
        }else
        {
            instance = this;
        }
    }
    void Start()
    {
        _scoreText.SetText("Score: " + 0);

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        GameplayEvent.EnemyHited += OnEnemyHitted;
        GameplayEvent.PlayerHited += OnPlayerHitted;
        GameplayEvent.GameOver += DisplayGameover;

    }
    private void OnDisable()
    {
        GameplayEvent.EnemyHited -= OnEnemyHitted;
        GameplayEvent.PlayerHited -= OnPlayerHitted;
        GameplayEvent.GameOver -= DisplayGameover;
    }
    private void OnPlayerHitted(PlayerData playerData)
    {
        
    }
    private void OnEnemyHitted(EnemyData enemyData)
    {
        PlayerData.score += enemyData.point;
        UpdateScore(PlayerData.score);
    }
    public void ResumePlay()
    {
        GameManager.Instance.HidePausePanel();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    private void UpdateScore(int score)
    {
        _scoreText.SetText("Score: " + score);
    }
    public void DisplayGameover()
    {
        StartCoroutine(FlickerRoutine());
        _restartText.gameObject.SetActive(true);
    }
    IEnumerator FlickerRoutine()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(3f);
        }
    }
}
