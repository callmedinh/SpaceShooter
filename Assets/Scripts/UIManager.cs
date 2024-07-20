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
    [SerializeField] private Image[] _livesImage;
    [SerializeField] private Sprite[] _livesSprite;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;

    public int score, bestScore;

    Player[] players;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _scoreText.SetText("Score: " + 0);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        _bestScoreText.SetText("Best: " + bestScore);

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

    }
    private void Update()
    {

        players = _gameManager.GetPlayerList().ToArray();
    }
    public void ResumePlay()
    {
        _gameManager.HidePausePanel();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.SetText("Score: " + playerScore.ToString());
    }
    public void CheckForBestScore(int playerScore)
    {
        if (playerScore > bestScore)
        {
            bestScore = playerScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            _bestScoreText.SetText("Best: " + bestScore.ToString());
        }
    }
    public void UpdateLivesImage(int lives, bool isPlayer1)
    {
        if (lives < 0)
        {
            return;
        }
        if (GameManager.isDouPlayerMode)
        {
            if (isPlayer1)
            {
                _livesImage[0].sprite = _livesSprite[lives];
            } else if (!isPlayer1)
            {
                _livesImage[1].sprite = _livesSprite[lives];
            }
        }
        else
        {
            if (_livesImage.Length > 0)
            {
                Debug.Log("Updating single player lives image to sprite index: " + lives);
                _livesImage[0].sprite = _livesSprite[lives];
            }else
            {
                Debug.LogError("Invalid lives value or _livesImage array length.");
            }
        }
    }
    public void DisplayGameover()
    {
        _gameManager.ClearPlayerList();
        _gameManager.GameOver();
        StartCoroutine(FlickerRoutine());
        _restartText.gameObject.SetActive(true);
    }
    IEnumerator FlickerRoutine()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
