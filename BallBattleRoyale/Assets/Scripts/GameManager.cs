using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TextMeshProUGUI levelText; 
    public bool isGameActive = true;
    public GameObject gameOverScreen;

    public PlayerController playerController;
    public Vector3 startingPosition;

    public BackgroundMusic backgroundMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = playerController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(float timePassed)
    {
        //levelText.text = "Poziom: " + levelCount;
        string formattedNumber = string.Format("{0:0:00}", timePassed);
        levelText.text = "Czas gry: " + formattedNumber;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        playerController.transform.position = startingPosition;
        Time.timeScale = 0;
        backgroundMusic.SwitchMusic();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToManu()
    {       
        SceneManager.LoadScene("MenuScene");
        
    }

    public void ExitToWindows()
    {
        Application.Quit();
    }
}
