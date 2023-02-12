using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip mainMusic;
    public AudioClip deathMusic;
    private AudioSource audioSource;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainMusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMusic()
    {
        if (!gameManager.isGameActive)
        {
            audioSource.Stop();
            audioSource.clip = deathMusic;
            audioSource.Play();
        }
    }
}
