using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionManager : MonoBehaviour
{
    public int currentPlayerIndex = 0;
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text speed;

    private void OnEnable()
    {
        UpdateInfo(GameManager.Instance.CurrentPlayer);
    }

    public void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % GameManager.Instance.playerList.Length;
        UpdateCurrentPlayer();
        UpdateInfo(GameManager.Instance.CurrentPlayer);
    }

    public void PrePlayer()
    {
        currentPlayerIndex = (currentPlayerIndex - 1 + GameManager.Instance.playerList.Length) % GameManager.Instance.playerList.Length;
        UpdateCurrentPlayer();
        UpdateInfo(GameManager.Instance.CurrentPlayer);
    }

    public void SelectPlayer()
    {
        UpdateCurrentPlayer(); // Đảm bảo cập nhật nhân vật trước khi bắt đầu game
        Debug.Log("Selected Player: " + GameManager.Instance.CurrentPlayer.namePlayer);
        GameManager.Instance.StartGame();
        this.gameObject.SetActive(false);
        
    }

    private void UpdateCurrentPlayer()
    {
        GameManager.Instance.CurrentPlayer = GameManager.Instance.playerList[currentPlayerIndex];
        UpdatePlayerImage(GameManager.Instance.CurrentPlayer.playerImage);
    }

    public void UpdatePlayerImage(Sprite newPlayerSprite)
    {
        playerImage.sprite = newPlayerSprite;
    }
    public void UpdateInfo(PlayerData playerData)
    {
        name_text.text = playerData.namePlayer;
        health.text = "Health: " + playerData.maxHealth.ToString();
        speed.text = "Speed: " + playerData.speed.ToString();
    }
}
