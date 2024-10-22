using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject PauseMenu;

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }else
        {
            instance = this;
        }
    }

    public void DisplayHUD(bool display)
    {
        HUD.SetActive(display);
    }
    public void TooglePauseMenu()
    {
        bool isPaused = PauseMenu.activeSelf;
        PauseMenu.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1 : 0;
    }
}
