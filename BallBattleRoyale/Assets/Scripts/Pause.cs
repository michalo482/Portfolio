using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{    
    public GameObject pauseScreen;
    public GameObject musicScreen;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && gameManager.isGameActive)
        {
            if (Time.timeScale > 0)
            {
                
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
            else
            {
                
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
        }
    }

    public void ShowMusicScreen()
    {
        musicScreen.SetActive(true);
    }
    
    public void HideMusicScreen()
    {
        musicScreen.SetActive(false);
    }
}
