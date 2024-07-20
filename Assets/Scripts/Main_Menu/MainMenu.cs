using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button soloButton = root.Q<Button>("Solo_btn");
        Button duoButton = root.Q<Button>("Duo_btn");

        soloButton.clickable.clicked += LoadSinglePlayerGame;
        duoButton.clickable.clicked += LoadDouPlayerGame;

    }
    public void LoadSinglePlayerGame()
    {
        Debug.Log("Single Player Game");
        GameManager.isDouPlayerMode = false;
        SceneManager.LoadScene(1);
    }
    public void LoadDouPlayerGame()
    {
        Debug.Log("Dou Player Game");
        GameManager.isDouPlayerMode = true;
        SceneManager.LoadScene(2);
    }
}
