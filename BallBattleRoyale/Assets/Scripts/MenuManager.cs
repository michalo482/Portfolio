using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject musicSettings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTheGame()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Prototype 4");
    }

    public void ExitToWindows()
    {
        Application.Quit();
    }

    public void CloseMusicStettings()
    {
        musicSettings.SetActive(false);
    }

    public void OpenMusicSettings()
    {
        musicSettings.SetActive(true); 
    }
}
