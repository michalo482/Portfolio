using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudioSource;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce = 10.0f;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJump = false;
    public bool dash = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            dirtParticle.Stop();
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            doubleJump = true;
            playerAudioSource.PlayOneShot(jumpSound, 0.4f);
            playerAnim.SetTrigger("Jump_trig");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && doubleJump)
        {
            dirtParticle.Stop();
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = false;
            playerAudioSource.PlayOneShot(jumpSound, 0.4f);
            playerAnim.SetTrigger("Jump_trig");
        }

        if(Input.GetKeyDown(KeyCode.E) && !gameOver)
        {
            dash = true;
            playerAnim.SetFloat("Speed_Multiplayer", 2.0f);
        }
        else if (dash)
        {
            dash = false;
            playerAnim.SetFloat("Speed_Multiplayer", 1.0f);
        }


       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            isOnGround = true;
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            dirtParticle.Stop();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudioSource.PlayOneShot(crashSound, 0.8f);
            explosionParticle.Play();
        }
    }
}
